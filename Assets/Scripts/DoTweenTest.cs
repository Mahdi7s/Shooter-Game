using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenTest : MonoBehaviour
{
    public List<Vector3> WayPoints;
    private TweenerCore<Vector3, Path, PathOptions> _tween;


    void Start()
    {
        _tween = transform.DOPath(WayPoints.ToArray(), 5).OnStepComplete(() => { Debug.LogError("loop completed"); })
                        .OnStart(() =>
                        {
                            Debug.LogError($"path length: {_tween.PathGetDrawPoints().Length}");
                        }).OnWaypointChange(i =>
                        {
                            Debug.LogError($"i={i} x={transform.position.x} y={transform.position.y} z={transform.position.z}");
                        }).SetEase(Ease.Linear).SetDelay(2).SetLoops(5, LoopType.Yoyo).SetOptions(false);
    }
}
