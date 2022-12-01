using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteTransition : MonoBehaviour
{
    public Image Target;
    public bool VisibleOnStart;
    public float ToVisibleDuration;
    public float ToInvisibleDuration;
    [MinMax(0, 10)]
    public MinMax VisibleDuration;
    [MinMax(0, 10)]
    public MinMax InvisibleDuration;
    private void Awake()
    {
        if (!Target)
        {
            Target = GetComponent<Image>();
        }
    }

    private void OnEnable()
    {
        Target.color = new Color(Target.color.r, Target.color.g, Target.color.b, VisibleOnStart ? 1 : 0);
        StartCoroutine(VisibleOnStart ? OffToOn() : OnToOff());
    }

    private void Start()
    {

    }

    private IEnumerator OnToOff()
    {
        yield return new WaitForSeconds(VisibleDuration.RandomValue);
        DOTween.ToAlpha(() => Target.color, x => Target.color = x, 0, ToInvisibleDuration).OnComplete(() =>
            {
                if (gameObject.activeInHierarchy && gameObject.activeSelf)
                    StartCoroutine(OffToOn());
            });
    }
    private IEnumerator OffToOn()
    {
        yield return new WaitForSeconds(InvisibleDuration.RandomValue);
        DOTween.ToAlpha(() => Target.color, x => Target.color = x, 1, ToVisibleDuration).OnComplete(() =>
        {
            if (gameObject.activeInHierarchy && gameObject.activeSelf)
                StartCoroutine(OnToOff());
        });
    }
}
