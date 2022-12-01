using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class AdvertiseProviderKey
    {
        [SerializeField] private AdvertiseProvider _advertiseProvider;
        [TextArea(3, 5)] [SerializeField] private string _key;
        public AdvertiseProvider AdvertiseProvider
        {
            get { return _advertiseProvider; }
            set { _advertiseProvider = value; }
        }
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
    }
}
