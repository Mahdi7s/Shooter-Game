using DG.Tweening;
using Menu.Controllers;
using Messaging.Hub.Providers;
using Packages.M4u.Scripts.Trixmen;

namespace Menu.ViewModels
{
    //[RequireComponent(typeof(HudController))]
    public class HudBindingViewModel : TrixViewModel<HudController>
    {
        private readonly TrixProp<int> _orangeCount = new TrixProp<int>();
        private readonly TrixProp<int> _playerCoin = new TrixProp<int>();
        private readonly TrixProp<bool> _showBackButton = new TrixProp<bool>();
        private int _coin;

        public int Coin
        {
            get { return _coin; }
            set
            {
                //if (!Equals(_coin, value) && _coinAnimator)
                //    _coinAnimator.SetTrigger("StartGlow");

                _coin = value;
                DOVirtual.Float(PlayerCoin, _coin, 1.7f, result =>
                {
                    PlayerCoin = (int)result;
                }).SetEase(Ease.InCirc);
            }
        }

        #region Properties
        public int OrangeCount
        {
            get { return _orangeCount.Value; }
            set { _orangeCount.Value = value; }
        }
        public int PlayerCoin
        {
            get { return _playerCoin.Value; }
            set { _playerCoin.Value = value; }
        }
        public bool ShowBackButton
        {
            get { return _showBackButton.Value; }
            set { _showBackButton.Value = value; }
        }
        #endregion Properties

        protected override void Awake()
        {
            base.Awake();
            IsShown = true;
            Controller.OrangeCountChanged = OrangeCountChanged;
            Controller.CoinAmountChanged = CoinAmountChanged;
            SimpleMessaging.Instance.Register<bool>(this, message =>
                {
                    ShowBackButton = !message.Message;
                }, GetType().Name);
        }

        private void CoinAmountChanged(int coinAmount)
        {
            Coin = coinAmount;
        }
        private void OrangeCountChanged(int orangeCount)
        {
            OrangeCount = orangeCount;
        }
        private void OnDestroy()
        {
            SimpleMessaging.Instance.UnRegister<bool>(this);
        }

        private void Start()
        {
            Controller.InitializeHud();
        }

        #region Events
        public void OnOrangeBarClickEvent()
        {
            Controller.OnOrangeBarClick();
        }

        public void OnCoinClickEvent()
        {
            Controller.OnCoinClick();
        }
        public void OnBackButtonClickEvent()
        {
            Controller.OnBackButtonPressed();
        }

        public void OnSettingButtonClickEvent()
        {
            Controller.OnSettingClick();
        }
        protected override void OnBackPressed()
        {
            //DoNothing
        }
        #endregion Events
    }
}