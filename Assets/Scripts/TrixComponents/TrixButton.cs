using Hellmade.Sound;
using Infrastructure;
using M4u;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TrixComponents
{
    public class TrixButton : Button
    {
        public AudioClip ClickSound;
        public float ClickDelay;
        private string _eventAction;
        private string _eventCategory;
        protected override void Start()
        {
            var bind = GetComponent<M4uEventBinding>();
            _eventAction = bind ? bind.Path : gameObject.name;
            _eventCategory = SceneManager.GetActiveScene().name;
        }
        /* protected override void Reset()
        {
            base.Reset();
            if (!ClickSound)
            {
                ClickSound = TrixResource.AudioClips.click;
            }
        } */
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            //Firebase.Analytics.Parameter[] LevelUpParameters = {
            //    new Firebase.Analytics.Parameter(
            //        Firebase.Analytics.FirebaseAnalytics.ParameterLevel, 5),
            //    new Firebase.Analytics.Parameter(
            //        Firebase.Analytics.FirebaseAnalytics.ParameterCharacter, "mrspoon"),
            //    new Firebase.Analytics.Parameter(
            //        "hit_accuracy", 3.14f)
            //};
            //Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelUp, LevelUpParameters);
            //Analytics.gua.sendEventHit(_eventCategory, _eventAction);
            if (ClickDelay > 0 && gameObject.activeSelf)
            {
                if (!interactable)
                    return;
                this.interactable = false;
                TrixButtonController.Instance.StartButtonEnable(this, ClickDelay);
            }
            PlaySound();
        }
        public void PlaySound()
        {
            if (ClickSound)
            {
                TrixSoundManager.Instance.StartPlay(ClickSound, Audio.AudioType.UiSound);
            }
        }
    }
}