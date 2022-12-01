using System;
using System.Collections;
using System.Linq;
using DataServices;
using DG.Tweening;
using Hellmade.Sound;
using Infrastructure;
using Menu.Controllers;
using Messaging.MessageData;
using Models.Constants;
using Packages.M4u.Scripts.Trixmen;
using ScriptableObjects;
using TrixComponents;
using UnityEngine;
using Utilities;

namespace Menu.ViewModels
{
    /*[RequireComponent(typeof(EndGameBoardsController))]*/
    public class EndGameBoardsBindingViewModel : TrixViewModel<EndGameBoardsController>
    {
        private bool _isEndChapter;
        private float _fakeVideoTimer;
        private int _audioId;
        private readonly TrixProp<string> _reward = new TrixProp<string>();
        private readonly TrixProp<string> _endChapterReward = new TrixProp<string>();
        private readonly TrixProp<float> _timer = new TrixProp<float>();
        private readonly TrixProp<string> _description = new TrixProp<string>();
        private readonly TrixProp<BoardType> _boardType = new TrixProp<BoardType>();
        private readonly TrixProp<Sprite> _firstChapterStar = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _secondChapterStar = new TrixProp<Sprite>();
        private readonly TrixProp<Sprite> _thirdChapterStar = new TrixProp<Sprite>();
        private readonly TrixProp<int> _firstMissionLoseCount = new TrixProp<int>();
        private readonly TrixProp<int> _secondMissionLoseCount = new TrixProp<int>();
        private readonly TrixProp<int> _thirdMissionLoseCount = new TrixProp<int>();
        private readonly TrixProp<int> _forthMissionLoseCount = new TrixProp<int>();
        private readonly TrixProp<int> _fifthMissionLoseCount = new TrixProp<int>();
        private readonly TrixProp<int> _sixthMissionLoseCount = new TrixProp<int>();
        private readonly TrixProp<int> _seventhMissionLoseCount = new TrixProp<int>();
        private readonly TrixProp<int> _eighthMissionLoseCount = new TrixProp<int>();
        private readonly TrixProp<int> _totalShots = new TrixProp<int>();
        private readonly TrixProp<int> _enemiesKilled = new TrixProp<int>();
        private readonly TrixProp<int> _innocentCasualties = new TrixProp<int>();
        private readonly TrixProp<float> _accuracy = new TrixProp<float>();
        private readonly TrixProp<bool> _showWarningPopup = new TrixProp<bool>();
        private readonly TrixProp<bool> _showPausePopup = new TrixProp<bool>();
        private readonly TrixProp<bool> _tempAdvMask = new TrixProp<bool>();
        private readonly TrixProp<bool> _timeout = new TrixProp<bool>();
        private readonly TrixProp<string> _warningDescription = new TrixProp<string>();
        private readonly TrixProp<bool> _disableReplayCoin = new TrixProp<bool>();
        private readonly TrixProp<int> _coinCountReplay = new TrixProp<int>();
        private readonly TrixProp<int> _totalCoin = new TrixProp<int>();

        public bool DisableReplayCoin
        {
            get { return _disableReplayCoin.Value; }
            set { _disableReplayCoin.Value = value; }
        }

        public int CoinCountReplay
        {
            get { return _coinCountReplay.Value; }
            set { _coinCountReplay.Value = value; }
        }

        public int TotalCoin
        {
            get { return _totalCoin.Value; }
            set { _totalCoin.Value = value; }
        }

        public string Reward
        {
            get { return _reward.Value; }
            set { _reward.Value = value; }
        }
        public string EndChapterReward
        {
            get { return _endChapterReward.Value; }
            set { _endChapterReward.Value = value; }
        }
        public float Timer
        {
            get { return _timer.Value; }
            set { _timer.Value = value; }
        }
        public string Description
        {
            get { return _description.Value; }
            set { _description.Value = value; }
        }
        public BoardType BoardType
        {
            get { return _boardType.Value; }
            set
            {
                _boardType.Value = value;
                Controller.BoardType = value;
            }
        }
        public Sprite FirstChapterStar
        {
            get { return _firstChapterStar.Value; }
            set { _firstChapterStar.Value = value; }
        }
        public Sprite SecondChapterStar
        {
            get { return _secondChapterStar.Value; }
            set { _secondChapterStar.Value = value; }
        }
        public Sprite ThirdChapterStar
        {
            get { return _thirdChapterStar.Value; }
            set { _thirdChapterStar.Value = value; }
        }
        public int FirstMissionLoseCount
        {
            get { return _firstMissionLoseCount.Value; }
            set { _firstMissionLoseCount.Value = value; }
        }
        public int SecondMissionLoseCount
        {
            get { return _secondMissionLoseCount.Value; }
            set { _secondMissionLoseCount.Value = value; }
        }
        public int ThirdMissionLoseCount
        {
            get { return _thirdMissionLoseCount.Value; }
            set { _thirdMissionLoseCount.Value = value; }
        }
        public int ForthMissionLoseCount
        {
            get { return _forthMissionLoseCount.Value; }
            set { _forthMissionLoseCount.Value = value; }
        }
        public int FifthMissionLoseCount
        {
            get { return _fifthMissionLoseCount.Value; }
            set { _fifthMissionLoseCount.Value = value; }
        }
        public int SixthMissionLoseCount
        {
            get { return _sixthMissionLoseCount.Value; }
            set { _sixthMissionLoseCount.Value = value; }
        }
        public int SeventhMissionLoseCount
        {
            get { return _seventhMissionLoseCount.Value; }
            set { _seventhMissionLoseCount.Value = value; }
        }
        public int EighthMissionLoseCount
        {
            get { return _eighthMissionLoseCount.Value; }
            set { _eighthMissionLoseCount.Value = value; }
        }
        public int TotalShots
        {
            get { return _totalShots.Value; }
            set { _totalShots.Value = value; }
        }
        public int EnemiesKilled
        {
            get { return _enemiesKilled.Value; }
            set { _enemiesKilled.Value = value; }
        }
        public int InnocentCasualties
        {
            get { return _innocentCasualties.Value; }
            set { _innocentCasualties.Value = value; }
        }
        public float Accuracy
        {
            get { return _accuracy.Value; }
            set { _accuracy.Value = value; }
        }
        public bool ShowWarningPopup
        {
            get { return _showWarningPopup.Value; }
            set { _showWarningPopup.Value = value; }
        }
        public bool ShowPausePopup
        {
            get { return _showPausePopup.Value; }
            set { _showPausePopup.Value = value; }
        }
        public bool TempAdvMask
        {
            get { return _tempAdvMask.Value; }
            set { _tempAdvMask.Value = value; }
        }
        public bool Timeout
        {
            get { return _timeout.Value; }
            set
            {
                _timeout.Value = value;
                Controller.Timeout = value;
            }
        }
        public string WarningDescription
        {
            get { return _warningDescription.Value; }
            set { _warningDescription.Value = value; }
        }
        private Tweener LoseTweener { get; set; }
        protected override void Awake()
        {
            base.Awake();
            FirstChapterStar = TrixResource.Sprites.spr_NoImage;
            SecondChapterStar = TrixResource.Sprites.spr_NoImage;
            ThirdChapterStar = TrixResource.Sprites.spr_NoImage;
            Controller.OnIsShownChanged = OnIsShownChanged;
            Controller.HideViewAction = HideBoardView;
            Controller.ShowWarningPopupAction = ShowWarningPopupAction;
            Controller.ShowTempAdvViewAction = ShowTempAdvViewAction;
            Controller.ShowPauseMenuAction = ShowPauseMenuAction;

           
        }

        private void ShowPauseMenuAction()
        {
            ShowPausePopup = true;
        }

        private void ShowWarningPopupAction(int lastSaved)
        {
            WarningDescription = lastSaved <= 0 ? "ماموریتی ذخیره نشده است" : $"تا ماموريت {lastSaved} اين فصل ذخيره شده است";
            ShowWarningPopup = true;
        }

        private void ShowTempAdvViewAction()
        {
#if UNITY_EDITOR || UNITY_EDITOR_64
            TempAdvMask = true;
            _fakeVideoTimer = 3;
            LoseTweener.Kill();
            Timer = 100;
#endif
        }
#if UNITY_EDITOR || UNITY_EDITOR_64
        private void Update()
        {
            if (TempAdvMask)
            {
                Timer = _fakeVideoTimer * 100 / 3;
                _fakeVideoTimer -= Time.deltaTime;
                if (_fakeVideoTimer <= 0)
                {
                    TempAdvMask = false;
                }
            }
        }
#endif
        private void OnIsShownChanged(bool shown, object data)
        {
            if (shown)
            {
                _isEndChapter = false;
                var boardData = data as GameplayMessageData;
                if (boardData != null && boardData.GameplayMessageType == GameplayMessageType.MissionEnded)
                {
                    switch (boardData.EndGameBoardType)
                    {
                        case BoardType.None:
                            break;
                        case BoardType.EndChapter:
                            FillChapterStatistics();
                            TrixResource.AudioClips.Win_async(winAudio =>
                            {
                                if (winAudio)
                                {
                                    _audioId = TrixSoundManager.Instance.StartPlay(winAudio, Audio.AudioType.UiSound);
                                }
                            });
                            _isEndChapter = true;
                            break;
                        case BoardType.Lose:
                            CoinDataService.Instance.GetPlayerCoin(playerCoin =>
                            {
                                int needCoin = GameDataService.Instance.GetReplayCoinNeeded(GameManager.Instance.ChapterMission
                                                   .ChapterNumber) ?? 0;
                                if (playerCoin >= GameDataService.Instance.GetReplayCoinNeeded(GameManager.Instance.ChapterMission.ChapterNumber))
                                {
                                    DisableReplayCoin = true;
                                    CoinCountReplay = needCoin;
                                    TotalCoin = playerCoin;
                                }
                                else
                                DisableReplayCoin = false;
                            });
                            Timeout = false;
                            TrixResource.AudioClips.Lose_async(loseAudio =>
                            {
                                if (loseAudio)
                                {
                                    _audioId = TrixSoundManager.Instance.StartPlay(loseAudio, Audio.AudioType.UiSound);
                                }
                            });
                            LoseTweener = DOVirtual.Float(1, 0, 10f, result =>
                            {
                                Timer = result;
                            }).SetEase(Ease.Linear).OnComplete(() => { Timeout = true; });
                            break;
                        case BoardType.Win:
                            TrixResource.AudioClips.Win_async(winAudio =>
                            {
                                if (winAudio)
                                {
                                    _audioId = TrixSoundManager.Instance.StartPlay(winAudio, Audio.AudioType.UiSound);
                                }
                            });
                            Controller.WinRewardAction = SetWinReward;
                            Controller.SetWinReward(boardData.ChapterMissionInfo.ChapterNumber, boardData.ChapterMissionInfo.MissionNumber, boardData.WinReward);
                            break;
                    }

                    BoardType = _isEndChapter ? BoardType.Win : boardData.EndGameBoardType;
                    Description = boardData.Message;
                }
            }
            else
            {
                BoardType = BoardType.None;
                Controller.ResumeButtonClick();
            }
        }

        private void SetWinReward(int rewardAmount)
        {
            Reward = rewardAmount > 0 ? $"جایزه: {rewardAmount} سکه" : string.Empty;
        }
        private void SetEndChapterReward(int rewardAmount)
        {
            EndChapterReward = rewardAmount > 0 ? $"جایزه: {rewardAmount} سکه" : string.Empty;
        }

        
        public void OnCloseButtonClickEvent()
        {
            TrixSoundManager.Instance.StopPlay(_audioId, 1f);
            _audioId = 0;
            if (ShowPausePopup)
            {
                ShowPausePopup = false;
                Controller.ResumeButtonClick();
            }
            Controller.CloseButtonClick();
        }

        public void OnRetryButtonCoinClickEvent()
        {
            if (!DisableReplayCoin) return;
            
            TrixSoundManager.Instance.StopPlay(_audioId, 1f);
            _audioId = 0;
           
            var coin = 0;

            int needCoin = GameDataService.Instance.GetReplayCoinNeeded(GameManager.Instance.ChapterMission
                               .ChapterNumber) ?? 0;

            CoinDataService.Instance.GetPlayerCoin(playerCoin =>
            {
                coin = playerCoin-needCoin;
                
                DOVirtual.Float(playerCoin, coin, 1f, result =>
                {
                    TotalCoin = (int)result;
                }).SetEase(Ease.Linear);

                CoinDataService.Instance.DecreasePlayerCoin(needCoin, "Source");

                StartCoroutine(ExecuteAfterTime(1.7f, () =>
                {
                    Controller.RetryCoinButtonClick();
                }));
              
               
            });

            
        }

        IEnumerator ExecuteAfterTime(float time, Action task)
        {

            yield return new WaitForSeconds(time);
            task();

        }
        public void OnRetryButtonClickEvent()
        {
            TrixSoundManager.Instance.StopPlay(_audioId, 1f);
            _audioId = 0;
            if (ShowPausePopup)
            {
                ShowPausePopup = false;
                Controller.ResumeButtonClick();
            }
            //ProTODO: Check that above if need else for below method
            Controller.RetryButtonClick();


        }
        public void OnContinueButtonClickEvent()
        {
            if (_isEndChapter)
            {
                TrixSoundManager.Instance.StopPlay(_audioId);
                _audioId = 0;
                TrixResource.AudioClips.EndChapter_async(endChapter =>
                {
                    if (endChapter)
                    {
                        _audioId = TrixSoundManager.Instance.StartPlay(endChapter, Audio.AudioType.UiSound);
                    }
                });
                BoardType = BoardType.EndChapter;
            }
            else
            {
                TrixSoundManager.Instance.StopPlay(_audioId, 1f);
                _audioId = 0;
                Controller.ContinueButtonClick();
            }
        }
        public void OnSaveButtonClickEvent()
        {
            TrixSoundManager.Instance.StopPlay(_audioId, 1f);
            _audioId = 0;
            Controller.SaveProgressByAdvertise();
        }

        public void OnResumeButtonClickEvent()
        {
            TrixSoundManager.Instance.StopPlay(_audioId, 1f);
            _audioId = 0;
            ShowPausePopup = false;
            Controller.ResumeButtonClick();
        }
        public void OnWarningConfirmButtonClickEvent()
        {
            TrixSoundManager.Instance.StopPlay(_audioId, 1f);
            _audioId = 0;
            ShowWarningPopup = false;
            Controller.WarningConfirm();
        }
        public void OnWarningDenyButtonClickEvent()
        {
            TrixSoundManager.Instance.StopPlay(_audioId, 1f);
            _audioId = 0;
            ShowWarningPopup = false;
        }
        private void HideBoardView()
        {
            BoardType = BoardType.None;
            IsShown = false;
        }

        private void FillChapterStatistics()
        {
            var chapterStars = GameDataService.Instance.CalculateStarsCount(GameManager.Instance.ChapterMission.ChapterNumber, GameManager.Instance.MissionProgresses);
            var reward = GameDataService.Instance.GetEndChapterReward(GameManager.Instance.ChapterMission.ChapterNumber);
            SetEndChapterReward(reward);

            FirstChapterStar = chapterStars >= 1 ? TrixResource.Sprites.spr_ActiveStar : TrixResource.Sprites.spr_DeactiveStar;
            SecondChapterStar = chapterStars >= 2 ? TrixResource.Sprites.spr_ActiveStar : TrixResource.Sprites.spr_DeactiveStar;
            ThirdChapterStar = chapterStars >= 3 ? TrixResource.Sprites.spr_ActiveStar : TrixResource.Sprites.spr_DeactiveStar;
            TotalShots = GameManager.Instance.MissionProgresses.Sum(x => x.TotalShots);
            EnemiesKilled = GameManager.Instance.MissionProgresses.Sum(x => x.EnemiesKilled);
            InnocentCasualties = GameManager.Instance.MissionProgresses.Sum(x => x.InnocentCasualties);
            Accuracy = (float)GameManager.Instance.MissionProgresses.Sum(x => x.TotalHits) / TotalShots;
            var firstMission = GameManager.Instance.MissionProgresses.FirstOrDefault(x => x.MissionNumber == 1);
            var secondMission = GameManager.Instance.MissionProgresses.FirstOrDefault(x => x.MissionNumber == 2);
            var thirdMission = GameManager.Instance.MissionProgresses.FirstOrDefault(x => x.MissionNumber == 3);
            var forthMission = GameManager.Instance.MissionProgresses.FirstOrDefault(x => x.MissionNumber == 4);
            var fifthMission = GameManager.Instance.MissionProgresses.FirstOrDefault(x => x.MissionNumber == 5);
            var sixthMission = GameManager.Instance.MissionProgresses.FirstOrDefault(x => x.MissionNumber == 6);
            var seventhMission = GameManager.Instance.MissionProgresses.FirstOrDefault(x => x.MissionNumber == 7);
            var eighthMission = GameManager.Instance.MissionProgresses.FirstOrDefault(x => x.MissionNumber == 8);
            if (firstMission != null)
            {
                FirstMissionLoseCount = firstMission.LoseCount;
            }
            if (secondMission != null)
            {
                SecondMissionLoseCount = secondMission.LoseCount;
            }
            if (thirdMission != null)
            {
                ThirdMissionLoseCount = thirdMission.LoseCount;
            }
            if (forthMission != null)
            {
                ForthMissionLoseCount = forthMission.LoseCount;
            }
            if (fifthMission != null)
            {
                FifthMissionLoseCount = fifthMission.LoseCount;
            }
            if (sixthMission != null)
            {
                SixthMissionLoseCount = sixthMission.LoseCount;
            }
            if (seventhMission != null)
            {
                SeventhMissionLoseCount = seventhMission.LoseCount;
            }
            if (eighthMission != null)
            {
                EighthMissionLoseCount = eighthMission.LoseCount;
            }
        }
        protected override void OnBackPressed()
        {
        }
    }
}