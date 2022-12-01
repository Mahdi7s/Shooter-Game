using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class ChapterMission
    {
        [SerializeField] private int _chapterNumber;
        [SerializeField] private int _missionNumber;

        public ChapterMission(int chapterNumber, int missionNumber)
        {
            ChapterNumber = chapterNumber;
            MissionNumber = missionNumber;
        }

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
    }
}
