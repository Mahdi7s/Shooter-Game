using System;
using Models;
using Models.Constants;

namespace Messaging.MessageData
{
    public class GameplayMessageData
    {
        public GameplayMessageType GameplayMessageType { get; set; }
        public int FireLimit { get; set; }
        public float Timeout { get; set; }
        public bool HasWind { get; set; }
        public WindDirectionSprite WindDirection { get; set; }
        public float WindPower { get; set; }
        public string Message { get; set; }
        public BoardType EndGameBoardType { get; set; }
        public int WinReward { get; set; }
        public ChapterMission ChapterMissionInfo { get; set; }
    }
}