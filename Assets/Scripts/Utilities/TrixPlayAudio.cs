using System.Collections;
using Infrastructure;
using UnityEngine;
using AudioType = Hellmade.Sound.Audio.AudioType;
namespace Utilities
{
    public class TrixPlayAudio : MonoBehaviour
    {
        public AudioClip AudioClip;
        public AudioType AudioType;
        public PlayType PlayType;
        public bool Loop;
        public float PlayDelay;
        [Range(0.00f, 1.00f)]
        public float Volume;
        public float FadeOutSeconds;

        private int _audioClipId = -1;


        private void Awake()
        {
            if (PlayType == PlayType.Awake)
                StartCoroutine(PlayWithDelay());
        }

        private void Start()
        {
            if (PlayType == PlayType.Start)
                StartCoroutine(PlayWithDelay());
        }
        private void OnEnable()
        {
            if (PlayType == PlayType.Enable)
                StartCoroutine(PlayWithDelay());
        }

        public void StartPlay()
        {
            if (PlayType == PlayType.Trigger)
                StartCoroutine(PlayWithDelay());
        }

        public void StopPlay()
        {
            if (_audioClipId < 0)
                return;

            StopCoroutine(PlayWithDelay());
            TrixSoundManager.Instance.StopPlay(_audioClipId, FadeOutSeconds);
        }
        private void OnDisable()
        {
            if (_audioClipId < 0)
                return;

            StopCoroutine(PlayWithDelay());
            TrixSoundManager.Instance.StopPlay(_audioClipId, FadeOutSeconds);
        }
        private IEnumerator PlayWithDelay()
        {
            yield return new WaitForSeconds(PlayDelay);
            _audioClipId = TrixSoundManager.Instance.StartPlay(AudioClip, AudioType, Loop, Volume, transform);
        }
    }

    public enum PlayType
    {
        Awake = 1,
        Start = 2,
        Enable = 3,
        Trigger = 4
    }
}
