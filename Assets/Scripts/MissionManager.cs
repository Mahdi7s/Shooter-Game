using DataServices;
using Infrastructure;
using Models;
using Models.Constants;
using Sirenix.OdinInspector;
using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
/// <summary>
/// this class is handling all stuff about level designing, loading, saving and ...
/// </summary>
[CustomEditor(typeof(GameObject))]
public class MissionManager : SerializedMonoBehaviour
{

    [BoxGroup("Main Configs")]
    public GameDataService DateService; //reference to repository layer

    [BoxGroup("Loaded Chapter And Mission")]
    [InlineButton("LastMission", ">>")]
    [InlineButton("PostMission", ">")]
    [InlineButton("PreMission", "<")]
    [InlineButton("FirstMission", "<<")]
    [HideLabel]
    [DisplayAsString]
    public string ChapterAndMission = "Chapter: 1 / Mission: 1";

    [BoxGroup("Loaded Chapter And Mission")]
    public int chapter;

    [BoxGroup("Loaded Chapter And Mission")]
    public int mission;

    [Button("Load Mission", ButtonSizes.Medium)]
    [BoxGroup("Loaded Chapter And Mission")]
    public void GoToChapterMission()
    {
        LoadMission();
    }

    private void FirstMission()
    {
        chapter = 1;
        mission = 1;
        UpdateChapterAndMissionLabels();
        LoadMission();
    }

    private void PreMission()
    {
        mission--;
        if (mission <= 0)
        {
            mission = 1;
            chapter--;
            if (chapter <= 0)
                chapter = 1;
        }

        UpdateChapterAndMissionLabels();
        LoadMission();
    }
    private void PostMission()
    {
        mission++;
        UpdateChapterAndMissionLabels();
        LoadMission();
    }

    private void LastMission()
    {
        var lastChapter = GameDataService.Instance.LoadDataWhere<Mission>().Max(m => m.Chapter);
        var lastMission = GameDataService.Instance.LoadDataWhere<Mission>(m => m.Chapter == lastChapter).Max(m => m.MissionNum);
        if (lastChapter > 0)
            chapter = lastChapter;
        if (lastMission > 0)
            mission = lastMission;
        UpdateChapterAndMissionLabels();
        LoadMission();
    }
    private void UpdateChapterAndMissionLabels()
    {
        ChapterAndMission =
            $"Chapter: {chapter}({GameDataService.Instance.MissionsConfig.Chapters.SingleOrDefault(c => c.Number == chapter)?.Title}) / Mission: {mission}";
    }
    [BoxGroup("Mission Info")]
    public int WinReward; //number of coins that user will get as reward after winning this level
    [BoxGroup("Mission Info")]
    [MultiLineProperty]
    [HideLabel]
    [Title("Mission Title")]
    public string MissionTitle;

    [HideLabel]
    [MultiLineProperty]
    [Title("Mission Goal")]
    [BoxGroup("Mission Info")]
    public string MissionGoal = string.Empty; //what user should do to pass this level

    [BoxGroup("Mission Info")]
    [HideLabel]
    [MultiLineProperty]
    [Title("Mission Description")]
    public string MissionDescription;

    [BoxGroup("Mission Info")]
    [HideLabel]
    [MultiLineProperty]
    [Title("Mission Achievement Description")]
    public string AchievementDescription;

    [BoxGroup("Mission Info")]
    [Title("Failure Message")]
    [MultiLineProperty]
    [HideLabel]
    public string FailureMessage = string.Empty;

    [BoxGroup("Mission Info")]
    [Title("Win Message")]
    [MultiLineProperty]
    [HideLabel]
    public string WinMessage = string.Empty;

    [BoxGroup("Mission Info")]
    public Sprite StartSpritePrefab;
    [BoxGroup("Mission Info")]
    public Sprite FailureSpritePrefab;
    [BoxGroup("Mission Info")]
    public Sprite WinSpritePrefab;
    [BoxGroup("Win Conditions")]
    [InlineButton("RefillKillList", "Refill List")]
    public bool IsKillOrderImportant = false;
    [BoxGroup("Win Conditions")]
    public List<EnemyController> KillList = new List<EnemyController>();
    [BoxGroup("Win Conditions")]
    public int FireLimit = 0;
    [BoxGroup("Win Conditions")]
    [SuffixLabel("seconds", Overlay = false)]
    [Range(0, 60)]
    public int Timeout = 0; //the time (in seconds) that user should finish this level before that

    [BoxGroup("Wind Options")]
    [Range(0, 3)]
    public float WindPower = 0;

    [BoxGroup("Wind Options")]
    public WindDirectionSprite WindDirection = WindDirectionSprite.spr_East;

    [BoxGroup("Wind Options")] public bool HasVariableWind = false;
    [BoxGroup("Wind Options")]
    [Tooltip("Will change wind power between 0 and WindPower")]
    [SuffixLabel("seconds", Overlay = false)]
    public float WindChangeDuration; //specifies how fast the wind power will change

    private List<EnemyController> _enemyControllers;

    public void RefillKillList()
    {
        var enemies = GameObject.FindObjectsOfType<EnemyController>();
    }
    /// <summary>
    /// saves current mission in scriptable object
    /// </summary>
    [Button(ButtonSizes.Large)]
    [GUIColor(0, 1, 0)]
    public void SaveMission()
    {
        var container = GameObject.FindWithTag("GameContainer"); //all characters are inside this GameObject
        var items = container.GetComponentsInChildren<ItemController>();
        _enemyControllers = new List<EnemyController>();
        var sceneItems = new List<SceneItem>();
        var enemies = new List<Enemy>();
        if (items.Length != KillList.Count)
        {
#if UNITY_EDITOR
            Debug.LogWarning("enemies count != kill list count!");
#endif
        }
        //setting up characters and way points
        foreach (var item in items)
        {
            var prefab = PrefabUtility.GetPrefabParent(item.gameObject);
            var enemy = item as EnemyController;
            if (enemy != null)
            {
                var order = KillList.IndexOf(enemy);
                var newEnemy = new Enemy
                {
                    Name = enemy.gameObject.name,
                    Prefab = prefab as GameObject,
                    Position = enemy.transform.localPosition,
                    Rotation = enemy.transform.localRotation,
                    Scale = enemy.transform.localScale,
                    KillOrder = order,
                    WayPoints = enemy.WalkPoints.Select(x => x.ToWayPoint()).ToList(),
                    IsRecursive = enemy.IsRecursive,
                    ActivateIn = enemy.ActivateIn,
                    RunLeft = enemy.RunLeft,
                    RunRight = enemy.RunRight,
                    Health = enemy.Health,
                    SAnimation = enemy.SkeletonAnimation,
                    Speed = enemy.Speed,
                    CanNotGetHit = enemy.CanNotGetHit,
                    OrderInLayer = enemy.OrderInLayer,
                    CharacterType = enemy.CharacterType,
                    TeasingType = enemy.TeasingType,
                    TeasingAnimation = enemy.TeasingAnimation,
                    GotTeasedAnimation = enemy.GotTeasedAnimation,
                    RunRightOnHit = enemy.RunRightOnHit,
                    RunLeftOnHit = enemy.RunLeftOnHit,
                    RunSpeed = enemy.RunSpeed,
                    StayPutAfterGotTeased = enemy.StayPutAfterGotTeased,

                    Id = !string.IsNullOrEmpty(enemy.Id) && enemy.Id != Guid.Empty.ToString()
                        ? enemy.Id
                        : Guid.NewGuid().ToString(),
                };
                enemies.Add(newEnemy);
                enemy.Id = newEnemy.Id;
                _enemyControllers.Add(enemy);
            }

            else
            {
                sceneItems.Add(new SceneItem
                {
                    Name = item.gameObject.name,
                    Prefab = prefab as GameObject,
                    Position = item.transform.localPosition,
                    Rotation = item.transform.localRotation,
                    Scale = item.transform.localScale
                });
            }
        }
        //setting up teasing targets
        enemies.ForEach(e =>
            {
                e.TeasingTargets = _enemyControllers.SingleOrDefault(ec => ec.Id == e.Id)?.TeasingTargets
                    .Select(t => t.Id).ToList();
            });
        //creating new mission and setting it up
        var newMission = new Mission
        {
            MissionGoal = MissionGoal,
            Description = MissionDescription,
            Title = MissionTitle,
            AchievementDescription = AchievementDescription,
            Chapter = chapter,
            MissionNum = this.mission,
            FireLimit = FireLimit,
            Timeout = Timeout,
            WindPower = WindPower,
            WindDirection = WindDirection,
            IsKillOrderImportant = IsKillOrderImportant,
            FailureImage = FailureSpritePrefab,
            WinMessage = WinMessage,
            FailureMessage = FailureMessage,
            HasVariableWind = HasVariableWind,
            WindChangeDuration = WindChangeDuration,
            //Background = GameDataService.Instance.GetBackgroundForChapter(chapter),
            WinReward = WinReward
        };
        newMission.SetLists(enemies, sceneItems); //assigning mission's enemies and scene items
        GameDataService.Instance.InsertOrUpdateMission(newMission);
        EditorUtility.SetDirty(GameDataService.Instance.MissionsConfig); //sets scriptable object as dirty so unity can save it.
    }
    /// <summary>
    /// loads the level specified by chapter and mission properties
    /// </summary>
    private void LoadMission()
    {
        var loadedMission = GameDataService.Instance.GetMission(chapter, mission);
        if (loadedMission == null)
        {
#if UNITY_EDITOR
            Debug.LogError("mission not found. chapter: " + chapter.ToString() + ", level: " + mission.ToString());
#endif
            return;
        }
        var container = GameObject.FindWithTag("GameContainer"); //all characters are inside this GameObject
        //clearing scene before loading new level's characters
        container.GetComponentsInChildren<EnemyController>().ToList().ForEach(e =>
        {
            DestroyImmediate(e.gameObject);
        });
        var enemies = loadedMission.GetEnemies();
        var enemyControllers = new List<EnemyController>();
        //setting up characters and their stuff
        foreach (var item in enemies)
        {
            if (item.Prefab != null)
            {
                var prefab = ResourceManager.LoadResource<GameObject>($"Characters/{item.Prefab.name}");
                var gObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                if (gObject != null)
                {
                    gObject.transform.parent = container.transform;
                    gObject.transform.localPosition = item.Position;
                    gObject.transform.localRotation = item.Rotation;
                    gObject.transform.localScale = item.Scale;
                    // ReSharper disable once NotAccessedVariable
                    var sAnimationController = gObject.GetComponent<SkeletonAnimation>(); 
                    sAnimationController = item.SAnimation; //this is for setting skeleton animation on newly created gObject. don't mess with it ;)
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
                    enemyController.KillOrder = item.KillOrder;
                    enemyController.Id = item.Id;
                    enemyController.RunLeftOnHit = item.RunLeftOnHit;
                    enemyController.RunRightOnHit = item.RunRightOnHit;
                    enemyController.RunSpeed = item.RunSpeed;
                    enemyController.StayPutAfterGotTeased = item.StayPutAfterGotTeased;
                    //setting order in layer for character (for design time)
                    var meshRenderer = gObject.GetComponent<MeshRenderer>();
                    meshRenderer.sortingOrder = item.OrderInLayer;
                    //setting up way points
                    item.WayPoints?.ForEach(w =>
                    {
                        var wayPointObject = ResourceManager.LoadResource<GameObject>("Waypoint");
                        wayPointObject = PrefabUtility.InstantiatePrefab(wayPointObject) as GameObject;
                        if (wayPointObject == null)
                        {
                            Debug.LogError("wayPointObject is null. loading failed.");
                            return;
                        }
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
                    enemyControllers.Add(enemyController);
                }
            }
        }
        //setting teasing targets
        foreach (var enemy in enemies)
        {
            var enemyController = enemyControllers.SingleOrDefault(e => e.Id == enemy.Id);
            if (enemyController != null)
            {
                var targets = enemyControllers.Where(e => enemy.TeasingTargets.Contains(e.Id));
                enemyController.TeasingTargets.AddRange(targets);
            }
        }
        foreach (var item in loadedMission.GetSceneItems())
        {
            if (item.Prefab != null)
            {
                var prefab = ResourceManager.LoadResource<GameObject>(item.Prefab.name);
                var gObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                if (gObject == null)
                {
                    Debug.LogError("gObject is null. loading failed.");
                    return;
                }
                gObject.transform.parent = container.transform;
                gObject.transform.localPosition = item.Position;
                gObject.transform.localRotation = item.Rotation;
                gObject.transform.localScale = item.Scale;
            }
        }
        chapter = loadedMission.Chapter;
        mission = loadedMission.MissionNum;
        MissionGoal = loadedMission.MissionGoal;
        MissionDescription = loadedMission.Description;
        MissionTitle = loadedMission.Title;
        AchievementDescription = loadedMission.AchievementDescription;
        WindPower = loadedMission.WindPower;
        WindDirection = loadedMission.WindDirection;
        FireLimit = loadedMission.FireLimit;
        Timeout = loadedMission.Timeout;
        IsKillOrderImportant = loadedMission.IsKillOrderImportant;
        WinMessage = loadedMission.WinMessage;
        FailureMessage = loadedMission.FailureMessage;
        FailureSpritePrefab = loadedMission.FailureImage;
        HasVariableWind = loadedMission.HasVariableWind;
        WindChangeDuration = loadedMission.WindChangeDuration;
        WinReward = loadedMission.WinReward;
        //setting level background
        var bg = GameObject.FindWithTag("Background");
        if (bg)
        {
            var bgPrefab =
                PrefabUtility.InstantiatePrefab(GameDataService.Instance.GetBackgroundForChapter(chapter)) as GameObject;
            if (bgPrefab)
            {
                var child = bg.transform.GetChild(0).GetComponentInChildren<Transform>();
                DestroyImmediate(child.gameObject); //clearing previous background
                bgPrefab.transform.parent = bg.transform;
            }
        }
        //if kill order is important, fill KillList in correct order
        if (IsKillOrderImportant)
        {
            KillList = enemyControllers.Where(e => e.KillOrder != -1).Select(e => new { order = e.GetOrder(), controller = e }).OrderBy(c => c.order).Select(e => e.controller).ToList();
        }
        UpdateChapterAndMissionLabels();
    }

    [Button(ButtonSizes.Large)]
    [GUIColor(51 * 0.0039f, 204 * 0.0039f, 51 * 0.0039f)]
    public void SaveAndGoToNextMission()
    {
        SaveMission();
        mission++;
    }

    [Button(ButtonSizes.Large)]
    [GUIColor(51 * 0.0039f, 153 * 0.0039f, 255 * 0.0039f)]
    public void SaveAndGoToNextChapter()
    {
        SaveMission();
        mission = 1;
        chapter++;
    }
}
#endif