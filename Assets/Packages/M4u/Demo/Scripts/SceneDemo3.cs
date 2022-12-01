//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace M4u
{
    /// <summary>
    /// SceneDemo3. New Bool CheckType, ColorBinding, SpecialBindings Demo
    /// </summary>
    public class SceneDemo3 : MonoBehaviour
	{
        public enum CheckType { OK, NG }

        public static readonly string CheckSuccess = "Success";
        public static readonly string CheckFail = "Fail";

        public M4uContextRoot ContextRoot;
        
        private M4uProperty<string> boolBindingText = new M4uProperty<string>();
        private M4uProperty<string> checkString = new M4uProperty<string>();
        private M4uProperty<CheckType> checkEnum = new M4uProperty<CheckType>();
		private M4uProperty<Color> color = new M4uProperty<Color>();
		private M4uProperty<string> text = new M4uProperty<string>();
        private M4uProperty<FontStyle> fontStyle = new M4uProperty<FontStyle>();
        private M4uProperty<int> fontSize = new M4uProperty<int>();
        private M4uProperty<float> lineSpacing = new M4uProperty<float>();
        private M4uProperty<bool> supportRichText = new M4uProperty<bool>();
        private M4uProperty<TextAnchor> alignment = new M4uProperty<TextAnchor>();
        private M4uProperty<bool> resizeTextForBestFit = new M4uProperty<bool>();
        
        public string BoolBindingText { get { return boolBindingText.Value; } set { boolBindingText.Value = value; } }
        public string CheckString { get { return checkString.Value; } set { checkString.Value = value; } }
        public CheckType CheckEnum { get { return checkEnum.Value; } set { checkEnum.Value = value; } }
		public Color Color { get { return color.Value; } set { color.Value = value; } }
		public string Text { get { return text.Value; } set { text.Value = value; } }
        public FontStyle FontStyle { get { return fontStyle.Value; } set { fontStyle.Value = value; } }
        public int FontSize { get { return fontSize.Value; } set { fontSize.Value = value; } }
        public float LineSpacing { get { return lineSpacing.Value; } set { lineSpacing.Value = value; } }
        public bool SupportRichText { get { return supportRichText.Value; } set { supportRichText.Value = value; } }
        public TextAnchor Alignment { get { return alignment.Value; } set { alignment.Value = value; } }
        public bool ResizeTextForBestFit { get { return resizeTextForBestFit.Value; } set { resizeTextForBestFit.Value = value; } }
        
        void Awake()
        {
            DemoContext.Instance.Demo3 = this;
            ContextRoot.Context = DemoContext.Instance;
            OnClickUpdate();
        }

        public void OnClickUpdate()
        {
            CheckString = (M4uUtil.Random(0, 2) == 0) ? CheckSuccess : CheckFail;
            CheckEnum = (M4uUtil.Random(0, 2) == 0) ? CheckType.OK : CheckType.NG;
            BoolBindingText = CheckString + "/" + CheckEnum.ToString();

			var time = Time.time;
			var src = Color;
			var dst = new UnityEngine.Color(M4uUtil.Random(0f, 1f), M4uUtil.Random(0f, 1f), M4uUtil.Random(0f, 1f), 1f);
			StartCoroutine(UpdateColor (time, src, dst));

            FontStyle = (UnityEngine.FontStyle)M4uUtil.Random(0, 4);
            FontSize = M4uUtil.Random(30, 50);
            LineSpacing = M4uUtil.Random(1f, 2f);
            SupportRichText = (M4uUtil.Random(0, 2) == 0);
            Alignment = (TextAnchor)M4uUtil.Random(3, 6);
            ResizeTextForBestFit = (M4uUtil.Random(0, 2) == 0);
            Text = "[Special]\n";
            Text += "FontStyle=" + FontStyle.ToString() + "\n";
            Text += "LineSpacing=" + LineSpacing + "\n";
            Text += "SupportRichText=" + SupportRichText + "\n";
            Text += "Alignment=" + Alignment.ToString() + "\n";
            Text += "ResizeTextForBestFit=" + ResizeTextForBestFit;
        }

		private IEnumerator UpdateColor(float time, Color src, Color dst)
		{
			float span = Time.time - time;
			if (span > 1f)
			{
				Color = dst;
				yield return null;
			}
			else
			{
				Color = new Color (src.r + (dst.r - src.r) * span, src.g + (dst.g - src.g) * span, src.b + (dst.b - src.b) * span, 1f);
				yield return null;
				StartCoroutine(UpdateColor (time, src, dst));
			}
		}
	}
}