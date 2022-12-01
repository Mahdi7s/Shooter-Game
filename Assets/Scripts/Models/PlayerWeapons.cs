using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class PlayerWeapons
    {
        [SerializeField] private List<PlayerWeapon> _playerWeaponsList = new List<PlayerWeapon>();
        [SerializeField] private int _equippedWeaponId;
        public List<PlayerWeapon> PlayerWeaponsList
        {
            get { return _playerWeaponsList; }
            set { _playerWeaponsList = value; }
        }
        public int EquippedWeaponId
        {
            get { return _equippedWeaponId; }
            set { _equippedWeaponId = value; }
        }
    }
}
