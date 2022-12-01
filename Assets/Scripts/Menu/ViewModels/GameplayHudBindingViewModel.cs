using Infrastructure;
using Menu.Controllers;
using Messaging.Hub.Providers;
using Messaging.MessageData;
using Models.Constants;
using Packages.M4u.Scripts.Trixmen;
using TrixComponents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(GameplayHudController))]*/
    public class GameplayHudBindingViewModel : TrixViewModel<GameplayHudController>
    {
        private readonly TrixProp<string> _fireLimit = new TrixProp<string>();
        private readonly TrixProp<string> _timeout = new TrixProp<string>();
        private readonly TrixProp<bool> _hasWind = new TrixProp<bool>();
        private readonly TrixProp<Sprite> _windDirection = new TrixProp<Sprite>();
        private readonly TrixProp<float> _windPower = new TrixProp<float>();
        private readonly TrixProp<bool> _showCancelArea = new TrixProp<bool>();
        private readonly TrixProp<RectTransform> _fireLimitHudPosition = new TrixProp<RectTransform>();
        private readonly TrixProp<RectTransform> _timeoutHudPosition = new TrixProp<RectTransform>();
        private WindDirectionSprite _windDirectionEnum;

        public string FireLimit
        {
            get { return _fireLimit.Value; }
            set { _fireLimit.Value = value; }
        }
        public string Timeout
        {
            get { return _timeout.Value; }
            set { _timeout.Value = value; }
        }
        public bool HasWind
        {
            get { return _hasWind.Value; }
            set
            {
                _hasWind.Value = value;
                if (FireLimitHudPosition)
                    SetFireLimitHudPosition();
                if (TimeoutHudPosition)
                    SetTimeoutHudPosition();
            }
        }

        public Sprite WindDirection
        {
            get { return _windDirection.Value; }
            set { _windDirection.Value = value; }
        }
        public float WindPower
        {
            get { return _windPower.Value; }
            set { _windPower.Value = value; }
        }
        public bool ShowCancelArea
        {
            get { return _showCancelArea.Value; }
            set { _showCancelArea.Value = value; }
        }
        public RectTransform FireLimitHudPosition
        {
            get { return _fireLimitHudPosition.Value; }
            set
            {
                _fireLimitHudPosition.Value = value;
                if (value)
                    SetFireLimitHudPosition();
            }
        }
        public RectTransform TimeoutHudPosition
        {
            get { return _timeoutHudPosition.Value; }
            set
            {
                _timeoutHudPosition.Value = value;
                if (value)
                    SetTimeoutHudPosition();
            }
        }

        public WindDirectionSprite WindDirectionEnum
        {
            get { return _windDirectionEnum; }
            set
            {
                if (Equals(_windDirectionEnum, value))
                    return;

                _windDirectionEnum = value;
                TrixResource.Sprites.GetByEnum(value, string.Empty, result =>
                {
                    WindDirection = result;
                });
            }
        }

        protected override void Awake()
        {
            base.Awake();
            WindDirection = GameManager.Instance.NoImageSprite;
            Controller.ShotFired = remainingShots => FireLimit = remainingShots;
            Controller.TimeoutChanged = time => Timeout = time;
            Controller.ShowCancelAreaAction = ShowCancelAreaAction;
            Controller.WindChanged = directionAndPower =>
            {
                HasWind = Controller.HasWind;
                WindDirectionEnum = directionAndPower.Item1;
                WindPower = directionAndPower.Item2;
            };
        }

        private void ShowCancelAreaAction(bool showCancelArea)
        {
            ShowCancelArea = showCancelArea;
        }

        private void SetFireLimitHudPosition()
        {
            if (_hasWind.Value)
            {
                FireLimitHudPosition.anchorMin = new Vector2(0.21f, 0f);
                FireLimitHudPosition.anchorMax = new Vector2(0.4f, 0.9f);
            }
            else
            {
                FireLimitHudPosition.anchorMin = new Vector2(0.3f, 0f);
                FireLimitHudPosition.anchorMax = new Vector2(0.49f, 0.9f);
            }
        }
        private void SetTimeoutHudPosition()
        {
            if (_hasWind.Value)
            {
                TimeoutHudPosition.anchorMin = new Vector2(0.405f, 0f);
                TimeoutHudPosition.anchorMax = new Vector2(0.595f, 0.9f);
            }
            else
            {
                TimeoutHudPosition.anchorMin = new Vector2(0.51f, 0f);
                TimeoutHudPosition.anchorMax = new Vector2(0.7f, 0.9f);
            }
        }

        public void OnPauseButtonClickEvent()
        {
            SimpleMessaging.Instance.SendMessage(new MessageData<GameplayMessageData, string>
            {
                Message = new GameplayMessageData
                {
                    GameplayMessageType = GameplayMessageType.GamePaused
                }
            });
        }
        protected override void OnBackPressed()
        {
        }
    }
}