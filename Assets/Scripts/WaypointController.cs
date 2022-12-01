using Models.Constants;
using Sirenix.OdinInspector;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointController : SerializedMonoBehaviour
{
    [SuffixLabel("seconds", Overlay = false)]
    public float Delay;
    public bool ShouldFlip;
    public AnimationNames SAnimationState;
    public AnimationNames SAnimationStateDelay;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position,new Vector3(1,0.2f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, new Vector3(1, 0.2f));
    }

    private void Start()
    {
        if (Application.isPlaying) return;
        var enemyController = gameObject.GetComponentInParent<EnemyController>();
        if (enemyController)
        {
            if (!enemyController.WalkPoints.Contains(this))
            {
                //Debug.LogError("attaching waypoint to enemy");
                enemyController.WalkPoints.Add(this);
            }
        }
    }

    private void OnDestroy()
    {
        if (Application.isPlaying) return;
        var enemyController = gameObject.GetComponentInParent<EnemyController>();
        if (enemyController)
        {
            //Debug.LogError("removing waypoint from enemy");
            enemyController.WalkPoints.Remove(this);
        }
    }
}
