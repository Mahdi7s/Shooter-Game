using System;
using Models.Constants;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class StatisticAdvertise
    {
        [SerializeField] private AdvertiseProvider _advertiseProvider;
        [SerializeField] private AdvertiseType _advertiseType;
        [SerializeField] private AdvertiseAction _advertiseAction;
        [SerializeField] private string _unitCode;
        private long _cooldown;
        [SerializeField] private int _advertiseOrder;
        [SerializeField] private bool _isActive;
        private int _prize;
        private int _dailyViewLimit;

        public AdvertiseProvider AdvertiseProvider
        {
            get { return _advertiseProvider; }
            set { _advertiseProvider = value; }
        }
        public AdvertiseType AdvertiseType
        {
            get { return _advertiseType; }
            set { _advertiseType = value; }
        }

        public AdvertiseAction AdvertiseAction
        {
            get { return _advertiseAction; }
            set { _advertiseAction = value; }
        }

        public string UnitCode
        {
            get { return _unitCode; }
            set { _unitCode = value; }
        }
        public long Cooldown
        {
            get { return _cooldown; }
            set { _cooldown = value; }
        }
        public int AdvertiseOrder
        {
            get { return _advertiseOrder; }
            set { _advertiseOrder = value; }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        public int Prize
        {
            get { return _prize; }
            set { _prize = value; }
        }
        public int DailyViewLimit
        {
            get { return _dailyViewLimit; }
            set { _dailyViewLimit = value; }
        }
    }
    public enum AdvertiseProvider
    {
        Tapligh = 1,
        Tapsell = 2
    }
    public enum AdvertiseType
    {
        Video = 1,
        FullBanner = 2,
        NativeBanner = 3
    }
}