using UnityEngine;

public class HierarchyHighlighter : MonoBehaviour
{
    public static readonly Color DefaultBackgroundColor = new Color(0.76f, 0.76f, 0.76f, 1f);
    public static readonly Color DefaultBackgroundColorInactive = new Color(0.306f, 0.396f, 0.612f, 1f);
    public static readonly Color DefaultTextColor = Color.black;

    [Header("Active State")]
    public Color TextColor = DefaultTextColor;
    public FontStyle TextStyle = FontStyle.Normal;

    /// <summary>
    /// Color of the background.  Set alpha 0 to not draw a background period.
    /// </summary>
    public Color BackgroundColor = DefaultBackgroundColor;
    [Header("Inactive State")]
    public bool CustomInactiveColors = false;

    public Color TextColorInactive = DefaultTextColor;

    public FontStyle TextStyleInactive = FontStyle.Normal;
    /// <summary>
    /// Color of the background in an inactive state.  Set alpha 0 to not draw a background period.
    /// </summary>
    public Color BackgroundColorInactive = DefaultBackgroundColorInactive;

    public HierarchyHighlighter()
    {
        
    }
    public HierarchyHighlighter(Color inBackgroundColor)
    {
        this.BackgroundColor = inBackgroundColor;
    }

    public HierarchyHighlighter(Color inBackgroundColor, Color inTextColor, FontStyle inFontStyle = FontStyle.Normal)
    {
        this.BackgroundColor = inBackgroundColor;
        this.TextColor = inTextColor;
        this.TextStyle = inFontStyle;
    }
}