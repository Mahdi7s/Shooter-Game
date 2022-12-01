using System;
using Models.Constants;
using UnityEngine;
using System.Collections.Generic;

namespace Models
{
    /// <summary>
    /// keeps animation data in scriptable object like what sound to play on each animation
    /// </summary>
    [Serializable]
    
    public class AnimationData
    {
        [SerializeField] private AnimationNames _animationName;
        [SerializeField] private List<AudioClip> _audioClips;
        [SerializeField] private bool _loopAudio;
        [SerializeField] private bool _playRandom;
        public List<AudioClip> AudioClips
        {
            get { return _audioClips; }
            set { _audioClips = value; }
        }
        public bool LoopAudio
        {
            get { return _loopAudio; }
            set { _loopAudio = value; }
        }

        public AnimationNames AnimationName
        {
            get { return _animationName; }
            set { _animationName = value; }
        }

        public bool PlayRandom
        {
            get
            {
                return _playRandom;
            }

            set
            {
                _playRandom = value;
            }
        }
    }
}
