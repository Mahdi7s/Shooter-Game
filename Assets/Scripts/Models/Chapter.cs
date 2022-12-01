using System;
using Models.Constants;
using UnityEngine;

namespace Models
{
    /// <summary>
    /// keeps chapters data in a scriptable object like background, title, ...
    /// </summary>
    [Serializable]
    public class Chapter
    {
        [SerializeField] private string _title;
        [SerializeField] private int _number;
        [SerializeField] private GameplayBackgrounds _background;
        [Tooltip("Max Lose Count To Get One Star")]
        [SerializeField] private int _oneStarMaxLose;

        [Tooltip("Max Lose Count To Get Two Star")]
        [SerializeField] private int _twoStarMaxLose;

        [Tooltip("Max Lose Count To Get Three Star")]
        [SerializeField] private int _threeStarMaxLose;

        [Tooltip("Reward of Chapter")]
        [SerializeField] private int _endChapterReward;

        [Tooltip("needed Coin for Replay Mission")]
        [SerializeField] private int _replayCoin;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public int ReplayCoin
        {
            get { return _replayCoin; }
            set { _replayCoin = value; }
        }

        public GameplayBackgrounds Background
        {
            get { return _background; }
            set { _background = value; }
        }

        public int OneStarMaxLose
        {
            get { return _oneStarMaxLose; }
            set { _oneStarMaxLose = value; }
        }

        public int TwoStarMaxLose
        {
            get { return _twoStarMaxLose; }
            set { _twoStarMaxLose = value; }
        }

        public int ThreeStarMaxLose
        {
            get { return _threeStarMaxLose; }
            set { _threeStarMaxLose = value; }
        }
        public int EndChapterReward
        {
            get { return _endChapterReward; }
            set { _endChapterReward = value; }
        }
    }
}
