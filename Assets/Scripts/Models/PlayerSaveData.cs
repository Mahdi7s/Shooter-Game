using System;
using System.Collections.Generic;

namespace Models
{
    public class PlayerSaveData
    {
        public int PlayerCoin { get; set; }
        public DateTime WheelsFortune { get; set; } 
        public int OrangeCount { get; set; } 
        public DateTime OrangeDate { get; set; }
        public int BoosterPower { get; set; }
        public List<ChapterMission> RewardedMissions { get; set; }
        public CompletedMissions CompletedMissions { get; set; }
        public int HighestChapterFinished { get; set; } 
        public PlayerWeapons PlayerWeapons { get; set; }
    }
}
