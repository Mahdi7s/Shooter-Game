using System.Collections.Generic;
using Models;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Advertises", menuName = "Datasets/Advertises", order = 3)]
    public class AdvertisesDataSet : ScriptableObject
    {
        public List<AdvertiseProviderKey> AdvertiseProviderKeys;
        public List<StatisticAdvertise> AdvertiseProviders;
    }
}
