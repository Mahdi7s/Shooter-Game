using System;
using System.Collections;
using System.Linq;
using Hellmade.Sound;
using Infrastructure;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class GameplayAudioController : MonoBehaviour
    {
        [SerializeField] private GameplayAudiosDataSet _gameplayAudiosDataSet;

        public GameplayAudiosDataSet GameplayAudiosDataSet
        {
            get { return _gameplayAudiosDataSet; }
            set { _gameplayAudiosDataSet = value; }
        }
        private void Awake()
        {
            if (!GameplayAudiosDataSet)
            {
                GameplayAudiosDataSet = ResourceManager.LoadResource<GameplayAudiosDataSet>("GameplayAudios");
            }
        }
        public void StartPlay(int currentChapter, int currentMission)
        {
            if (GameplayAudiosDataSet)
            {
                var selectedMission =
                    GameplayAudiosDataSet.GameplayAudios.FirstOrDefault(x => x.ChapterNumber == currentChapter && x.MissionNumber == currentMission);
                if (selectedMission == null)
                {
                    selectedMission =
                        GameplayAudiosDataSet.GameplayAudios.FirstOrDefault(x => x.ChapterNumber == currentChapter && x.MissionNumber == 0);
                }

                if (selectedMission != null)
                {
                    try
                    {
                        ResourceManager.Instance.LoadResourceAsync<AudioClip>($"AudioClips\\GameplayMusics\\{selectedMission.Music}", string.Empty,
                            music => { TrixSoundManager.Instance.StartPlay(music, Audio.AudioType.Music, true); });

                        foreach (var environment in selectedMission.Environments)
                        {
                            ResourceManager.Instance.LoadResourceAsync<AudioClip>($"AudioClips\\GameplayEnvironments\\{environment}", string.Empty, environmentAudio =>
                            {
                                StartCoroutine(PlayLoop(environmentAudio));
                            });
                        }
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError(exception.Message);
                    }
                }
            }
        }

        private IEnumerator PlayLoop(AudioClip audioClip)
        {
            if (audioClip)
            {
                var waitTime = Random.Range(audioClip.length, audioClip.length + 20);
                Debug.LogWarning("WaitTime:" + waitTime);
                yield return new WaitForSeconds(waitTime);
                TrixSoundManager.Instance.StartPlay(audioClip, Audio.AudioType.UiSound, finishedCallback:
                    () => { StartCoroutine(PlayLoop(audioClip)); });
            }
        }
        public void StopPlay()
        {
            TrixSoundManager.Instance.StopAllMusic();
            StopAllCoroutines();
        }
    }
}
