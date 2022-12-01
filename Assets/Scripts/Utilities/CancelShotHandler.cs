using Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities
{
    public class CancelShotHandler : MonoBehaviour, IPointerEnterHandler//, IPointerExitHandler
    {
        private bool _isInGameObject;
        //private bool IsDragging { get; set; } = false;
        private void Update()
        {
            if (_isInGameObject)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

                if (hit && hit.transform)
                {
                    if (hit.transform.GetComponent<CancelShotHandler>())
                    {
                        Debug.LogError("In Area");
                    }
                    else
                    {
                        Debug.LogError("PointerExit");
                        _isInGameObject = false;
                        //GameManager.Instance.IsInCancelArea = false;
                    }
                }
                else
                {
                    Debug.LogError("Hit Is Null");
                    _isInGameObject = false;
                    //GameManager.Instance.IsInCancelArea = false;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.LogError("PointerEnter");
            _isInGameObject = true;
            //GameManager.Instance.IsInCancelArea = true;
        }

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    Debug.LogError("PointerExit");
        //    GameManager.Instance.IsInCancelArea = false;
        //}
    }
}
