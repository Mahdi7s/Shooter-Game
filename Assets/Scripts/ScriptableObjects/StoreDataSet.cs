using System.Collections.Generic;
using Models;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Store", menuName = "Datasets/StoreDataSet", order = 6)]
    public class StoreDataSet : ScriptableObject
    {
        public List<StoreItem> StoreItems;
    }
}
