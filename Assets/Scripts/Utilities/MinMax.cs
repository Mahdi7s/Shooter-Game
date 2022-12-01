using System;
using UnityEngine;

[Serializable]
public class MinMax
{
    public float Min, Max;

    public MinMax(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public float Clamp(float value)
    {
        return Mathf.Clamp(value, Min, Max);
    }

    public float RandomValue
    {
        get { return UnityEngine.Random.Range(Min, Max); }
    }
}
