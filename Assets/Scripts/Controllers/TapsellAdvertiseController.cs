using System;
using Contracts;
using Models;
using TapsellSDK;
using UnityEngine;

namespace Controllers
{
    public class TapsellAdvertiseController : MonoBehaviour, IAdvertiseController
    {
        private TapsellAd _currentTapsellAdvertise;
        private void Awake()
        {
            AdvertiseController.Instance.AdvertiseProviders.Add(this);
            var key = AdvertiseController.Instance.GetAdvertiseProviderKey(AdvertiseProvider);
            if (!string.IsNullOrEmpty(key))
            {
                Tapsell.initialize(key);
                Tapsell.setDebugMode(false);
            }
        }

        public AdvertiseProvider AdvertiseProvider { get; set; } = AdvertiseProvider.Tapsell;
        public void ShowAdvertise(StatisticAdvertise advertise, Action<AdvertiseResult> result)
        {
            switch (advertise.AdvertiseType)
            {
                case AdvertiseType.Video:
                    ShowVideo(advertise, result);
                    break;
                case AdvertiseType.FullBanner:
                    break;
                case AdvertiseType.NativeBanner:
                    break;
            }
        }

        private void ShowVideo(StatisticAdvertise advertise, Action<AdvertiseResult> resultAction)
        {
            CheckTapsellAvailability(advertise, result =>
            {
                if (result)
                {
                    SetupTapsellListeners(advertise, resultAction);
                    if (_currentTapsellAdvertise.zoneId.Equals(advertise.UnitCode))
                    {
                        Tapsell.showAd(_currentTapsellAdvertise, new TapsellShowOptions
                        {
                            backDisabled = false,
                            immersiveMode = false,
                            rotationMode = TapsellShowOptions.ROTATION_LOCKED_LANDSCAPE,
                            showDialog = true
                        });
                    }
                    else
                    {
                        resultAction(new AdvertiseResult
                        {
                            IsSuccess = false,
                            AdvertiseAction = advertise.AdvertiseAction,
                            Message = "خطا در تنظیمات تبلیغات",
                            Reward = 0
                        });
                    }
                }
                else
                {
                    resultAction(new AdvertiseResult
                    {
                        IsSuccess = false,
                        AdvertiseAction = advertise.AdvertiseAction,
                        Message = "تبلیغات تپسل در دسترس نمی باشد",
                        Reward = 0
                    });
                }
            });
        }
        private void CheckTapsellAvailability(StatisticAdvertise statisticAdvertise, Action<bool> onResult)
        {
            Tapsell.requestAd(statisticAdvertise.UnitCode, false,
                //triggered whenever the ad is available
                tapsellAd =>
                {
                    _currentTapsellAdvertise = tapsellAd;
                    onResult(true);
                },
                //triggered whenever no ad is available
                zoneId =>
                {
                    onResult(false);
                },
                //triggered whenever an error has been occured 
                tapsellError =>
                {
                    onResult(false);
                },
                //triggered when no network is available
                zoneId =>
                {
                    onResult(false);
                },
                //triggered when the current ad is expired
                tapsellAd =>
                {
                    onResult(false);
                });
        }
        private void SetupTapsellListeners(StatisticAdvertise statisticAdvertise, Action<AdvertiseResult> resultAction)
        {
            Tapsell.setRewardListener(finishResult =>
            {
                if (finishResult.rewarded && finishResult.completed && finishResult.zoneId.Equals(statisticAdvertise.UnitCode))
                {
                    resultAction(new AdvertiseResult
                    {
                        IsSuccess = true,
                        AdvertiseAction = statisticAdvertise.AdvertiseAction,
                        Message = string.Empty,
                        Reward = statisticAdvertise.Prize
                    });
                }
                else
                {
                    resultAction(new AdvertiseResult
                    {
                        IsSuccess = false,
                        AdvertiseAction = statisticAdvertise.AdvertiseAction,
                        Message = "خطا در پخش ویدئو",
                        Reward = 0
                    });
                }
            });
        }
    }
}
