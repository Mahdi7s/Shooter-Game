using UnityEngine;

public class MinMaxAttribute : PropertyAttribute
{
    public float Min, Max;
    public MinMaxAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}
