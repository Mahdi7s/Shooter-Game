using System;
using Models.Constants;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Weapon
    {
        //ProTODO: Add Achievement (For Unlock) Enums

        [SerializeField] private int _weaponId;
        [SerializeField] private string _weaponName;
        [SerializeField] private string _weaponDescription;
        [SerializeField] private WeaponsSprite _weaponEnum;
        [SerializeField] private bool _disableWindEffect;
        [SerializeField] private bool _silenced;
        [SerializeField] private AccessType _accessType;
        [SerializeField] private PackageType _packageType;
        [SerializeField] private int _accessCost;

        public int WeaponId
        {
            get { return _weaponId; }
            set { _weaponId = value; }
        }
        public string WeaponName
        {
            get { return _weaponName; }
            set { _weaponName = value; }
        }
        public string WeaponDescription
        {
            get { return _weaponDescription; }
            set { _weaponDescription = value; }
        }
        public WeaponsSprite WeaponEnum
        {
            get { return _weaponEnum; }
            set { _weaponEnum = value; }
        }
        public bool DisableWindEffect
        {
            get { return _disableWindEffect; }
            set { _disableWindEffect = value; }
        }
        public bool Silenced
        {
            get { return _silenced; }
            set { _silenced = value; }
        }
        public AccessType AccessType
        {
            get { return _accessType; }
            set { _accessType = value; }
        }
        public PackageType PackageType
        {
            get { return _packageType; }
            set { _packageType = value; }
        }
        public int AccessCost
        {
            get { return _accessCost; }
            set { _accessCost = value; }
        }
    }
}
