using Controllers;
using DataServices;
using Hellmade.Sound;
using Infrastructure;
using Menu.Controllers;
using Models;
using Models.Constants;
using Packages.M4u.Scripts.Trixmen;
using UnityEngine;
using Utilities;
using HudController = Menu.Controllers.HudController;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(SpinnerController))]*/
    public class SpinnerBindingViewModel : TrixViewModel<SpinnerController>
    {
        private readonly TrixProp<WheelsFortuneHandler> _wheelsFortune = new TrixProp<WheelsFortuneHandler>();
        private readonly TrixProp<bool> _canSpin = new TrixProp<bool>();
        private readonly TrixProp<bool> _showFreeSpinner = new TrixProp<bool>();
        private readonly TrixProp<bool> _showAdvertiseSpinner = new TrixProp<bool>();
        private readonly TrixProp<bool> _showMessageNormal = new TrixProp<bool>();
        private readonly TrixProp<bool> _showMessageAds = new TrixProp<bool>();
        private readonly TrixProp<string> _message = new TrixProp<string>();
        private readonly TrixProp<Animator> _rewardAnimator = new TrixProp<Animator>();
        private readonly TrixProp<Animator> _advertiseAnimator = new TrixProp<Animator>();
        private bool _canRetryAdvertiseSpin = false;

        public WheelsFortuneHandler WheelsFortune
        {
            get { return _wheelsFortune.Value; }
            set { _wheelsFortune.Value = value; }
        }

        public bool ShowMessageAds
        {
            get { return _showMessageAds.Value; }
            set { _showMessageAds.Value = value; }
        }

        public bool ShowMessageNormal
        {
            get { return _showMessageNormal.Value; }
            set { _showMessageNormal.Value = value; }
        }

        public bool CanSpin
        {
            get { return _canSpin.Value; }
            set { _canSpin.Value = value; }
        }

        public bool ShowFreeSpinner
        {
            get { return _showFreeSpinner.Value; }
            set { _showFreeSpinner.Value = value; }
        }

        public bool ShowAdvertiseSpinner
        {
            get { return _showAdvertiseSpinner.Value; }
            set { _showAdvertiseSpinner.Value = value; }
        }

        public string Message
        {
            get { return _message.Value; }
            set { _message.Value = value; }
        }

        public Animator RewardAnimator
        {
            get { return _rewardAnimator.Value; }
            set { _rewardAnimator.Value = value; }
        }

        public Animator AdvertiseAnimator
        {
            get { return _advertiseAnimator.Value; }
            set { _advertiseAnimator.Value = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            Controller.OnIsShownChanged = OnIsShownChanged;
        }

        private void SpinAdvertiseWheelsFortune(AdvertiseAction advertiseAction)
        {
            if (advertiseAction == AdvertiseAction.Spinner)
            {
                StartAdvertiseSpin();
            }
        }

        private void ShowAdvertiseError(string message)
        {
            Message = "خطا در نمایش ویدئو تبلیغاتی";
        }

        private void OnIsShownChanged(bool shown, object data)
        {
            if (shown)
            {
                CanSpin = true;
                _canRetryAdvertiseSpin = false;
                ShowFreeSpinner = true;
                ShowAdvertiseSpinner = false;
            }
        }

        public void GiveRewardToPlayer(WheelsFortuneItem reward)
        {
            if (reward.RewardType == WheelsFortuneRewardType.Coin)
            {
                CoinDataService.Instance.IncreasePlayerCoin(reward.ItemAmount, "Source");
                Message = $"{reward.ItemAmount} سکه جایزه گرفتی !";
            }
            else if (reward.RewardType == WheelsFortuneRewardType.Orange)
            {
                PlayerOrangeHandler.Instance.IncreaseOrange(reward.ItemAmount);
                Message = $"{reward.ItemAmount} پرتقال جایزه گرفتی !";
            }
        }

        private void StartAdvertiseSpin()
        {
            if (WheelsFortune && CanSpin && ShowAdvertiseSpinner)
            {
                TrixResource.AudioClips.spinner_async(spin =>
                {
                    TrixSoundManager.Instance.StartPlay(spin, Audio.AudioType.UiSound);
                    CanSpin = false;
                    _canRetryAdvertiseSpin = false;
                    ShowAdvertiseSpinner = false;
                    WheelsFortune.Spin(result =>
                    {
                        if (result != null)
                        {
                            ShowMessageAds = true;
                            ShowMessageNormal = false;
                            GiveRewardToPlayer(result);
                        }
                        else
                        {
                            CanSpin = true;
                            _canRetryAdvertiseSpin = true;
                            ShowAdvertiseSpinner = true;
                        }

                        TrixResource.AudioClips.reward_spinner_async(spin2 =>
                        {
                            TrixSoundManager.Instance.StartPlay(spin2, Audio.AudioType.UiSound);
                        });
                    });
                });
            }
        }

        public void OnStartFreeSpinClickEvent()
        {
            if (WheelsFortune && CanSpin && ShowFreeSpinner)
            {
                CanSpin = false;
                ShowFreeSpinner = false;
                Controller.HideView(typeof(HudController));
                TrixResource.AudioClips.spinner_async(spin =>
                {
                    TrixSoundManager.Instance.StartPlay(spin, Audio.AudioType.UiSound);


                    WheelsFortune.Spin(result =>
                    {
                        CanSpin = true;
                        Controller.ShowView(typeof(HudController), hideOtherViews: false);
                        if (result != null)
                        {
                            ShowMessageAds = false;
                            ShowMessageNormal = true;
                            ShowAdvertiseSpinner = true;
                            GiveRewardToPlayer(result);
                        }

                        TrixResource.AudioClips.reward_spinner_async(spin2 =>
                        {
                            TrixSoundManager.Instance.StartPlay(spin2, Audio.AudioType.UiSound);
                        });

                       
                    });
                });
            }
        }

        public void OnStartAdvertiseSpinClickEvent()
        {
            if (_canRetryAdvertiseSpin)
                StartAdvertiseSpin();
            else
                AdvertiseController.Instance.ShowAdvertise(AdvertiseType.Video, AdvertiseAction.Spinner, result =>
                {
                   
                    if (result.IsSuccess)
                    {
                        SpinAdvertiseWheelsFortune(result.AdvertiseAction);
                    }
                    else
                    {
                        ShowAdvertiseError(result.Message);
                    }
                });
        }

        protected override void OnBackPressed()
        {
            if (IsShown)
            {
                Controller.ShowView(typeof(MainMenuController));
            }
        }
    }
}