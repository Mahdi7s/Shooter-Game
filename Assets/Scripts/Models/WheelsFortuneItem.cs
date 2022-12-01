using Models.Constants;
using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class WheelsFortuneItem
    {
        [SerializeField] private int _itemId;
        [SerializeField] private WheelsFortuneRewardType _rewardType;
        [SerializeField] private int _itemAmount;

        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }
        public WheelsFortuneRewardType RewardType
        {
            get { return _rewardType; }
            set { _rewardType = value; }
        }
        public int ItemAmount
        {
            get { return _itemAmount; }
            set { _itemAmount = value; }
        }
    }
}
