using Menu.Controllers;
using Packages.M4u.Scripts.Trixmen;
using UnityEngine;

namespace Menu.ViewModels
{
    //[RequireComponent(typeof(OrangeItemController))]
    public class OrangeItemBindingViewModel : TrixViewModel<OrangeItemController>
    {
        private bool _isAvailable;
        private readonly TrixProp<Color> _orangeColor = new TrixProp<Color>(Color.white);
        public int OrderInList { get; set; }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set
            {
                _isAvailable = value;
                if (value)
                {
                    OrangeColor = Color.white;
                }
                else
                {
                    OrangeColor = new Color32(51, 51, 51, 159);
                }
            }
        }

        public Color OrangeColor
        {
            get { return _orangeColor.Value; }
            set { _orangeColor.Value = value; }
        }

        protected override void Awake()
        {
            IsListView = true;
            base.Awake();
        }

        protected override void OnBackPressed()
        {
            //DoNothing
        }
    }
}