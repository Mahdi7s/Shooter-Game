using System;
using System.Collections;
using UnityEngine;
using Utilities;
using Hellmade.Sound;
using AudioType = Hellmade.Sound.Audio.AudioType;

namespace Infrastructure
{
    public class TrixSoundManager : Singleton<TrixSoundManager>
    {
        private float _globalMusicVolume;
        private float _fadeOutTimer = 1;
        private bool _isFadedOut;

        private bool CanPlaySfx { get; set; } = true;
        private bool CanPlayMusic { get; set; } = true;

        private void Start()
        {
            _globalMusicVolume = EazySoundManager.GlobalMusicVolume;
        }

        private void Update()
        {
            if (_isFadedOut && (_fadeOutTimer -= Time.deltaTime) <= 0)
            {
                ResumeVolume();
            }
        }

        public void SetConfig(bool canPlayMusic, bool canPlaySfx)
        {
            CanPlayMusic = canPlayMusic;
            CanPlaySfx = canPlaySfx;
        }
        public int StartPlay(AudioClip audioClip, AudioType audioType, bool loop = false, float volume = 1, Transform sourceTransform = null, Action finishedCallback = null)
        {
            if (audioType == AudioType.Music && !CanPlayMusic)
                return -1;
            if (audioType != AudioType.Music && !CanPlaySfx)
                return -1;
            if (!audioClip)
                return -1;

            if (!loop && finishedCallback != null)
                StartCoroutine(DelayedCallback(audioClip.length, finishedCallback));

            switch (audioType)
            {
                case AudioType.Music:
                    return EazySoundManager.PlayMusic(audioClip, volume, loop, false);
                case AudioType.Sound:
                    return EazySoundManager.PlaySound(audioClip, volume, loop, sourceTransform);
                case AudioType.UiSound:
                    return EazySoundManager.PlayUiSound(audioClip, volume, loop);
                case AudioType.UiVoice:
                    FadeVolumeOut(audioClip.length);
                    return EazySoundManager.PlayUiSound(audioClip, volume, loop);
                default:
                    return -1;
            }
        }
        private IEnumerator DelayedCallback(float timeout, Action callback)
        {
            yield return new WaitForSeconds(timeout);
            callback();
        }
        public void StopPlay(AudioClip audioClip, float fadeOutSeconds = 0)
        {
            if (!audioClip)
                return;
            try
            {
                var aud = EazySoundManager.GetAudio(audioClip);
                if (aud != null)
                {
                    aud.FadeOutSeconds = fadeOutSeconds;
                    aud.Stop();
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }
        public void StopPlay(int audioClipId, float fadeOutSeconds = 0)
        {
            if (audioClipId < 0)
                return;

            try
            {
                var aud = EazySoundManager.GetAudio(audioClipId);
                if (aud != null)
                {
                    if (aud.Type == AudioType.UiVoice)
                    {
                        ResumeVolume();
                    }
                    aud.FadeOutSeconds = fadeOutSeconds;
                    aud.Stop();
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        /// <summary>
        /// Stop all music playing
        /// </summary>
        /// <param name="fadeOutSeconds"> How many seconds it needs for all music audio to fade out. It will override  their own fade out seconds. If -1 is passed, all music will keep their own fade out seconds</param>
        public void StopAllMusic(float fadeOutSeconds = 0)
        {
            EazySoundManager.StopAllMusic(fadeOutSeconds);
        }

        /// <summary>
        /// Stop all sounds playing
        /// </summary>
        public void StopAllSounds()
        {
            EazySoundManager.StopAllSounds();
        }
        /// <summary>
        /// Stop all UI sound fx playing
        /// </summary>
        public void StopAllUiSounds()
        {
            EazySoundManager.StopAllUISounds();
            ResumeVolume();
        }
        public void PausePlay(AudioClip audioClip)
        {
            var aud = EazySoundManager.GetAudio(audioClip);
            aud?.Pause();
        }

        public void ResumePlay(AudioClip audioClip, AudioType audioType)
        {
            if (audioType == AudioType.Music && !CanPlayMusic)
                return;
            if (audioType != AudioType.Music && !CanPlaySfx)
                return;
            var aud = EazySoundManager.GetAudio(audioClip);
            aud?.Resume();
        }

        private void FadeVolumeOut(float duration)
        {
            EazySoundManager.GlobalMusicVolume = 0.3f;
            _isFadedOut = true;
            _fadeOutTimer = duration;
        }

        private void ResumeVolume()
        {
            _isFadedOut = false;
            _fadeOutTimer = 1;
            EazySoundManager.GlobalMusicVolume = _globalMusicVolume;
        }
    }
}
