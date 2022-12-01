using TrixComponents;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

public class CustomMenu : MonoBehaviour
{
    [MenuItem("GameObject/Trix Components/Trix Button", false, 10)]
    private static void CreateTrixButton(MenuCommand menuCommand)
    {
        GameObject clickedGameobject = menuCommand.context as GameObject;
        GameObject parent = null;
        GameObject btn = new GameObject("Trix Button", typeof(CanvasRenderer), typeof(Image), typeof(TrixButton), typeof(Animator));

        btn.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animators/pref_global_button");

        TrixButton trixBtn = btn.GetComponent<TrixButton>();
        trixBtn.ClickSound = /*Audios.sfx_click;*/TrixResource.AudioClips.Click;// Resources.Load<AudioClip>("Audios/SFX/sfx_click");
        trixBtn.transition = Selectable.Transition.Animation;

        btn.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
        GameObject textGameobject = new GameObject("Trix Button Text", typeof(CanvasRenderer), typeof(TrixText));
        textGameobject.transform.parent = btn.transform;
        TrixText txt = textGameobject.GetComponent<TrixText>();
        txt.text = "Trix Button";
        txt.alignment = TextAnchor.MiddleCenter;
        txt.alignByGeometry = true;
        txt.color = Color.black;
        txt.fontSize = 15;
        txt.raycastTarget = false;
        txt.supportRichText = false;
        txt.font = Resources.Load<Font>("Fonts/fnt_lalezar");

        RectTransform textRectTransform = textGameobject.GetComponent<RectTransform>();
        textRectTransform.anchorMin = Vector2.zero;
        textRectTransform.anchorMax = Vector2.one;
        textRectTransform.sizeDelta = Vector2.zero;
        textRectTransform.offsetMin = new Vector2(0, 0);
        textRectTransform.offsetMax = new Vector2(0, 0);

        Image image = btn.GetComponent<Image>();
        image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        image.type = Image.Type.Sliced;

        parent = CheckForCanvas(clickedGameobject);

        GameObjectUtility.SetParentAndAlign(btn, parent);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(btn, "Create " + btn.name);
        Selection.activeObject = btn;
    }

    [MenuItem("GameObject/Trix Components/Trix Text", false, 11)]
    private static void CreateTrixText(MenuCommand menuCommand)
    {
        GameObject clickedGameobject = menuCommand.context as GameObject;
        GameObject parent = null;
        GameObject txt = new GameObject("Trix Text", typeof(CanvasRenderer), typeof(TrixText));
        txt.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
        TrixText trixText = txt.GetComponent<TrixText>();
        trixText.text = "New Text";
        trixText.alignment = TextAnchor.MiddleCenter;
        trixText.color = Color.black;
        trixText.fontSize = 15;
        trixText.raycastTarget = false;
        trixText.supportRichText = false;
        trixText.font = Resources.Load<Font>("Fonts/fnt_lalezar");

        parent = CheckForCanvas(clickedGameobject);

        GameObjectUtility.SetParentAndAlign(txt, parent);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(txt, "Create " + txt.name);
        Selection.activeObject = txt;
    }

    [MenuItem("GameObject/Trix Components/Trix NumText", false, 12)]
    private static void CreateTrixNumText(MenuCommand menuCommand)
    {
        GameObject clickedGameobject = menuCommand.context as GameObject;
        GameObject parent = null;
        GameObject txt = new GameObject("Trix NumText", typeof(CanvasRenderer), typeof(TrixNumText));
        txt.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
        TrixNumText trixNumText = txt.GetComponent<TrixNumText>();
        trixNumText.text = "New NumText";
        trixNumText.alignment = TextAnchor.MiddleCenter;
        trixNumText.color = Color.black;
        trixNumText.fontSize = 18;
        trixNumText.raycastTarget = false;
        trixNumText.supportRichText = false;
        trixNumText.font = Resources.Load<Font>("Fonts/fnt_lalezar");

        parent = CheckForCanvas(clickedGameobject);

        GameObjectUtility.SetParentAndAlign(txt, parent);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(txt, "Create " + txt.name);
        Selection.activeObject = txt;
    }

    [MenuItem("GameObject/Trix Components/Trix InputField", false, 13)]
    private static void CreateTrixInputField(MenuCommand menuCommand)
    {
        GameObject clickedGameobject = menuCommand.context as GameObject;
        GameObject parent = null;
        GameObject input = new GameObject("Trix InputField", typeof(CanvasRenderer), typeof(Image), typeof(TrixInputField));

        input.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
        GameObject childPlaceholder = new GameObject("Trix Placeholder", typeof(CanvasRenderer), typeof(TrixText));
        GameObject childText = new GameObject("Trix Text", typeof(CanvasRenderer), typeof(TrixText));
        childPlaceholder.transform.parent = input.transform;
        childText.transform.parent = input.transform;

        Image image = input.GetComponent<Image>();
        image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/InputFieldBackground.psd");
        image.type = Image.Type.Sliced;

        TrixText txt = childText.GetComponent<TrixText>();
        txt.color = Color.black;
        txt.fontSize = 11;
        txt.raycastTarget = false;
        txt.supportRichText = false;
        txt.alignment = TextAnchor.MiddleRight;
        txt.font = Resources.Load<Font>("Fonts/fnt_lalezar");

        TrixText placeholder = childPlaceholder.GetComponent<TrixText>();
        placeholder.text = "متن را وارد کنید...";
        placeholder.fontSize = 11;
        placeholder.fontStyle = FontStyle.Italic;
        placeholder.alignment = TextAnchor.MiddleRight;
        placeholder.font = Resources.Load<Font>("Fonts/fnt_lalezar");

        Color placeholderColor = txt.color;
        placeholderColor.a *= 0.5f;
        placeholder.color = placeholderColor;

        RectTransform textRectTransform = childText.GetComponent<RectTransform>();
        textRectTransform.anchorMin = Vector2.zero;
        textRectTransform.anchorMax = Vector2.one;
        textRectTransform.sizeDelta = Vector2.zero;
        textRectTransform.offsetMin = new Vector2(10, 6);
        textRectTransform.offsetMax = new Vector2(-10, -7);

        RectTransform placeholderRectTransform = childPlaceholder.GetComponent<RectTransform>();
        placeholderRectTransform.anchorMin = Vector2.zero;
        placeholderRectTransform.anchorMax = Vector2.one;
        placeholderRectTransform.sizeDelta = Vector2.zero;
        placeholderRectTransform.offsetMin = new Vector2(10, 6);
        placeholderRectTransform.offsetMax = new Vector2(-10, -7);

        input.GetComponent<TrixInputField>().textComponent = txt;
        input.GetComponent<TrixInputField>().placeholder = placeholder;


        parent = CheckForCanvas(clickedGameobject);

        GameObjectUtility.SetParentAndAlign(input, parent);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(input, "Create " + input.name);
        Selection.activeObject = input;
    }

    [MenuItem("GameObject/Trix Components/Trix Toggle", false, 14)]
    private static void CreateTrixToggle(MenuCommand menuCommand)
    {
        GameObject clickedGameobject = menuCommand.context as GameObject;
        GameObject parent = null;
        GameObject btn = new GameObject("Trix Toggle", typeof(TrixToggle), typeof(Animator));

        btn.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animators/pref_global_button");

        TrixToggle trixTggle = btn.GetComponent<TrixToggle>();
        trixTggle.Animator = btn.GetComponent<Animator>();
        trixTggle.ClickSound = /*Audios.sfx_click;*/TrixResource.AudioClips.Click;// Resources.Load<AudioClip>("Audios/SFX/sfx_click");
        trixTggle.transition = Selectable.Transition.Animation;

        btn.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 20);
        GameObject toggleOn = new GameObject("Toggle ON", typeof(CanvasRenderer), typeof(Image));
        GameObject toggleOff = new GameObject("Toggle OFF", typeof(CanvasRenderer), typeof(Image));
        toggleOn.transform.parent = btn.transform;
        toggleOff.transform.parent = btn.transform;
        Image onImage = toggleOn.GetComponent<Image>();
        Image offImage = toggleOff.GetComponent<Image>();
        onImage.sprite = null;
        onImage.color = Color.green;
        offImage.sprite = null;
        offImage.color = Color.red;

        RectTransform toggleOnRectTransform = toggleOn.GetComponent<RectTransform>();
        toggleOnRectTransform.anchorMin = Vector2.zero;
        toggleOnRectTransform.anchorMax = Vector2.one;
        toggleOnRectTransform.sizeDelta = Vector2.zero;
        toggleOnRectTransform.offsetMin = new Vector2(0, 0);
        toggleOnRectTransform.offsetMax = new Vector2(0, 0);

        RectTransform toggleOffRectTransform = toggleOff.GetComponent<RectTransform>();
        toggleOffRectTransform.anchorMin = Vector2.zero;
        toggleOffRectTransform.anchorMax = Vector2.one;
        toggleOffRectTransform.sizeDelta = Vector2.zero;
        toggleOffRectTransform.offsetMin = new Vector2(0, 0);
        toggleOffRectTransform.offsetMax = new Vector2(0, 0);

        GameObject handle = new GameObject("Handle", typeof(CanvasRenderer), typeof(Image));
        handle.transform.parent = btn.transform;
        Image handleImage = handle.GetComponent<Image>();
        handleImage.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd"); ;

        RectTransform handleRectTransform = handle.GetComponent<RectTransform>();
        handleRectTransform.anchorMin = new Vector2(0, 0.5f);
        handleRectTransform.anchorMax = new Vector2(0, 0.5f);
        handleRectTransform.pivot = new Vector2(0, 0.5f);
        handleRectTransform.offsetMin = new Vector2(0, 0);
        handleRectTransform.offsetMax = new Vector2(0, 0);
        handleRectTransform.sizeDelta = new Vector2(20, 20);

        parent = CheckForCanvas(clickedGameobject);

        GameObjectUtility.SetParentAndAlign(btn, parent);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(btn, "Create " + btn.name);
        Selection.activeObject = btn;
    }

    private static GameObject CheckForCanvas(GameObject clickedGameobject)
    {
        if (clickedGameobject && clickedGameobject.GetComponentInParent<Canvas>())
        {
            return clickedGameobject;
        }
        else
        {
            Canvas cnv = (Canvas)FindObjectOfType(typeof(Canvas));
            if (cnv)
            {
                return cnv.gameObject;
            }
            else
            {
                GameObject eventSystem = null;
                EventSystem evntSys = (EventSystem)FindObjectOfType(typeof(EventSystem));
                if (evntSys)
                {
                    eventSystem = evntSys.gameObject;
                }
                else
                {
                    eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
                }
                GameObject canvasParent = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                canvasParent.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                return canvasParent;
            }
        }
    }

}
