using System.Linq;

public static class StringExtensions
{
    public static string ToPersian(this string str, bool hindoNumbers = false)
    {
        str = str.Replace("ک", "ك").Replace("ی", "ي");
        // if(str.EndsWith("ي"))
        // 	str = str.Substring(0, str.Length-1) + "ی";

        return ArabicSupport.ArabicFixer.Fix(str, true, hindoNumbers).Replace("ﺃ", "آ").Replace("ﻲ", "ﯽ").Replace("ي", "ی");
    }
    public static bool IsRtl(this string str)
    {
        var isRtl = false;
        foreach (var _char in str)
        {
            if ((_char >= 1536 && _char <= 1791) || (_char >= 65136 && _char <= 65279))
            {
                isRtl = true;
                break;
            }
        }
        return isRtl;
    }
    public static string ToEnglish(this string str, bool hindoNumbers = false)
    {
        str = str
            .Replace("۰", "0").Replace("١", "1").Replace("۱", "1").
            Replace("۲", "2").Replace("۳", "3").
            Replace("۴", "4").Replace("۵", "5").
            Replace("۶", "6").Replace("۷", "7").
            Replace("۸", "8").Replace("۹", "9");
        // if(str.EndsWith("ي"))
        // 	str = str.Substring(0, str.Length-1) + "ی";

        return str;
    }
    public static string ToDesignEvent(this string eventAction)
    {
        return string.Join(":",
            eventAction.Split('.').Select(x => x.Length > 16 ? x.Substring(x.Length - 16, 16) : x).ToArray());
    }
}
