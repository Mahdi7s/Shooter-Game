using System;
using System.Collections.Generic;
using Models.Constants;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Character
    {
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private List<AnimationData> _animations;
        public CharacterType CharacterType
        {
            get { return _characterType; }
            set { _characterType = value; }
        }

        public List<AnimationData> Animations
        {
            get { return _animations; }
            set { _animations = value; }
        }
    }
}
