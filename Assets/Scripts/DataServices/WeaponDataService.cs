using System;
using Infrastructure;
using Menu.Models;
using ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Models;
using Models.Constants;
using UnityEngine;
using Utilities;

namespace DataServices
{
    public class WeaponDataService : Singleton<WeaponDataService>
    {
        [SerializeField] private WeaponsDataSet _weaponsDataSet;

        public WeaponsDataSet WeaponsDataSet
        {
            get { return _weaponsDataSet; }
            set { _weaponsDataSet = value; }
        }

        private void Awake()
        {
            if (!WeaponsDataSet)
            {
                WeaponsDataSet = ResourceManager.LoadResource<WeaponsDataSet>("Weapons");
            }
        }

        public List<WeaponItemModel> GetWeapons()
        {
            return WeaponsDataSet.Weapons.OrderBy(x => x.WeaponId).Select(x => new WeaponItemModel
            {
                AccessType = x.AccessType,
                DisableWindEffect = x.DisableWindEffect,
                Silenced = x.Silenced,
                AccessCost = x.AccessCost,
                WeaponName = x.WeaponName,
                WeaponDescription = x.WeaponDescription,
                WeaponId = x.WeaponId,
                PackageType = x.PackageType,
                WeaponEnum = x.WeaponEnum
            }).ToList();
        }
        public void AddPlayerWeapon(int weaponId)
        {
            var playerWeapons = GameManager.Instance.PlayerSaveData.PlayerWeapons;
            if (playerWeapons.PlayerWeaponsList.All(x => x.WeaponId != weaponId))
            {
                playerWeapons.PlayerWeaponsList.Add(new PlayerWeapon { WeaponId = weaponId });
            }
            playerWeapons.EquippedWeaponId = weaponId;
            GameDataService.Instance.SaveProgress();
        }
        public void EquipPlayerWeapon(int weaponId)
        {
            GameManager.Instance.PlayerSaveData.PlayerWeapons.EquippedWeaponId = weaponId;
            GameDataService.Instance.SaveProgress();
        }
        public void GetCurrentWeapon(Action<Weapon> callback)
        {
            callback(WeaponsDataSet.Weapons.FirstOrDefault(x => x.WeaponId == GameManager.Instance.PlayerSaveData.PlayerWeapons.EquippedWeaponId));
        }
        public void GetEquippedWeaponId(Action<int> callback)
        {
            callback(GameManager.Instance.PlayerSaveData.PlayerWeapons.EquippedWeaponId);
        }
        public PlayerWeapons GetInitialWeapons()
        {
            var playerWeapons = new PlayerWeapons();
            foreach (var weapon in WeaponsDataSet.Weapons.OrderBy(x => x.WeaponId))
            {
                if (weapon.AccessType == AccessType.Free)
                {
                    if (playerWeapons.EquippedWeaponId <= 0)
                    {
                        playerWeapons.EquippedWeaponId = weapon.WeaponId;
                    }
                    playerWeapons.PlayerWeaponsList.Add(new PlayerWeapon
                    {
                        WeaponId = weapon.WeaponId,
                    });
                }
            }
            return playerWeapons;
        }
    }
}
