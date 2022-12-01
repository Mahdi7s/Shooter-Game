using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class MissionProgress
    {
        [SerializeField] private int _chapterNumber;
        [SerializeField] private int _missionNumber;
        [SerializeField] private int _loseCount;
        [SerializeField] private int _totalShots;
        [SerializeField] private int _missedShots;
        [SerializeField] private int _innocentCasualties;
        [SerializeField] private int _enemiesKilled;
        [SerializeField] private int _totalHits;

        public int ChapterNumber
        {
            get { return _chapterNumber; }
            set { _chapterNumber = value; }
        }

        public int MissionNumber
        {
            get { return _missionNumber; }
            set { _missionNumber = value; }
        }

        public int LoseCount
        {
            get { return _loseCount; }
            set { _loseCount = value; }
        }

        public int TotalShots
        {
            get { return _totalShots; }
            set { _totalShots = value; }
        }

        public int MissedShots
        {
            get { return _missedShots; }
            set { _missedShots = value; }
        }

        public int InnocentCasualties
        {
            get { return _innocentCasualties; }
            set { _innocentCasualties = value; }
        }

        public int EnemiesKilled
        {
            get { return _enemiesKilled; }
            set { _enemiesKilled = value; }
        }

        public int TotalHits
        {
            get { return _totalHits; }
            set { _totalHits = value; }
        }
    }
}
