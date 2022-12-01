using System;
using Models.Constants;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class StoreItem
    {
        [SerializeField] private int _id;
        [SerializeField] private int _order;
        [SerializeField] private int _coinAmount;
        [SerializeField] private int _cost;
        [SerializeField] private PackageType _packageType;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }
        public int CoinAmount
        {
            get { return _coinAmount; }
            set { _coinAmount = value; }
        }
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public PackageType PackageType
        {
            get { return _packageType; }
            set { _packageType = value; }
        }
    }
}
