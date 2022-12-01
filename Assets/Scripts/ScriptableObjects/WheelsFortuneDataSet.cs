using System.Collections.Generic;
using Models;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "WheelsFortune", menuName = "Datasets/WheelsFortune", order = 5)]
    public class WheelsFortuneDataSet : ScriptableObject
    {
        public List<WheelsFortuneItem> WheelsFortuneItems;
    }
}
