using System;
using System.Collections;
using System.Collections.Generic;
using Models.Constants;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class WayPoint
    {
        [SerializeField] private Vector2 _position;
        [SerializeField] private Vector2 _rotation;
        [SerializeField] private Vector2 _scale;
        [SerializeField] private bool _shouldFlip;
        [SerializeField] private float _delay;
        [SerializeField] private AnimationNames _sAnimationState;
        [SerializeField] private AnimationNames _sAnimationStateDelay;

        public AnimationNames SAnimationState
        {
            get { return _sAnimationState; }
            set { _sAnimationState = value; }
        }


        public AnimationNames SAnimationStateDelay
        {
            get { return _sAnimationStateDelay; }
            set { _sAnimationStateDelay = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Vector2 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public bool ShouldFlip
        {
            get { return _shouldFlip; }
            set { _shouldFlip = value; }
        }

        public float Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }
    }

    public static class WayPointExtensions
    {
        public static WayPoint ToWayPoint(this WaypointController wayPointController)
        {
            if (!wayPointController)
                return null;
            var data = new WayPoint
            {
                Position = wayPointController.transform.localPosition,
                Rotation = wayPointController.transform.rotation.eulerAngles,
                Scale = wayPointController.transform.localScale,
                ShouldFlip = wayPointController.ShouldFlip,
                Delay = wayPointController.Delay,
                SAnimationState = wayPointController.SAnimationState,
                SAnimationStateDelay = wayPointController.SAnimationStateDelay
            };
            return data;
        }

        public static void SetTransform(this WaypointController wayPointController, WayPoint wayPoint)
        {
            if(!wayPointController.gameObject)
            {
#if UNITY_EDITOR
                Debug.LogWarning("wayPointController gameObject is null, ignoring...");
#endif
                return;
            }
            wayPointController.gameObject.transform.SetPositionAndRotation(wayPoint.Position, Quaternion.Euler(wayPoint.Rotation));
            wayPointController.gameObject.transform.localPosition = wayPoint.Position;
            wayPointController.gameObject.transform.localScale = wayPoint.Scale;
        }
    }
}