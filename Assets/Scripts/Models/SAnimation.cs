using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class SAnimation
    {
        [SerializeField] private string _skeletonDataAssetName;
        [SerializeField] private string _initialSkinName;
        [SerializeField] private int _sortingLayerId;
        [SerializeField] private int _orderInLayer;
        [SerializeField] private string _animationName;
        [SerializeField] private bool _loop;
        [SerializeField] private float _timeScale;

        public string SkeletonDataAssetName
        {
            get { return _skeletonDataAssetName; }
            set { _skeletonDataAssetName = value; }
        }

        public string InitialSkinName
        {
            get { return _initialSkinName; }
            set { _initialSkinName = value; }
        }

        public int SortingLayerId
        {
            get { return _sortingLayerId; }
            set { _sortingLayerId = value; }
        }

        public int OrderInLayer
        {
            get { return _orderInLayer; }
            set { _orderInLayer = value; }
        }

        public string AnimationName
        {
            get { return _animationName; }
            set { _animationName = value; }
        }

        public bool Loop
        {
            get { return _loop; }
            set { _loop = value; }
        }

        public float TimeScale
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }
    }

    public static class SkeletonAnimExtensions
    {
        public static SAnimation ToModel(this SkeletonAnimation skeletonAnimation)
        {
            var sAnim = new SAnimation
            {
                SkeletonDataAssetName = skeletonAnimation.SkeletonDataAsset.name,
                InitialSkinName = skeletonAnimation.initialSkinName,
                OrderInLayer = skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder,
                SortingLayerId = skeletonAnimation.GetComponent<MeshRenderer>().sortingLayerID,
                AnimationName = skeletonAnimation.AnimationName,
                Loop = skeletonAnimation.loop,
                TimeScale = skeletonAnimation.timeScale
            };
            return sAnim;
        }

        public static void AddToGameObject(this SAnimation sAnimation, GameObject gameObject)
        {
            if(sAnimation == null || !gameObject)
                return;
        
//        var retval = new SkeletonAnimation
//        {
//            initialSkinName = sAnimation.InitialSkinName,
//            AnimationName = sAnimation.AnimationName,
//            loop = sAnimation.Loop,
//            timeScale = sAnimation.TimeScale
//        };
            SkeletonDataAsset skeletonDataAsset = Resources.Load("Spine/Barbarian/"+sAnimation.SkeletonDataAssetName) as SkeletonDataAsset;
            //skeletonDataAsset.skeletonJSON = new TextAsset() { name = "Sag" };
            //skeletonDataAsset.
            var skeletonAnim = SkeletonAnimation.AddToGameObject(gameObject, skeletonDataAsset);
            skeletonAnim.initialSkinName = sAnimation.InitialSkinName;
            skeletonAnim.AnimationName = sAnimation.AnimationName;
            skeletonAnim.loop = sAnimation.Loop;
            skeletonAnim.timeScale = sAnimation.TimeScale;
            var meshRenderer = skeletonAnim.GetComponent<MeshRenderer>();
            meshRenderer.sortingOrder = sAnimation.OrderInLayer;
            meshRenderer.sortingLayerID = sAnimation.SortingLayerId;

        }
    }
}