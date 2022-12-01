using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class PlayerWeapon
    {
        [SerializeField] private int _weaponId;
        public int WeaponId
        {
            get { return _weaponId; }
            set { _weaponId = value; }
        }

    }
}
