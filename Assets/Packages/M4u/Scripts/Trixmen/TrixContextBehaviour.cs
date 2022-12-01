using M4u;
using Menu.ViewModels;
using UnityEngine;

namespace Packages.M4u.Scripts.Trixmen
{
    public class TrixContextBehaviour<TViewModel> : M4uContextMonoBehaviour, ITrixContext where TViewModel : ViewModelBase
    {
        [SerializeField] private TViewModel _viewModel;

        public TViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        protected virtual void Awake()
        {
            if (!ViewModel)
            {
                ViewModel = GetComponent<TViewModel>();
            }
        }

        private void Reset()
        {
            if (!ViewModel)
                ViewModel = GetComponent<TViewModel>();
        }
    }
}
