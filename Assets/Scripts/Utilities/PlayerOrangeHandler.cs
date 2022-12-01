using DataServices;
using Infrastructure;
using Models.Constants;
using System;

namespace Utilities
{
    public class PlayerOrangeHandler : Singleton<PlayerOrangeHandler>
    {
        private int _orangeCount;
        private TimeSpan _remainingTime;

        private void Awake()
        {
            TimeController.Instance.TimeChangedEvent.AddListener(OnTimeChanged);
            CheckForBoosters();
            InitializeOranges();
        }

        private DateTime OrangeDate { get; set; }
        public int OrangeCount
        {
            get { return _orangeCount; }
            private set { SetOrangeCount(value); }
        }

        private void SetOrangeCount(int value, bool isDirectSet = false)
        {
            var temp = value;
            if (value < 0)
                temp = 0;
            else if (value > StaticValues.MaxOrangeCount)
                temp = StaticValues.MaxOrangeCount;
            if (temp != _orangeCount)
            {
                _orangeCount = temp;
                GameManager.Instance.PlayerSaveData.OrangeCount = _orangeCount;
                if (!isDirectSet)
                {
                    OrangeDate = TimeController.Instance.GameTime;
                    GameManager.Instance.PlayerSaveData.OrangeDate = OrangeDate;
                }
                GameDataService.Instance.SaveProgress();
                OrangeCountChanged?.Invoke(_orangeCount);
            }
        }
        public void IncreaseOrange(int count)
        {
            SetOrangeCount(_orangeCount + count, true);
        }

        public TimeSpan RemainingTime
        {
            get { return _remainingTime; }
            private set
            {
                _remainingTime = OrangeCount >= StaticValues.MaxOrangeCount ?
                    TimeSpan.Zero :
                    TimeSpan.FromSeconds(StaticValues.OrangeTimer - (value.TotalSeconds % StaticValues.OrangeTimer));

                OrangeTimeChanged?.Invoke(_remainingTime);
            }
        }
        public Action<int> OrangeCountChanged { get; set; }
        public Action<TimeSpan> OrangeTimeChanged { get; set; }
        private void OnTimeChanged()
        {
            OrangeCount = OrangeCount + CalculateOrangeCount();
        }
        private void CheckForBoosters()
        {
            if (GameManager.Instance.PlayerSaveData.BoosterPower == 2)
                StaticValues.OrangeTimer = 180;
            else if (GameManager.Instance.PlayerSaveData.BoosterPower == 1)
                StaticValues.OrangeTimer = 360;
            else
                StaticValues.OrangeTimer = 720;
        }
        private void InitializeOranges()
        {
            OrangeDate = GameManager.Instance.PlayerSaveData.OrangeDate;
            OrangeCount = GameManager.Instance.PlayerSaveData.OrangeCount + CalculateOrangeCount();
        }
        private int CalculateOrangeCount()
        {
            RemainingTime = TimeController.Instance.GameTime - OrangeDate;
            return (int)((TimeController.Instance.GameTime - OrangeDate).TotalSeconds / StaticValues.OrangeTimer);
        }
        public bool TryUseOrange()
        {
            if (OrangeCount <= 0)
            {
                return false;
            }
            OrangeCount--;
            return true;
        }
    }
}
