using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class ChaptersStars
    {
        [SerializeField] private int _chapterNumber;
        [SerializeField] private int _chapterStarCount;

        public int ChapterNumber
        {
            get { return _chapterNumber; }
            set { _chapterNumber = value; }
        }

        public int ChapterStarCount
        {
            get { return _chapterStarCount; }
            set { _chapterStarCount = value; }
        }
    }
}
