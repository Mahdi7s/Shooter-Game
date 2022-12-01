using System;
using System.Collections.Generic;
using Models.Constants;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class GameplayAudio
    {
        [Range(1, 10)]
        [SerializeField] private int _chapterNumber;
        [Range(0, 8)]
        [SerializeField] private int _missionNumber;
        [SerializeField] private GameplayMusics _music;
        [SerializeField] private List<GameplayEnvironments> _environments = new List<GameplayEnvironments>();

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

        public GameplayMusics Music
        {
            get { return _music; }
            set { _music = value; }
        }

        public List<GameplayEnvironments> Environments
        {
            get { return _environments; }
            set { _environments = value; }
        }
    }
}
