using Infrastructure;
using System;
using Utilities;

namespace DataServices
{
    public class CoinDataService : Singleton<CoinDataService>
    {
        public Action<int> CoinAmountChanged { get; set; }
        public void GetPlayerCoin(Action<int> callback)
        {
            callback(GameManager.Instance.PlayerSaveData.PlayerCoin);
        }

        public void IncreasePlayerCoin(int amount, string sourceName, bool needSave = true)
        {
            if (amount <= 0)
                return;

            var newCoinAmount = GameManager.Instance.PlayerSaveData.PlayerCoin + amount;

            if (!string.IsNullOrWhiteSpace(sourceName))
            {
                //ProTODO: Firebase Analytics
            }

            GameManager.Instance.PlayerSaveData.PlayerCoin = newCoinAmount;
            if (needSave)
                GameDataService.Instance.SaveProgress();
            CoinAmountChanged?.Invoke(newCoinAmount);
        }

        public void DecreasePlayerCoin(int amount, string sinkName, bool needSave = true)
        {
            if (amount <= 0)
                return;

            var newCoinAmount = GameManager.Instance.PlayerSaveData.PlayerCoin - amount;

            if (!string.IsNullOrWhiteSpace(sinkName))
            {
                //ProTODO: Firebase Analytics
            }

            GameManager.Instance.PlayerSaveData.PlayerCoin = newCoinAmount;
            if (needSave)
                GameDataService.Instance.SaveProgress();
            CoinAmountChanged?.Invoke(newCoinAmount);
        }
    }
}
