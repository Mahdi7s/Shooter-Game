using Contracts;
using DataServices;
using DG.Tweening;
using Extensions;
using Infrastructure;
using Menu.Controllers;
using Messaging.Hub.Providers;
using Messaging.MessageData;
using Models;
using Models.Constants;
using Sirenix.Utilities;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;
using Hellmade.Sound;
using GameplayMessageData = Messaging.MessageData.GameplayMessageData;

namespace Controllers
{
    /// <summary>
    /// controls mission flow, like characters lifecycle, wins and loses and ...
    /// </summary>
    public class MissionController : Singleton<MissionController>, IPauseController
    {
        private const int WaitTimeAfterLevelEnd = 3; //before any action after level finished, will wait this much (in seconds)
        public GameObject EnemiesContainer; //a game object that holds a reference to parent of all enemy controllers
        public GameObject BackgroundGameObject;
        public ParticleSystem ShootEffect;
        public GameplayAudioController GameplayAudioController;
        /// <summary>
        /// will be shown on level intro screen
        /// </summary>
        public string MissionGoal { get; private set; }
        public int FireLimit { get; private set; }
        /// <summary>
        /// if true, user should hit characters in given order.
        /// order is specified by KillList.
        /// </summary>
        public bool IsKillOrderImportant { get; private set; }
        /// <summary>
        /// time (in seconds) that user has to finish the level
        /// </summary>
        public int Timeout { get; private set; }
        private DateTime _missionEndTime; //the DateTime that level should be finished before that.
        /// <summary>
        /// list of characters that user should hit in order.
        /// not required if IsKillListImportant is false.
        /// </summary>
        public List<EnemyController> KillList { get; private set; }
        public WindDirectionSprite WindDirection { get; private set; }
        /// <summary>
        /// determines if the game is paused or not.
        /// </summary>
        public bool IsPaused { get; set; }
        /// <summary>
        /// if true, wind power will vary during time.
        /// has no effect if wind power is 0.
        /// </summary>
        public bool HasVariableWind { get; private set; }
        public float WindPower { get; private set; }
        private float _timeout;
        private int _currentMission = 1;
        private int _currentChapter = 1;
        private MissionProgress _missionProgress; //holds mission progress info like shots fired so far and ...
        private List<EnemyController> _enemyControllers = new List<EnemyController>(); //reference to all enemies in scene
        private int _currentMissionTotalShots;
        private Mission _loadedMission; //reference to current mission that is loaded from data source
        private bool _levelFailed; //if true, means that user has failed in current level
        private int _winReward; //number of coins that user will get if win this level
        private AudioClip _backgroundMusic;
        private Weapon _currentWeapon; //reference to the weapon that user is equipped in this level
        //public TrixPlayAudio ShootSoundEffect;

        /// <summary>
        /// determines the time (in seconds) it will take to decrease wind power to 0 and return it back to original value.
        /// has no effect if HasVariableWind is false. 
        /// </summary>
        public float WindChangeDuration { get; private set; }

        /// <summary>
        /// loads a level from a chapter at runtime into GamePlay scene.
        /// </summary>
        /// <param name="currentChapter">chapter number</param>
        /// <param name="currentMission">level number</param>
        private void LoadLevel(int currentChapter, int currentMission)
        {
            if (IsPaused)
                return;
            _levelFailed = false;
            //HACK: By ProSNY Play Gameplay Sounds 
            if (GameplayAudioController)
                GameplayAudioController.StartPlay(currentChapter, currentMission);

            _currentChapter = currentChapter;
            _currentMission = currentMission;
            //holds info about current level progress like shots, hits and ...
            _missionProgress = new MissionProgress() { ChapterNumber = _currentChapter, MissionNumber = _currentMission };
            _loadedMission = GameDataService.Instance.GetMission(currentChapter, currentMission);
            if (_loadedMission == null)
            {
#if UNITY_EDITOR
                Debug.LogError("mission not found. chapter: " + currentChapter.ToString() + ", level: " + currentMission.ToString());
#endif
                return;
            }
            _enemyControllers = new List<EnemyController>(); //will keep all characters loaded on this level
            _currentMissionTotalShots = 0;
            var container = EnemiesContainer; //reference to GameObject that holds characters at scene.
            foreach (var enemyController in container.GetComponentsInChildren<EnemyController>())
            {
                //clears every character from previous level
                Destroy(enemyController.gameObject);
            }
            var enemies = _loadedMission.GetEnemies();
            foreach (var item in enemies) //setting up characters
            {
                if (item.Prefab != null)
                {
                    var prefab = ResourceManager.LoadResource<GameObject>($"Characters/{item.Prefab.name}");
                    var gObject = Instantiate(prefab);
                    if (gObject != null)
                    {
                        gObject.transform.parent = container.transform;
                        gObject.transform.localPosition = item.Position;
                        gObject.transform.localRotation = item.Rotation;
                        gObject.transform.localScale = item.Scale;
                        // ReSharper disable once NotAccessedVariable
                        //setting skeleton animation of character
                        var sAnimationController = gObject.GetComponent<SkeletonAnimation>();
                        sAnimationController = item.SAnimation;
                        var enemyController = gObject.GetComponent<EnemyController>();
                        enemyController.IsRecursive = item.IsRecursive;
                        enemyController.RunLeft = item.RunLeft;
                        enemyController.RunRight = item.RunRight;
                        enemyController.ActivateIn = item.ActivateIn;
                        enemyController.WalkPoints = new List<WaypointController>();
                        enemyController.SetOrder(item.KillOrder);
                        enemyController.Health = item.Health;
                        enemyController.Speed = item.Speed;
                        enemyController.CanNotGetHit = item.CanNotGetHit;
                        enemyController.OrderInLayer = item.OrderInLayer;
                        enemyController.CharacterType = item.CharacterType;
                        enemyController.TeasingType = item.TeasingType;
                        enemyController.TeasingAnimation = item.TeasingAnimation;
                        enemyController.GotTeasedAnimation = item.GotTeasedAnimation;
                        enemyController.Id = item.Id;
                        enemyController.RunLeftOnHit = item.RunLeftOnHit;
                        enemyController.RunRightOnHit = item.RunRightOnHit;
                        enemyController.RunSpeed = item.RunSpeed;
                        enemyController.StayPutAfterGotTeased = item.StayPutAfterGotTeased;
                        //setting order in layer for character
                        var meshRenderer = gObject.GetComponent<MeshRenderer>();
                        meshRenderer.sortingOrder = item.OrderInLayer;
                        //setting way points
                        item.WayPoints?.ForEach(w =>
                        {
                            var wayPointObject = ResourceManager.LoadResource<GameObject>("Waypoint");
                            wayPointObject = Instantiate(wayPointObject);
                            wayPointObject.transform.parent = gObject.transform;
                            wayPointObject.transform.localPosition = w.Position;
                            wayPointObject.transform.rotation = Quaternion.Euler(w.Rotation);
                            wayPointObject.transform.localScale = w.Scale;

                            var wayPointController = wayPointObject.GetComponent<WaypointController>();
                            wayPointController.transform.localPosition = wayPointObject.transform.localPosition;
                            wayPointController.transform.rotation = wayPointObject.transform.rotation;
                            wayPointController.transform.localScale = wayPointObject.transform.localScale;
                            wayPointController.ShouldFlip = w.ShouldFlip;
                            wayPointController.Delay = w.Delay;
                            wayPointController.SAnimationState = w.SAnimationState;
                            wayPointController.SAnimationStateDelay = w.SAnimationStateDelay;
                            enemyController.WalkPoints.Add(wayPointController);
                        });
                        _enemyControllers.Add(enemyController);
                    }
                    else
                    {
                        Debug.LogError("gObject is null. loading failed.");
                    }
                }
            }
            //setting teasing targets
            foreach (var enemy in enemies)
            {
                var enemyController = _enemyControllers.SingleOrDefault(e => e.Id == enemy.Id);
                if (enemyController != null)
                {
                    var targets = _enemyControllers.Where(e => enemy.TeasingTargets.Contains(e.Id));
                    enemyController.TeasingTargets.AddRange(targets);
                }
            }
            //setting scene items (not characters)
            foreach (var item in _loadedMission.GetSceneItems())
            {
                if (item.Prefab != null)
                {
                    var prefab = ResourceManager.LoadResource<GameObject>(item.Prefab.name);
                    var gObject = Instantiate(prefab);
                    if (gObject != null)
                    {
                        gObject.transform.parent = container.transform;
                        gObject.transform.localPosition = item.Position;
                        gObject.transform.localRotation = item.Rotation;
                        gObject.transform.localScale = item.Scale;
                    }
                    else
                    {
                        Debug.LogError("gObject is null. can not instantiate prefab with name = " + item.Prefab.name + ". skipping to next item...");
                    }
                }
            }
            //setting mission info
            MissionGoal = _loadedMission.MissionGoal;
            WindDirection = _loadedMission.WindDirection;
            WindPower = _loadedMission.WindPower;
            FireLimit = _loadedMission.FireLimit;
            IsKillOrderImportant = _loadedMission.IsKillOrderImportant;
            Timeout = _loadedMission.Timeout;
            HasVariableWind = _loadedMission.HasVariableWind;
            WindChangeDuration = _loadedMission.WindChangeDuration;
            _winReward = _loadedMission.WinReward;
            //HACK: Commented By ProSNY
            //_backgroundMusic = GameDataService.Instance.GetBackgroundMusicForChapter(currentChapter);
            //if (_backgroundMusic)
            //    TrixSoundManager.Instance.StartPlay(_backgroundMusic, Audio.AudioType.Music, true);
            var background = GameDataService.Instance.GetBackgroundForChapter(currentChapter);
            //clearing previous background objects
            Destroy(BackgroundGameObject.transform.GetChild(0).GetComponentInChildren<Transform>().gameObject);
            Instantiate(background, BackgroundGameObject.transform);
            //if kill order is important, we gonna fill KillList so we can check hot order based on that later.
            if (IsKillOrderImportant)
            {
                KillList = _enemyControllers.Where(e => e.KillOrder != -1).Select(e => new { order = e.GetOrder(), controller = e }).OrderBy(c => c.order).Select(e => e.controller).ToList();
            }
            //setting up wind conditions
            if (HasVariableWind)
            {
                var lastWindPower = WindPower;
                var initWindPower = WindPower;
                WindPower = 0.1f;
                //changing wind power using tween
                DOTween.Sequence().SetLoops(-1).SetEase(Ease.Linear).Append(DOTween.To(() => WindPower, x =>
                {
                    WindPower = x;
                    //we do this to optimize messaging between components. this way message will be sent on every 0.1 change of wind power
                    if (Mathf.Abs(WindPower - lastWindPower) > 0.1f)
                    {
                        lastWindPower = WindPower;
                        //sends message to GamePlayHudController to setup hud ui elements
                        SimpleMessaging.Instance.SendMessage(new MessageData<GameplayMessageData, string>
                        {
                            Message = new GameplayMessageData
                            {
                                GameplayMessageType = GameplayMessageType.WindChanged,
                                HasWind = WindPower > 0 && WindDirection != WindDirectionSprite.Url,
                                WindDirection = WindDirection,
                                WindPower = float.Parse(WindPower.ToString("F1"))
                            }
                        });
                    }
                }, initWindPower, WindChangeDuration)).Append(DOTween.To(() => WindPower, x => //going backward on wind power
                {
                    WindPower = x;
                    if (Mathf.Abs(WindPower - lastWindPower) > 0.1f)
                    {
                        lastWindPower = WindPower;
                        //sends message to GamePlayHudController to setup hud ui elements
                        SimpleMessaging.Instance.SendMessage(new MessageData<GameplayMessageData, string>
                        {
                            Message = new GameplayMessageData
                            {
                                GameplayMessageType = GameplayMessageType.WindChanged,
                                HasWind = WindPower > 0 && WindDirection != WindDirectionSprite.Url,
                                WindDirection = WindDirection,
                                WindPower = float.Parse(WindPower.ToString("F1"))
                            }
                        });
                    }
                }, 0.1f, WindChangeDuration));

            }
            //try to get weapon settings
            WeaponDataService.Instance.GetCurrentWeapon(w => { _currentWeapon = w; });
        }
        /// <summary>
        /// waits for specific time then does an action
        /// </summary>
        /// <param name="waitTime">time to wait (in seconds)</param>
        /// <param name="callback">action to execute after wait</param>
        /// <returns></returns>
        IEnumerator WaitAndDoAction(int waitTime, Action callback)
        {
            yield return new WaitForSeconds(waitTime);
            callback?.Invoke();
        }
        /// <summary>
        /// is called when user wins a level
        /// </summary>
        private void Win(EnemyController lastCharacterGotHit)
        {
            if (IsPaused || _levelFailed)
                return;

            //HACK: By ProSNY Stop Gameplay Sounds
            if (GameplayAudioController)
                GameplayAudioController.StopPlay();

            if (!lastCharacterGotHit.RunLeftOnHit && !lastCharacterGotHit.RunRightOnHit)
            {
                DOTween.KillAll();
                StopAllMovingCharacters();
            }
            GameManager.Instance.IsInGameplay = false;
            _missionProgress.MissedShots = _missionProgress.TotalShots - _missionProgress.TotalHits;
            GameManager.Instance.AddChapterProgress(_missionProgress); //to save users progress
            var chapterMissionCount = GameDataService.Instance.GetLevelsCount(_currentChapter); //checking that all levels of current chapter are finished
            if (_currentMission >= chapterMissionCount)
            {
                //chapter finished
                StartCoroutine(WaitAndDoAction(WaitTimeAfterLevelEnd, () =>
                {
                    DOTween.KillAll();
                    StopAllMovingCharacters();
                    GameManager.Instance.SaveEndChapter();
                    var gameplayMessageData = new GameplayMessageData
                    {
                        GameplayMessageType = GameplayMessageType.MissionEnded,
                        EndGameBoardType = BoardType.EndChapter,
                        Message = _loadedMission.WinMessage
                    };
                    //sends message to EndGameBoardController to show end game board
                    SimpleMessaging.Instance.SendMessage(new MessageData<ShowViewMessageData, string>
                    {
                        Command = StaticValues.ShowViewCommand,
                        Message = new ShowViewMessageData
                        {
                            ViewModelType = typeof(EndGameBoardsController),
                            ViewModelData = gameplayMessageData
                        }
                    });
                }));
            }
            else
            {
                //chapter not finished
                StartCoroutine(WaitAndDoAction(WaitTimeAfterLevelEnd, () =>
                {
                    GameManager.Instance.ChapterMission = new ChapterMission(_currentChapter, _currentMission + 1);
                    var gameplayMessageData = new GameplayMessageData
                    {
                        ChapterMissionInfo = new ChapterMission(_currentChapter, _currentMission),
                        GameplayMessageType = GameplayMessageType.MissionEnded,
                        EndGameBoardType = BoardType.Win,
                        Message = _loadedMission.WinMessage,
                        WinReward = _winReward
                    };
                    //sends message to EndGameBoardController to show end game board
                    SimpleMessaging.Instance.SendMessage(new MessageData<ShowViewMessageData, string>
                    {
                        Command = StaticValues.ShowViewCommand,
                        Message = new ShowViewMessageData
                        {
                            ViewModelType = typeof(EndGameBoardsController),
                            ViewModelData = gameplayMessageData
                        }
                    });
                }));
            }
        }
        /// <summary>
        /// is called when user loses a level
        /// </summary>
        private void Lose()
        {
            _levelFailed = true; //this flag is used in other methods
            DOTween.KillAll(); //stops every active tween
            if (IsPaused)
                return;
            StopAllMovingCharacters();
            _missionProgress.LoseCount++;
            //HACK: By ProSNY Stop Gameplay Sounds
            if (GameplayAudioController)
                GameplayAudioController.StopPlay();

            StartCoroutine(WaitAndDoAction(WaitTimeAfterLevelEnd, () =>
            {
                GameManager.Instance.AddChapterProgress(_missionProgress);
                GameManager.Instance.IsInGameplay = false;
                var gameplayMessageData = new GameplayMessageData
                {
                    GameplayMessageType = GameplayMessageType.MissionEnded,
                    EndGameBoardType = BoardType.Lose,
                    Message = _loadedMission.FailureMessage
                };
                //sends message to EndGameBoardController to show end game board
                SimpleMessaging.Instance.SendMessage(new MessageData<ShowViewMessageData, string>
                {
                    Command = StaticValues.ShowViewCommand,
                    Message = new ShowViewMessageData
                    {
                        ViewModelType = typeof(EndGameBoardsController),
                        ViewModelData = gameplayMessageData
                    }
                });
            }));
        }

        private void StopAllMovingCharacters()
        {
            //set all moving characters animation to stand
            _enemyControllers.Where(e =>
                    e.SkeletonAnimation.AnimationName != AnimationNames.Stand.ToString() &&
                    e.SkeletonAnimation.AnimationName != AnimationNames.Die.ToString())
                .ForEach(e => e.SkeletonAnimation.PlayAnimation(e, AnimationNames.Stand));
        }

        private void Awake()
        {
            if (IsPaused)
                return;

            //HACK: By ProSNY
            if (!GameplayAudioController)
                GameplayAudioController = GetComponent<GameplayAudioController>();

            SimpleMessaging.Instance.Register<GameplayMessageData>(this, m =>
            {
                //receives message from GamePlayHudController to pause the game
                if (m.Message.GameplayMessageType == GameplayMessageType.GamePaused)
                {
                    Pause();
                    _enemyControllers.ForEach(e => e.Pause());
                }
                //receives message from EndGameBoardController to resume the game
                else if (m.Message.GameplayMessageType == GameplayMessageType.GameResumed)
                {
                    Resume();
                    _enemyControllers.ForEach(e => e.Resume());
                }
            });

            SimpleMessaging.Instance.Register<GameplayMessageData>(this, m =>
            {
                //receives message from MissionDescriptionController to load a level
                if (m.Message.GameplayMessageType == GameplayMessageType.MissionStarted)
                {
                    //on first start of game (chapter 1 and level 1), we gonna get welcome achievement from google (idk why, ask Sina :D)
                    if (GameManager.Instance.ChapterMission.ChapterNumber == 1 &&
                        GameManager.Instance.ChapterMission.MissionNumber == 1)
                    {
                        GooglePlayController.Instance.WelcomeAchievement((result) => { });
                    }
                    LoadLevel(GameManager.Instance.ChapterMission.ChapterNumber,
                        GameManager.Instance.ChapterMission.MissionNumber);
                    GameManager.Instance.IsInGameplay = true;
                    //HACK: By ProSNY Add if(Timeout > 0){...}
                    //check if Timeout is more than zero, set the mission time limit
                    if (Timeout > 0)
                    {
                        //setting mission end time so we can check level timeout later
                        _missionEndTime = TimeController.Instance.GamePlayTime.AddSeconds(Timeout);
                        //checking fot level timeout
                        TimeController.Instance.TimeChangedEvent.AddListener(() =>
                        {
                            var timeLeft = _missionEndTime - TimeController.Instance.GamePlayTime;
                            if (timeLeft <= TimeSpan.Zero && GameManager.Instance.IsInGameplay)
                            {
                                if (!_levelFailed) //if user is already lost, don't lose!
                                    Lose();
                            }
                        });
                    }
                    //sends message to GamePlayHudController to setup hud ui elements
                    SimpleMessaging.Instance.SendMessage(new MessageData<GameplayMessageData, string>
                    {
                        Message = new GameplayMessageData
                        {
                            GameplayMessageType = GameplayMessageType.MissionStarted,
                            FireLimit = FireLimit,
                            Timeout = Timeout,
                            HasWind = WindPower > 0 && WindDirection != WindDirectionSprite.Url,
                            WindDirection = WindDirection,
                            WindPower = WindPower
                        }
                    });
                }

            }, StaticValues.MissionController);
        }

        public override void OnDestroy()
        {
            SimpleMessaging.Instance.UnRegister<GameplayMessageData>(this);
            base.OnDestroy();
        }

        /// <summary>
        /// is called when an enemy character killed.
        /// </summary>
        /// <param name="enemy">reference to enemy that is killed</param>
        public void EnemyKilled(EnemyController enemy)
        {
            if (IsPaused || _levelFailed)
                return;
            if (!HasEnemyKilledInCorrectOrder(enemy))
            {
                Lose();
                return;
            }
            _missionProgress.EnemiesKilled++;
            if (CanNotWin()) //checks if all the characters that can be killed, are killed or not
            {
                if (HasFireLimitReached())
                    Lose();
                return;
            }
            Win(enemy);
        }
        /// <summary>
        /// checks that if enemy is killed in correct order.
        /// has no effect if IsKillOrderImportant is false.
        /// </summary>
        /// <param name="enemy">reference to enemy character that is killed</param>
        /// <returns></returns>
        private bool HasEnemyKilledInCorrectOrder(EnemyController enemy)
        {
            if (_levelFailed)
                return false;
            if (!IsKillOrderImportant)
                return true;
            //we get number of enemies killed so far the compare currently killed enemy's order with that.
            var killedSoFar = _enemyControllers.Count(e => e.IsDead && !e.CanNotGetHit);
            return enemy.GetOrder() == killedSoFar - 1;
        }
        /// <summary>
        /// checks if all the characters that can be killed, are killed or not
        /// </summary>
        /// <returns>true if there are still enemies that should be killed, otherwise returns false</returns>
        private bool CanNotWin()
        {
            return _enemyControllers.Any(enemy => !enemy.IsDead && !enemy.CanNotGetHit);
        }

        /// <summary>
        /// is called when an innocent character is hit
        /// </summary>
        /// <param name="character">reference to the character that is hit</param>
        public void InnocentKilled(EnemyController character)
        {
            if (IsPaused || _levelFailed)
                return;
            _missionProgress.InnocentCasualties++;
            Lose();
        }
        /// <summary>
        /// is called when user fires a shot, no matter it's a hit or a miss.
        /// </summary>
        /// <param name="aimedPosition">the position that shot is aimed to</param>
        public void ShotFired(Vector3 aimedPosition)
        {
            if (IsPaused || _levelFailed)
                return;
            TrixResource.AudioClips.Shoot_async(shootSfx =>
            {
                if (shootSfx)
                { 
                     TrixSoundManager.Instance.StartPlay(shootSfx, Audio.AudioType.UiSound);
                }
            });
            _missionProgress.TotalShots++;
            _currentMissionTotalShots++;
            //sends message to GamePlayHudController to update fire limit on ui
            SimpleMessaging.Instance.SendMessage(new MessageData<GameplayMessageData, string>
            {
                Message = new GameplayMessageData()
                {
                    GameplayMessageType = GameplayMessageType.ShotFired,
                    FireLimit = FireLimit - _currentMissionTotalShots
                }
            });
            //send message to GamePlayHudController to hide cancel area ui
            SimpleMessaging.Instance.SendMessage(new MessageData<GameplayMessageData, string>
            {
                Message = new GameplayMessageData()
                {
                    GameplayMessageType = GameplayMessageType.AimEnded,
                }
            }, typeof(GameplayHudController).ToString());
            //shows shoot effect particles
            if (ShootEffect)
            {
                ShootEffect.transform.position = aimedPosition;
                ShootEffect.Play();
                //ShootSoundEffect.StartPlay();
            }
        }
        /// <summary>
        /// is called when an enemy character is hit
        /// </summary>
        /// <param name="target">reference to enemy character that is hit</param>
        public void TargetHit(EnemyController target)
        {
            if (IsPaused || _levelFailed)
                return;
            _missionProgress.TotalHits++;

        }
        /// <summary>
        /// is called when a shot is missed.
        /// when user misses a shot, enemies with RunLeft or RunRight set to true, will run
        /// </summary>
        public void ShotMissed()
        {
            if (IsPaused || _levelFailed)
                return;
            if (HasFireLimitReached())
                Lose();
            if (_currentWeapon != null && _currentWeapon.Silenced)
                return;
            var alreadyLost = false;
            //checking that should enemy run or not
            _enemyControllers.Where(e => !e.IsDead).ForEach(e =>
            {
                if (e.RunLeft || e.RunRight)
                {
                    e.Run(enemy =>
                    {
                        if (alreadyLost) return; //if user has already lost, don't lose again!
                        alreadyLost = true;
                        Lose();
                    });
                }
            });
        }
        /// <summary>
        /// is called when user reaches fire limit
        /// </summary>
        /// <returns>true if fire limit has reached</returns>
        private bool HasFireLimitReached()
        {
            return FireLimit > 0 && FireLimit == _currentMissionTotalShots && CanNotWin();
        }
        /// <summary>
        /// is called when user cancels a shot
        /// </summary>
        public void ShotCanceled()
        {
            if (IsPaused)
                return;
            //send message to GamePlayHudController to hide cancel area ui
            SimpleMessaging.Instance.SendMessage(new MessageData<GameplayMessageData, string>
            {
                Message = new GameplayMessageData()
                {
                    GameplayMessageType = GameplayMessageType.AimEnded,
                }
            }, typeof(GameplayHudController).ToString());
        }
        /// <summary>
        /// is called when user starts aiming
        /// </summary>
        public void AimingStarted()
        {
            if (IsPaused)
                return;
            //send message to GamePlayHudController to show cancel area ui
            SimpleMessaging.Instance.SendMessage(new MessageData<GameplayMessageData, string>
            {
                Message = new GameplayMessageData()
                {
                    GameplayMessageType = GameplayMessageType.AimStarted,
                }
            }, typeof(GameplayHudController).ToString());
        }
        /// <summary>
        /// is called when user pauses the game
        /// </summary>
        public void Pause()
        {
            IsPaused = true;
            GameManager.Instance.IsPaused = true;
        }
        /// <summary>
        /// is called when user resumes the game
        /// </summary>
        public void Resume()
        {
            IsPaused = false;
            GameManager.Instance.IsPaused = false;
        }
        /// <summary>
        /// gets user's current weapon
        /// </summary>
        /// <returns>user's currently selected weapon</returns>
        public Weapon GetCurrentWeapon()
        {
            return _currentWeapon;
        }
    }
}
