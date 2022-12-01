using System.Collections.Generic;
using Models;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapons", menuName = "Datasets/WeaponsDataSet", order = 2)]
    public class WeaponsDataSet : ScriptableObject
    {
        public List<Weapon> Weapons;
    }
}
