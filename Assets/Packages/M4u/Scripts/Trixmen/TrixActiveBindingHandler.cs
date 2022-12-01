using UnityEngine;
[ExecuteInEditMode]
public class TrixActiveBindingHandler : MonoBehaviour
{
    [SerializeField] [ReadOnly] private bool _isActiveOnEditMode;
    [SerializeField] private Vector3 _savedScale = Vector3.one;

    public Vector3 SavedScale
    {
        get { return _savedScale; }
        set { _savedScale = value; }
    }

    public bool IsActiveOnEditMode
    {
        get { return _isActiveOnEditMode; }
        set
        {
            _isActiveOnEditMode = value;
            if (!value)
            {
                SavedScale = transform.localScale;
            }
            transform.localScale = new Vector3(value ? SavedScale.x : 0, SavedScale.y, SavedScale.z);
        }
    }
}
