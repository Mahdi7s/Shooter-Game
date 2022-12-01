using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Infrastructure;
using Models;
using Models.Constants;
using ScriptableObjects;
using UnityEngine;
using Utilities;

namespace Controllers
{
    public class AdvertiseController : MonoBehaviour
    {
        public static AdvertiseController Instance { get; private set; }
        [SerializeField] private AdvertisesDataSet _advertises;
        public AdvertisesDataSet AdvertisesDataSet
        {
            get { return _advertises; }
            set { _advertises = value; }
        }
        private List<StatisticAdvertise> _orderedAdvertiseProviders = new List<StatisticAdvertise>();

        public List<IAdvertiseController> AdvertiseProviders { get; set; } = new List<IAdvertiseController>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
                Instance = this;
            }
        }

        public string GetAdvertiseProviderKey(AdvertiseProvider advertiseProvider)
        {
            var provider = AdvertisesDataSet.AdvertiseProviderKeys.FirstOrDefault(x => x.AdvertiseProvider == advertiseProvider);
            return provider != null ? provider.Key : string.Empty;
        }
        public void ShowAdvertise(AdvertiseType advertiseType, AdvertiseAction advertiseAction, Action<AdvertiseResult> callBack)
        {
            if (!GameManager.Instance.CanUseAdvertises)
            {
                callBack(new AdvertiseResult
                {
                    AdvertiseAction = advertiseAction,
                    IsSuccess = false,
                    Message = "عدم دسترسی به سیستم تبلیغاتی",
                    Reward = 0
                });
            }
            else
            {
                _orderedAdvertiseProviders = AdvertisesDataSet.AdvertiseProviders.
                    Where(x => x.IsActive && x.AdvertiseType == advertiseType && x.AdvertiseAction == advertiseAction)
                    .OrderBy(x => x.AdvertiseOrder).ToList();

#if !UNITY_EDITOR && !UNITY_EDITOR_64
                CheckProvider(advertiseAction, _orderedAdvertiseProviders, callBack);
#else
                StartCoroutine(FakeVideoWaiting(advertiseAction, callBack));
#endif
            }
        }
        private IEnumerator FakeVideoWaiting(AdvertiseAction advertiseAction, Action<AdvertiseResult> callBack)
        {
            yield return new WaitForSeconds(3f);
            callBack(new AdvertiseResult
            {
                AdvertiseAction = advertiseAction,
                IsSuccess = true,
                Message = string.Empty,
                Reward = 0
            });
        }
        private void CheckProvider(AdvertiseAction advertiseAction, List<StatisticAdvertise> orderedAdvertiseProviders, Action<AdvertiseResult> callBack)
        {
            if (AdvertiseProviders.Count > 0 && orderedAdvertiseProviders.Count > 0)
            {
                var provider = orderedAdvertiseProviders[0];
                var advertiseProvider =
                    AdvertiseProviders.FirstOrDefault(x => x.AdvertiseProvider == provider.AdvertiseProvider);
                if (advertiseProvider != null)
                {
                    advertiseProvider.ShowAdvertise(provider, result =>
                    {
                        if (result.IsSuccess)
                        {
                            callBack(result);
                        }
                        else
                        {
                            orderedAdvertiseProviders.Remove(provider);
                            CheckProvider(advertiseAction, orderedAdvertiseProviders, callBack);
                        }
                    });
                }
                else
                {
                    orderedAdvertiseProviders.Remove(provider);
                    CheckProvider(advertiseAction, orderedAdvertiseProviders, callBack);
                }
            }
            else
            {
                callBack(new AdvertiseResult
                {
                    AdvertiseAction = advertiseAction,
                    IsSuccess = false,
                    Message = "تبلیغی برای نمایش وجود ندارد",
                    Reward = 0
                });
            }
        }
    }
}
