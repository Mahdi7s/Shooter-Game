using System;
using System.Collections.Generic;
using Models.Constants;
using Spine.Unity;
using UnityEngine;

namespace Models
{
    /// <summary>
    /// keeps enemies data in a scriptable object
    /// </summary>
    [Serializable]
    public class Enemy : SceneItem
    {
        [SerializeField] private int _killOrder;
        [SerializeField] private SkeletonAnimation _sAnimation;
        [SerializeField] private List<WayPoint> _wayPoints;
        [SerializeField] private bool _isRecursive;
        [SerializeField] private int _activateIn;
        [SerializeField] private bool _runLeft;
        [SerializeField] private bool _runRight;
        private int _timeToArrive;
        [SerializeField] private int _health;
        [SerializeField] private float _speed;
        [SerializeField] private bool _canNotGetHit;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private TeasingType _teasingType;
        [SerializeField] private AnimationNames _teasingAnimation;
        [SerializeField] private AnimationNames _gotTeasedAnimation;
        [SerializeField] private GameObject _teasingThrowableObject;
        [SerializeField] private List<string> _teasingTargetIds;
        [SerializeField] private bool _runLeftOnHit;
        [SerializeField] private bool _runRightOnHit;
        [SerializeField] private float _runSpeed;
        [SerializeField] private bool _stayPutAfterGotTeased;

        public bool StayPutAfterGotTeased
        {
            get { return _stayPutAfterGotTeased; }
            set { _stayPutAfterGotTeased = value; }
        }

        public float RunSpeed
        {
            get { return _runSpeed; }
            set { _runSpeed = value; }
        }

        public bool RunRightOnHit
        {
            get { return _runRightOnHit; }
            set { _runRightOnHit = value; }
        }

        public bool RunLeftOnHit
        {
            get { return _runLeftOnHit; }
            set { _runLeftOnHit = value; }
        }
        /// <summary>
        /// holds Ids of target characters
        /// </summary>
        public List<string> TeasingTargets
        {
            get { return _teasingTargetIds; }
            set { _teasingTargetIds = value; }
        }

        public GameObject TeasingThrowableObject
        {
            get { return _teasingThrowableObject; }
            set { _teasingThrowableObject = value; }
        }

        public AnimationNames GotTeasedAnimation
        {
            get { return _gotTeasedAnimation; }
            set { _gotTeasedAnimation = value; }
        }

        public AnimationNames TeasingAnimation
        {
            get { return _teasingAnimation; }
            set { _teasingAnimation = value; }
        }

        public TeasingType TeasingType
        {
            get { return _teasingType; }
            set { _teasingType = value; }
        }

        public CharacterType CharacterType
        {
            get { return _characterType; }
            set { _characterType = value; }
        }

        public bool CanNotGetHit
        {
            get { return _canNotGetHit; }
            set { _canNotGetHit = value; }
        }

        public int KillOrder
        {
            get { return _killOrder; }
            set { _killOrder = value; }
        }


        public SkeletonAnimation SAnimation
        {
            get { return _sAnimation; }
            set { _sAnimation = value; }
        }

        public List<WayPoint> WayPoints
        {
            get { return _wayPoints; }
            set { _wayPoints = value; }
        }

        public bool IsRecursive
        {
            get { return _isRecursive; }
            set { _isRecursive = value; }
        }
        /// <summary>
        /// character will start to move on way points after waits for ActivateIn seconds.
        /// </summary>
        public int ActivateIn
        {
            get { return _activateIn; }
            set { _activateIn = value; }
        }
        /// <summary>
        /// if true, character will run left if there is a missed shot
        /// </summary>
        public bool RunLeft
        {
            get { return _runLeft; }
            set { _runLeft = value; }
        }
        /// <summary>
        /// if true, character will run right if there is a missed shot
        /// </summary>
        public bool RunRight
        {
            get { return _runRight; }
            set { _runRight = value; }
        }

        public int TimeToArrive //reserved for future uses
        {
            get { return _timeToArrive; }
            set { _timeToArrive = value; }
        }

        public int Health //reserved for future uses
        {
            get { return _health; }
            set { _health = value; }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Enemy()
        {
            if (string.IsNullOrEmpty(Id) || Id == Guid.Empty.ToString())
            {
                Id = Guid.NewGuid().ToString();
            }
        }
    }
}
