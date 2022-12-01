using Models;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameplayAudios", menuName = "Datasets/GameplayAudiosDataSet", order = 7)]
    public class GameplayAudiosDataSet : ScriptableObject
    {
        public List<GameplayAudio> GameplayAudios;
    }


}
