using System.Collections;
using UnityEngine;
using Utilities;

namespace TrixComponents
{
    public class TrixButtonController : Singleton<TrixButtonController>
    {
        public float ClickDelay { get; set; }

        public void StartButtonEnable(TrixButton trixButton, float clickDelay)
        {
            StartCoroutine(EnableButton(trixButton, clickDelay));
        }

        private IEnumerator EnableButton(TrixButton trixButton, float clickDelay)
        {
            yield return new WaitForSeconds(clickDelay);
            if (trixButton)
                trixButton.interactable = true;
        }
    }
}
