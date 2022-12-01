using System;
using UnityEngine;

namespace Utilities
{
    public static class MarketUtility
    {
        private static Action _successAction, _failAction;
        private static GameObject _fade;
        public static void BeginPurchase(string itemStoreId, Action success, Action fail, GameObject fade)
        {
#if UNITY_EDITOR
            success();
#else
        _successAction = success;
        _failAction = fail;
        _fade = fade;
        try
        {
            if (_fade)
            {
                _fade.SetActive(true);
                _fade.transform.SetAsLastSibling();
            }
            DoPurchaseSoomla(itemStoreId);

        }
        catch (Exception exception)
        {
            Debug.LogError($"{exception.Message}\t{(string.IsNullOrEmpty(exception.StackTrace) ? "" : exception.StackTrace)}");
            PurchaseComplete(false);
        }
#endif
        }
        private static void DoPurchaseSoomla(string itemStoreId)
        {
            if (InitMarketBehavior.Instance.PublishingMarket == StoresName.Cafebazaar || InitMarketBehavior.Instance.PublishingMarket == StoresName.IranApps)
            {
                InitMarketBehavior.Instance.BeginBazaarPurchase(itemStoreId, purchase =>
                {
                    PurchaseComplete(true);
                }, () =>
                {
                    PurchaseComplete(false);
                });
            }
        }
        private static void PurchaseComplete(bool complete)
        {
            if (complete)
            {
                _successAction();
                SetFadeInactive();
            }
            else
            {
                _failAction();
                SetFadeInactive();
            }
        }
        public static void SetFadeInactive()
        {
            if (_fade)
            {
                _fade.transform.SetAsFirstSibling();
                _fade.SetActive(false);
            }
        }
    }
}
