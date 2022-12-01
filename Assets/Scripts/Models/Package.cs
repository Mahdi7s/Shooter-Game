using System;
using Models.Constants;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Package
    {
        [SerializeField] private PackageType _packageType;
        [SerializeField] private string _sku;
        [SerializeField] private bool _isConsumable;

        public PackageType PackageType
        {
            get { return _packageType; }
            set { _packageType = value; }
        }
        public string Sku
        {
            get { return _sku; }
            set { _sku = value; }
        }
        public bool IsConsumable
        {
            get { return _isConsumable; }
            set { _isConsumable = value; }
        }
    }
}
