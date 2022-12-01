using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class CompletedMissions
    {
        [SerializeField] private List<MissionProgress> _missionProgresses = new List<MissionProgress>();
        [SerializeField] private List<ChaptersStars> _chaptersStars = new List<ChaptersStars>();

        public List<MissionProgress> MissionProgresses
        {
            get { return _missionProgresses; }
            set { _missionProgresses = value; }
        }

        public List<ChaptersStars> ChaptersStars
        {
            get { return _chaptersStars; }
            set { _chaptersStars = value; }
        }
    }
}
