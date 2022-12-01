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
    public class StoreDataService : Singleton<StoreDataService>
    {
        [SerializeField] private StoreDataSet _storeDataSet;

        public StoreDataSet StoreDataSet
        {
            get { return _storeDataSet; }
            set { _storeDataSet = value; }
        }

        private void Awake()
        {
            if (!StoreDataSet)
            {
                StoreDataSet = ResourceManager.LoadResource<StoreDataSet>("Store");
            }
        }

        public List<StoreItemModel> GetStoreItems()
        {
            return StoreDataSet.StoreItems.OrderBy(x => x.Order).Select(x => new StoreItemModel
            {
                Id = x.Id,
                PackageType = x.PackageType,
                CoinAmount = x.CoinAmount,
                Cost = x.Cost
            }).ToList();
        }
    }
}
