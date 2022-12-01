using System.Collections;
using Hellmade.Sound;
using Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace TrixComponents
{
    public class TrixToggle : Toggle
    {
        public AudioClip ClickSound;
        public Animator Animator;
        public float ClickDelay = 0.3f;

        public void PlaySound()
        {
            if (ClickSound)
            {
                TrixSoundManager.Instance.StartPlay(ClickSound, Audio.AudioType.UiSound);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            onValueChanged.AddListener(onVal => { Animator.SetTrigger(isOn ? "ON" : "OFF"); });
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (ClickDelay > 0 && gameObject.activeSelf)
            {
                if (!interactable)
                    return;
                interactable = false;
                StartCoroutine(EnableButton());
            }
            PlaySound();
        }

        private IEnumerator EnableButton()
        {
            yield return new WaitForSeconds(ClickDelay);
            interactable = true;
        }
    }
}
