using System;
using System.Collections.Generic;
using Infrastructure;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class InitMarketBehavior : Singleton<InitMarketBehavior>
    {
        public PackageDataSet PackageDataSet;
        public StoresName PublishingMarket;
        public int VersionCode = 1000;
        public string PackageName;

        public GameObject CafeBazaarGameObject;

        private GameObject _cafePlugin;
        private InAppStore _inAppStore;

        private static bool _marketInitialized;

        private void Awake()
        {
            if (!PackageDataSet)
            {
                PackageDataSet = ResourceManager.LoadResource<PackageDataSet>("Packages");
            }
            SceneManager.activeSceneChanged += OnSceneChange;
            InitSoomlaMarket();
        }

        private void OnSceneChange(Scene previous, Scene next)
        {
            if (PublishingMarket == StoresName.Cafebazaar || PublishingMarket == StoresName.IranApps)
                _marketInitialized = false;
        }

        public bool InitSoomlaMarket()
        {
            if (!_marketInitialized)
            {
                if (PublishingMarket == StoresName.Cafebazaar || PublishingMarket == StoresName.IranApps)
                {
                    _cafePlugin = Instantiate(CafeBazaarGameObject, GameManager.Instance.transform);
                    _cafePlugin.name = "com.bobardo IAB";
                    var storeHandler = _cafePlugin.GetComponent<StoreHandler>();
                    _inAppStore = _cafePlugin.GetComponent<InAppStore>();

                    storeHandler.publicKey =
                        "MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBrwCrSGDvkwElEDoDhUJplLQWMFz79fXAeIR9Fi0jVQ+6UhVZNHFORDKNjMCcbvkJ2Yd5+hf7Ay3IGPDa+y2qgnR/Iet/ntpQaEdz544FCT8XI5FawMmVNV5m7g7iuelf2OIpjkkSgiC/rFmvFkdNE30A2uIpsht7BCHXCK2tgmIAuHDrQDvf+5UjYE4+GD+76cQZcAezE/J38MHyIgz7AcrPEtJiwaUnH9c41/AEpO8CAwEAAQ==";
                    var tempList = new List<Product>();
                    foreach (var package in PackageDataSet.Packages)
                    {
                        tempList.Add(new Product
                        {
                            productId = package.Sku,
                            type = package.IsConsumable ? Product.ProductType.Consumable : Product.ProductType.NonConsumable
                        });
                    }

                    _inAppStore.products = tempList.ToArray();

                    _marketInitialized = true;
                    //Debug.LogError("market initialized...");
                }
                else
                {
                    //_marketInitialized = SoomlaStore.Initialize(new StoreAssets(ids, unconsumableIds));
                }
            }
            return _marketInitialized;
        }

        public void BeginBazaarPurchase(string sku, Action<Purchase> successAction, Action failAction)
        {
            //inAppStore = Camera.main.GetComponentInChildren<InAppStore>();
            _inAppStore.SucessAction = successAction;
            _inAppStore.FailAction = failAction;
            for (int i = 0; i < _inAppStore.products.Length; i++)
            {
                if (string.Equals(_inAppStore.products[i].productId, sku, StringComparison.InvariantCultureIgnoreCase))
                {
                    _inAppStore.purchaseProduct(i);
                }
            }
        }
    }
}