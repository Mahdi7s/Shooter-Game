using Models.Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Mission
    {
        [SerializeField] private List<Enemy> _enemies;
        [SerializeField] private List<SceneItem> _items;
        [SerializeField] private int _chapter;
        [SerializeField] private int _missionNum;
        [SerializeField] private string _missionGoal;
        [SerializeField] private bool _isKillOrderImportant;
//        [SerializeField] private List<Enemy> _killOrder;
        [SerializeField] private int _fireLimit;
        [SerializeField] private int _timeout;
        [SerializeField] private WindDirectionSprite _windDirection;
        [SerializeField] private float _windPower;
        [SerializeField] private string _winMessage;
        [SerializeField] private Sprite _startImage;
        [SerializeField] private Sprite _winImage;
        [SerializeField] private string _failureMessage;
        [SerializeField] private Sprite _failureImage;
        [SerializeField] private string _achievementDescription;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private bool _hasVariableWind;
        [SerializeField] private float _windChangeDuration;
        //[SerializeField] private GameObject _background;
        [SerializeField] private int _winReward;

        public int WinReward
        {
            get { return _winReward; }
            set { _winReward = value; }
        }

        //public GameObject Background
        //{
        //    get { return _background; }
        //    set { _background = value; }
        //}
        public bool HasVariableWind
        {
            get { return _hasVariableWind; }
            set { _hasVariableWind = value; }
        }
        public float WindChangeDuration
        {
            get { return _windChangeDuration; }
            set { _windChangeDuration = value; }
        }
        public string AchievementDescription
        {
            get { return _achievementDescription; }
            set { _achievementDescription = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

//        public List<Enemy> KillOrder
//        {
//            get { return _killOrder; }
//            set { _killOrder = value; }
//        }
    
        public int Chapter
        {
            get { return _chapter; }
            set { _chapter = value; }
        }

        public int MissionNum
        {
            get { return _missionNum; }
            set { _missionNum = value; }
        }

        public string MissionGoal
        {
            get { return _missionGoal; }
            set { _missionGoal = value; }
        }

        public bool IsKillOrderImportant
        {
            get { return _isKillOrderImportant; }
            set { _isKillOrderImportant = value; }
        }

        public int FireLimit
        {
            get { return _fireLimit; }
            set { _fireLimit = value; }
        }

        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public WindDirectionSprite WindDirection
        {
            get { return _windDirection; }
            set { _windDirection = value; }
        }
        public float WindPower
        {
            get { return _windPower; }
            set { _windPower = value; }
        }

        public string WinMessage
        {
            get { return _winMessage; }
            set { _winMessage = value; }
        }

        public Sprite StartImageName
        {
            get { return _startImage; }
            set { _startImage = value; }
        }

        public Sprite WinImageName
        {
            get { return _winImage; }
            set { _winImage = value; }
        }

        public string FailureMessage
        {
            get { return _failureMessage; }
            set { _failureMessage = value; }
        }

        public Sprite FailureImage
        {
            get { return _failureImage; }
            set { _failureImage = value; }
        }

        public void SetLists(List<Enemy> enemies, List<SceneItem> items)
        {
            _enemies = enemies;
            _items = items;
        }

        public List<Enemy> GetEnemies()
        {
            return _enemies;
        }
        public List<SceneItem> GetSceneItems()
        {
            return _items;
        }
    }
}
