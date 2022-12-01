using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Constants;
using Utilities;

namespace Controllers
{
    public class PurchaseController : Singleton<PurchaseController>
    {
        private List<Package> _packages = new List<Package>();
        private void Start()
        {
            if (InitMarketBehavior.Instance.PackageDataSet)
            {
                _packages = InitMarketBehavior.Instance.PackageDataSet.Packages;
            }
        }

        public void BuyBooster(int number, Action<bool> isSucceed)
        {
            var booster = _packages.FirstOrDefault(x => x.PackageType == (number == 1 ? PackageType.Booster01 : PackageType.Booster02));
            if (booster != null)
            {
#if !UNITY_EDITOR && !UNITY_EDITOR_64
                MarketUtility.BeginPurchase(booster.Sku, () => { isSucceed(true); }, () => { isSucceed(false); }, null);
#else
                isSucceed(true);
#endif
            }
        }
        public void BuyPackage(PackageType packageType, Action<bool> isSucceed)
        {
            var package = _packages.FirstOrDefault(x => x.PackageType == packageType);
            if (package != null)
            {
#if !UNITY_EDITOR && !UNITY_EDITOR_64
                MarketUtility.BeginPurchase(package.Sku, () => { isSucceed(true); }, () => { isSucceed(false); }, null);
#else
                isSucceed(true);
#endif
            }
        }

    }
}
