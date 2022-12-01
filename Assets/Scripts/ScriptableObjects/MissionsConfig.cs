using Models;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MissionConfig", menuName = "LevelDesigner/MissionConfig")]
    public class MissionsConfig : ScriptableObject
    {
        public List<Character> Characters;
        public List<Chapter> Chapters;
        public List<Mission> Missions;  
    }
}
