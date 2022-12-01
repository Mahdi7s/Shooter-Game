using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/RtlText")]
public class RtlText : Text
{
    private const char LineEnding = '\n';


    public string BaseText
    {
        get { return base.text; }
    }

    public override string text
    {
        get
        {
            return ToRtlText(this, BaseText);
        }
        set 
        { 
            base.text = value;
        }
    }

    public static string ToRtlText(Text txt, string baseText = null)
    {
        // Populate base text in rect transform and calculate number of lines.
        baseText = baseText ?? txt.text;
        txt.cachedTextGenerator.Populate(baseText, txt.GetGenerationSettings(txt.rectTransform.rect.size));
        // Make list of lines
        List<UILineInfo> lines = txt.cachedTextGenerator.lines as List<UILineInfo>;
        if (lines == null) return null;
        string linedText = "";
        for (int i = 0; i < lines.Count; i++)
        {
            // Find Start and Length of RTL line and append Line Ending character.
            if (i < lines.Count - 1)
            {
                int startIndex = lines[i].startCharIdx;
                int length = lines[i + 1].startCharIdx - lines[i].startCharIdx;
                linedText += baseText.Substring(startIndex, length);
                if (linedText.Length > 0 &&
                    linedText[linedText.Length - 1] != '\n' &&
                    linedText[linedText.Length - 1] != '\r')
                {
                    linedText += LineEnding;
                }
            }
            else
            {
                // For the Last line, we only need startIndex and line continues to the end.
                linedText += baseText.Substring(lines[i].startCharIdx);
                //if (resizeTextForBestFit) linedText += '\n';
            }
        }
        return linedText.ToPersian(true);
    }
}