using System.Collections.Generic;
using Models;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Packages", menuName = "Datasets/Packages", order = 4)]
    public class PackageDataSet : ScriptableObject
    {
        public List<Package> Packages;
    }
}
