using Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers
{
    /// <summary>
    /// controls shooting behavior and scope movement
    /// </summary>
    public class ShooterController : MonoBehaviour
    {
        private bool _isMouseHeldDown;
        public GameObject Scope; //reference to scope object
        public GameObject CancelShotArea;
        private EnemyController[] _targets;
        private Vector3 _firstTouchPosition; //holds position of where user touches at first touch while aiming
        private Vector3 _touchOffset; //difference between where user has touched at first and scope's position 
        private Vector3 _minScreenBounds;
        private Vector3 _maxScreenBounds;

        private ScopeController _scopeController; //reference to scope controller
        // Use this for initialization
        private void Start()
        {
            _minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            _maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            _scopeController = Scope.GetComponent<ScopeController>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameManager.Instance.IsPaused)
                return;
            if (!GameManager.Instance.IsInGameplay)
                return;
            if (IsPointerOverGameObject() /*&& !GameManager.Instance.IsInCancelArea*/) //if pointer is on a GUI element, then it's not gameplay mode
                return;
            if (Input.GetMouseButtonDown(0))
            {
                _isMouseHeldDown = true;
                _firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _touchOffset = Scope.transform.position - _firstTouchPosition;
            }
            if (Input.GetMouseButton(0))
            {
                SetScopeVisibility(true);
                MissionController.Instance.AimingStarted();
                var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) * 4 + _touchOffset * 4;
                Scope.transform.position = Vector2.Lerp(Scope.transform.position,
                    new Vector3(Mathf.Clamp(targetPos.x, _minScreenBounds.x, _maxScreenBounds.x), Mathf.Clamp(targetPos.y, _minScreenBounds.y, _maxScreenBounds.y), Mathf.Clamp(targetPos.z, _minScreenBounds.z, _maxScreenBounds.z))
                    , Time.deltaTime * 3);
            }
            if (Input.GetMouseButtonUp(0) && _isMouseHeldDown)
            {
                _isMouseHeldDown = false;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

                if (hit && hit.transform)
                {
                    if (hit.transform.CompareTag("CancelArea"))
                    {
                        SetScopeVisibility(false);
                        ResetScope();
                        MissionController.Instance.ShotCanceled();
                        return;
                    }
                }

                var aimedPosition = _scopeController.GetRayCastOrigin();
                SetScopeVisibility(false);
                MissionController.Instance.ShotFired(aimedPosition);
                Debug.Log("shoot!");
                var raycastHit = Physics2D.Raycast(aimedPosition, Vector3.forward);
                if (raycastHit.collider != null && raycastHit.collider.gameObject.GetComponentInParent<EnemyController>())
                {
                    Debug.Log("hit!");
                    raycastHit.collider.gameObject.GetComponentInParent<EnemyController>().Hit();
                }
                else
                {
                    MissionController.Instance.ShotMissed();
                }
                ResetScope();
            }
        }

        private void SetScopeVisibility(bool isActive)
        {
            Scope.SetActive(isActive);
            if (CancelShotArea)
                CancelShotArea.SetActive(isActive);
        }
        private bool IsPointerOverGameObject()
        {
            //check mouse
            if (EventSystem.current.IsPointerOverGameObject())
                return true;

            //check touch
            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                    return true;
            }
            return false;
        }
        private void ResetScope()
        {
            if (GameManager.Instance.IsPaused)
                return;
            Scope.transform.position = Vector3.zero;
        }

    }
}
