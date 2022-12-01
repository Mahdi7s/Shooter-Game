using System;
using Infrastructure;
using Messaging.Hub.Providers;
using Messaging.MessageData;
using Models.Constants;

namespace Menu.Controllers
{
    public class GameplayHudController : ControllerBase
    {
        protected internal override bool IsSingleView { get; set; }
        protected internal override bool IsPopup { get; set; }
        public Action<string> TimeoutChanged { get; set; }
        public Action<string> ShotFired { get; set; }
        public Action<Tuple<WindDirectionSprite, float>> WindChanged { get; set; }
        public Action<bool> ShowCancelAreaAction { get; set; }
        public DateTime EndGameTime { get; set; }
        public bool UnlimitedFireShots { get; set; }
        public bool HasWind { get; set; }
        protected override void Awake()
        {
            base.Awake();
            TimeController.Instance.TimeChangedEvent.AddListener(OnTimeChanged);
            SimpleMessaging.Instance.Register<GameplayMessageData>(this, data =>
            {
                switch (data.Message.GameplayMessageType)
                {
                    case GameplayMessageType.None:
                        break;
                    case GameplayMessageType.MissionStarted:
                        HasWind = data.Message.HasWind;
                        if (HasWind)
                        {
                            WindChanged?.Invoke(new Tuple<WindDirectionSprite, float>(data.Message.WindDirection, data.Message.WindPower));
                        }
                        else
                        {
                            WindChanged?.Invoke(new Tuple<WindDirectionSprite, float>(WindDirectionSprite.Url, 0));
                        }
                        if (data.Message.FireLimit <= 0)
                        {
                            UnlimitedFireShots = true;
                            ShotFired?.Invoke("∞");
                        }
                        else
                        {
                            UnlimitedFireShots = false;
                            ShotFired?.Invoke(data.Message.FireLimit.ToString());
                        }
                        if (data.Message.Timeout <= 0)
                        {
                            EndGameTime = TimeController.Instance.GamePlayTime.AddSeconds(-1);
                            TimeoutChanged?.Invoke("∞");
                        }
                        else
                        {
                            EndGameTime = TimeController.Instance.GamePlayTime.AddSeconds(data.Message.Timeout);
                            TimeoutChanged?.Invoke($"{TimeSpan.FromSeconds(data.Message.Timeout).TotalSeconds:00}");
                        }
                        //GameManager.Instance.IsInGameplay = true;
                        break;
                    case GameplayMessageType.ShotFired:
                        if (!UnlimitedFireShots)
                        {
                            ShotFired?.Invoke(data.Message.FireLimit.ToString());
                        }
                        break;
                    case GameplayMessageType.WindChanged:
                        if (HasWind)
                        {
                            WindChanged?.Invoke(new Tuple<WindDirectionSprite, float>(data.Message.WindDirection, data.Message.WindPower));
                        }
                        break;
                    case GameplayMessageType.MissionEnded:
                        break;
                    case GameplayMessageType.GamePaused:
                        break;
                    case GameplayMessageType.GameResumed:
                        break;
                    case GameplayMessageType.AimStarted:
                        break;
                    case GameplayMessageType.AimEnded:
                        break;
                }
            });
            SimpleMessaging.Instance.Register<GameplayMessageData>(this, data =>
            {
                switch (data.Message.GameplayMessageType)
                {
                    case GameplayMessageType.None:
                        break;
                    case GameplayMessageType.MissionStarted:
                        break;
                    case GameplayMessageType.ShotFired:
                        break;
                    case GameplayMessageType.MissionEnded:
                        break;
                    case GameplayMessageType.GamePaused:
                        break;
                    case GameplayMessageType.GameResumed:
                        break;
                    case GameplayMessageType.AimStarted:
                        ShowCancelAreaAction?.Invoke(true);
                        break;
                    case GameplayMessageType.AimEnded:
                        ShowCancelAreaAction?.Invoke(false);
                        break;
                }
            }, GetType().FullName);
        }
        private void OnTimeChanged()
        {
            if (!GameManager.Instance.IsInGameplay)
                return;

            var timer = EndGameTime - TimeController.Instance.GamePlayTime;
            if (timer >= TimeSpan.Zero)
                TimeoutChanged?.Invoke($"{timer.TotalSeconds:00}");
        }

        private void OnDestroy()
        {
            SimpleMessaging.Instance.UnRegister<GameplayMessageData>(this);
            TimeController.Instance.TimeChangedEvent.RemoveListener(OnTimeChanged);
        }
    }
}