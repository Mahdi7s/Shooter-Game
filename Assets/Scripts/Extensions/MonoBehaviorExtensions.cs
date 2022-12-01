using UnityEngine;

namespace Extensions
{
    public static class MonoBehaviourExtension
    {
        public static void SetTransform(this Transform targetTransform, Transform sourceTransform)
        {
            if (sourceTransform != null)
            {
                sourceTransform.localRotation = sourceTransform.localRotation;
                targetTransform.localPosition = sourceTransform.localPosition;
                targetTransform.localScale = sourceTransform.localScale;
            }
        }
    }
}