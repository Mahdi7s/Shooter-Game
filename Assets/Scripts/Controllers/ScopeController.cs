using Extensions;
using UnityEngine;
namespace Controllers
{
    /// <summary>
    /// ScopeController controls scope behaviors like wind effects and ...
    /// </summary>
    public class ScopeController : MonoBehaviour
    {
        /// <summary>
        /// holds an empty GameObject that specifies center of scope
        /// </summary>
        public GameObject NormalizedGridMarker;
        /// <summary>
        /// calculates target point to aim with existing wind effect. if there is wind with power = 1 from east, target point to aim is -1 on x axis.
        /// </summary>
        /// <returns>returns the point in world position that center of scope should aim to it in order to hit the target.</returns>
        public Vector3 GetRayCastOrigin()
        {
            var currentWeapon = MissionController.Instance.GetCurrentWeapon();
            //if there is no grid marker specified or weapon disables wind effect, so aim target is 0,0,0
            if (!NormalizedGridMarker || currentWeapon != null && currentWeapon.DisableWindEffect)
                return transform.TransformPoint(Vector3.zero);
            var windDirection = MissionController.Instance.WindDirection.ToVector2() * MissionController.Instance.WindPower;
            var windX = windDirection.x;
            var windY = windDirection.y;
            var offsetX = NormalizedGridMarker.transform.localPosition.x;
            var rayCastLocalPosition = new Vector3(offsetX * windX, offsetX * windY);
            var rayCastWorldPosition = transform.TransformPoint(rayCastLocalPosition);
            return rayCastWorldPosition;

        }   
    }
}
