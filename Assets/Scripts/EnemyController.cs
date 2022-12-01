using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Controllers;
using CustomAttributes;
using DG.Tweening;
using Extensions;
using Infrastructure;
using Models.Constants;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

[RequireComponent(typeof(SkeletonAnimation))]
[ExecuteInEditMode]
public class EnemyController : ItemController, IPauseController
{
    public bool IsDead { get; private set; }
    /// <summary>
    /// this is important for playing animations sound
    /// </summary>
    [BoxGroup("Characteristic")] 
    public CharacterType CharacterType;
    [BoxGroup("Characteristic")]
    public int Health = 100;
    [BoxGroup("Characteristic")]
    public bool CanNotGetHit;
    [BoxGroup("Teasing")]
    public AnimationNames TeasingAnimation;
    [BoxGroup("Teasing")]
    public AnimationNames GotTeasedAnimation;
    [BoxGroup("Teasing")]
    public TeasingType TeasingType;
   
    [BoxGroup("Teasing")]
    public GameObject TeasingThrowableObject;
    [BoxGroup("Teasing")]
    public bool StayPutAfterGotTeased;

    [BoxGroup("Teasing")]
    public List<EnemyController> TeasingTargets;
    [BoxGroup("Path to walk")]
    public List<WaypointController> WalkPoints = new List<WaypointController>();
    public int KillOrder { get; set; } = -1;
    [BoxGroup("Appearance")]
    public SkeletonAnimation SkeletonAnimation;
    [BoxGroup("Appearance")]
    public bool IsRecursive;
    [BoxGroup("Appearance")]
    [SerializeField, GetSet("OrderInLayer")]
    private int _orderInLayer;
    [BoxGroup("Appearance")]
    public int ActivateIn;
    [BoxGroup("Appearance")]
    public float RunSpeed = 1;
    [BoxGroup("Appearance")]
    public float Speed = 1;
    public string Id { get; set; }
    private float _duration;
    private bool _hasCustomAnimation;
    WaypointController _currentWayPoint;
    public bool IsPaused { get; set; }
    [BoxGroup("Run After Fail")]
    public bool RunLeft;
    [BoxGroup("Run After Fail")]
    public bool RunRight;
    [BoxGroup("Run After Hit")]
    public bool RunRightOnHit;
    [BoxGroup("Run After Hit")]
    public bool RunLeftOnHit;

    public int OrderInLayer
    {
        get { return _orderInLayer; }
        set
        {
            _orderInLayer = value;
            SkeletonAnimation.gameObject.GetComponent<MeshRenderer>().sortingOrder = value;
        }
    }

    private float _teasingDistance = 0.5f; //if teaser's distance from target gets <= teasing distance, teasing will start
    public int GetOrder()
    {
        return KillOrder;
    }
    public void SetOrder(int order)
    {
        KillOrder = order;
    }
    private float _delay = 0;
    private bool _rewinding;
    private float _lastAnimTimeScale;
    private LoopType _loopType;
    private bool _gotTeased;
    private string _lastAnimationStateBeforeTease;
    public bool IsAlreadyPlayingSound { get; set; }
    /// <summary>
    ///when a teaser, teased a target, after teaser's teasing animation finished, target's got-teased animation will start.
    ///this handler gets called when target's teasing animation finished. 
    ///every teasing target registers this handler and executes it when got teased. 
    /// </summary>
    public AnimationState.TrackEntryEventDelegate TeasingEventHandler { get; set; }
    private void Start()
    {
        if (!Application.isPlaying)
        {
            //if character has not any default way point with 0,0,0 transform, we should create and attach one.
            CheckAndInsertDefaultWayPoint();
        }
        else
        {
            TeasingEventHandler = (track, e) =>
            {
                //that's what is gonna happen:
                //if calling animation was Teased, that means character got teased and teaser's teasing animation has finished
                //so it's gonna save last animation was playing on itself (teasing target)
                //and turns off looping then registers a handler for it's got-teased animation
                //after that it pauses any DOTween it has then plays it's got-teased animation and after that 
                //turns loop back on.
                //when it's got-teased animation is finished, it will checks that should stay put or should go back the last animation
                //it was playing.
                //in case it should stay put, kills all DOTween it has, else plays paused DOTween and resets to it's last animation was playing.
                if (e.Data.Name == AnimationTriggerNames.Teased.ToString())
                {
                    SkeletonAnimation.loop = false;
                    _lastAnimationStateBeforeTease = SkeletonAnimation.AnimationName;
                    SkeletonAnimation.AnimationState.Event += (t, ev) =>
                    {
                        
                        if (ev.Data.Name == AnimationTriggerNames.GotTeased.ToString())
                        {
                            if (!StayPutAfterGotTeased)
                            {
                                gameObject.transform.DOPlay();
                                SkeletonAnimation.loop = true;
                                SkeletonAnimation.PlayAnimation(this, _lastAnimationStateBeforeTease);
                            }
                            else
                            {
                                gameObject.transform.DOKill();
                            }
                        }
                    };
                    gameObject.transform.DOPause();
                    SkeletonAnimation.PlayAnimation(this, GotTeasedAnimation);
                    SkeletonAnimation.loop = true;
                    _gotTeased = true;
                }
            }; 
            InitializeEnemyWalking();
        }


    }

    private void CheckAndInsertDefaultWayPoint()
    {
        if (WalkPoints == null)
            WalkPoints = new List<WaypointController>();
        if (!WalkPoints.Any() || WalkPoints.Where(w => w != null).All(w => w.transform.localPosition != Vector3.zero))
        {
            var wayPointPrefab = ResourceManager.LoadResource<GameObject>("WayPoint");
            var wayPointObject = Instantiate(wayPointPrefab);
            wayPointObject.transform.parent = gameObject.transform;
            wayPointObject.transform.localPosition = Vector3.zero;
            var wayPointController = wayPointObject.GetComponent<WaypointController>();
            wayPointController.transform.localPosition = wayPointObject.transform.localPosition;
            wayPointController.transform.rotation = wayPointObject.transform.rotation;
            wayPointController.transform.localScale = wayPointObject.transform.localScale;
            WalkPoints.Insert(0, wayPointController);
        }
    }

    private void InitializeEnemyWalking()
    {
        if (!Application.isPlaying) return;
        if (IsPaused) return;
        SkeletonAnimation.loop = true;
        int loops;
        //creating a path from way points. character will tween on this path.
        var pathLength = WalkPoints.Where(w => w != null).Select(w => w.transform.position).Aggregate((w1, w2) => w2 - w1);
        if (Speed <= 0)
            return;
        //determines tween duration based on character speed and the path it has.
        _duration = (Mathf.Abs(pathLength.x) + Mathf.Abs(pathLength.y)) / Speed;
        if (IsRecursive) //if character's tween is recursive, we gonna use yoyo loop type.
        {
            loops = -1;
            _loopType = LoopType.Yoyo;
        }
        else //else we gonna use restart loop type.
        {
            loops = 1;
            _loopType = LoopType.Restart;
        }
        InitializeAndTween(WalkPoints, loops);
    }

    /// <summary>
    ///this method will delay currently running tween on character for given time
    ///then resumes the tween. 
    /// </summary>
    /// <param name="time">time to delay</param>
    /// <returns></returns>
    private IEnumerator Delay(float time)
    {
        while (IsPaused)
        {
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(time);
        if (!IsDead)
        {
            SkeletonAnimation.PlayAnimation(this, _hasCustomAnimation ? _currentWayPoint.SAnimationState : AnimationNames.Walk);
            gameObject.transform.DOPlay();
        }

    }
    /// <summary>
    /// will tween character on given way points and loops
    /// </summary>
    /// <param name="walkPoints">way points to walk</param>
    /// <param name="loops">number of loops for tween. -1 to infinite loops</param>
    private void InitializeAndTween(List<WaypointController> walkPoints, int loops)
    {
        if (IsPaused)
            return;
        //this determines whether current way point has a custom animation or not.
        _hasCustomAnimation = false;

        _currentWayPoint = null;
        if (walkPoints.Count > 0)
        {
            gameObject.transform.DOPath(walkPoints.Where(w => w != null).Select(w => w.transform.position).ToArray(),
                    _duration).OnStepComplete(() => //this callback gets called after every tween loop is finished.
                    {
                        if(IsDead)
                            return;
                        //if that's not first way point and character was running or walking, we should flip it.
                        if (walkPoints.IndexOf(_currentWayPoint) == 0 && _currentWayPoint.SAnimationState == AnimationNames.Run || _currentWayPoint.SAnimationState == AnimationNames.Walk)
                        {
                            
                            gameObject.transform.DOScaleX(gameObject.transform.localScale.x * -1, 0); }
                    })
                .OnStart(() => //this callback gets called on tween start. we play first way point's animation
                {
                    var sAnimationState = walkPoints[0].SAnimationState;
                    SkeletonAnimation.PlayAnimation(this, sAnimationState == AnimationNames.None ? AnimationNames.Walk : sAnimationState);
                }).OnWaypointChange(i => //this callback gets called every time character reaches to a new way point
                {
                    if (IsDead)
                    {
                        gameObject.transform.DOKill();
                        return;
                    }
                    _currentWayPoint = walkPoints[i];
                    //if current way point has custom animation, we play it.
                    if (_currentWayPoint.SAnimationState != AnimationNames.None)
                    {
                        _hasCustomAnimation = true;
                        SkeletonAnimation.PlayAnimation(this, _currentWayPoint.SAnimationState);
                    }
                    else
                    {
                        _hasCustomAnimation = false;
                    }
                    //if character should flip on this way point, we flip it.
                    if (_currentWayPoint.ShouldFlip)
                    {
                        gameObject.transform.DOScaleX(gameObject.transform.localScale.x * -1, 0);
                    }
                    //if character should delay on this way point, we delay it.
                    if (_currentWayPoint.Delay > 0)
                    {
                        //if character has custom animation for delay, we gonna set it.
                        if (_currentWayPoint.SAnimationStateDelay == AnimationNames.None)
                            SkeletonAnimation.PlayAnimation(this, _hasCustomAnimation ? _currentWayPoint.SAnimationState : AnimationNames.Stand);
                        else
                        {
                            SkeletonAnimation.PlayAnimation(this, _currentWayPoint.SAnimationStateDelay);
                        }
                        gameObject.transform.DOPause();
                        StartCoroutine(Delay(_currentWayPoint.Delay));

                    }
                }).SetEase(Ease.Linear).SetDelay(ActivateIn).SetLoops(loops, _loopType).SetOptions(false);
        }
    }
    /// <summary>
    /// every time character gets hit, this method will be called.
    /// </summary>
    public void Hit()
    {
        if (IsPaused)
            return;
        IsDead = true;
        //controlling that should character run to sides after getting hit or not.
        if (RunLeftOnHit || RunRightOnHit)
        {
            //killing character's tween before run to sides.
            gameObject.transform.DOKill();
            Run(null);
        }
        else
        {
            SkeletonAnimation.loop = false;
            SkeletonAnimation.PlayAnimation(this, AnimationNames.Die);
            DOTween.Kill(gameObject.transform);
        }
        
        //checks that user is allowed to hit this character or not
        if (CanNotGetHit)
        {
            MissionController.Instance.InnocentKilled(this);
            return;
        }
        MissionController.Instance.TargetHit(this);
        MissionController.Instance.EnemyKilled(this);

    }
    /// <summary>
    /// moves character to left or right side out of screen
    /// </summary>
    /// <param name="runCompletedCallback">a callback that will be called after character finished running out of screen</param>
    public void Run(Action<EnemyController> runCompletedCallback)
    {
        if (IsPaused)
            return;
        //getting screen boundaries
        var minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        SkeletonAnimation.PlayAnimation(this, AnimationNames.Walk);
        if (RunRight || RunRightOnHit)
        {
            if (transform.localScale.x > 0) //faces left
                gameObject.transform.DOScaleX(gameObject.transform.localScale.x * -1, 0);
            gameObject.transform.DOMoveX(maxScreenBounds.x + 3,
                (maxScreenBounds.x - gameObject.transform.position.x) / RunSpeed).OnComplete(() =>
                {
                    SkeletonAnimation.PlayAnimation(this, AnimationNames.Stand);
                    runCompletedCallback?.Invoke(this);
                });
        }
        else if (RunLeft || RunLeftOnHit)
        {
            if (transform.localScale.x < 0) //faces right
                gameObject.transform.DOScaleX(gameObject.transform.localScale.x * -1, 0);
            gameObject.transform.DOMoveX(minScreenBounds.x - 3,
                (gameObject.transform.position.x - minScreenBounds.x) / RunSpeed).OnComplete(() =>
                {
                    SkeletonAnimation.PlayAnimation(this, AnimationNames.Stand);
                    runCompletedCallback?.Invoke(this);
                });
        }
    }

    /// <summary>
    /// pauses character, both animations anf tween
    /// </summary>
    public void Pause()
    {
        DOTween.timeScale = 0;
        _lastAnimTimeScale = SkeletonAnimation.timeScale;
        IsPaused = true;
        SkeletonAnimation.timeScale = 0;
    }
    /// <summary>
    /// resumes character, both animations and tween
    /// </summary>
    public void Resume()
    {
        DOTween.timeScale = 1;
        IsPaused = false;
        SkeletonAnimation.timeScale = _lastAnimTimeScale;
    }

    private void Update()
    {
        if (!Application.isPlaying) return;
        //looks for any target that is close enough to get teased.
        for (var i = 0; i < TeasingTargets.Count; i++)
        {
            var target = TeasingTargets[i];
            if (!target._gotTeased && TeasingType == TeasingType.NonThrowable && Vector2.Distance(target.gameObject.transform.position, transform.position) <= 0.6f) //can teas
            {
                SkeletonAnimation.loop = false;
                //sets target's handler that will be called when teaser's teasing animation is finished.
                SkeletonAnimation.AnimationState.Event += target.TeasingEventHandler;
                SkeletonAnimation.PlayAnimation(this, TeasingAnimation);
                SkeletonAnimation.loop = true;
            }
        }

    }

  
}
