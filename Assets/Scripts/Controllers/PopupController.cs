using System;
using System.Collections;
using System.Collections.Generic;
using Contracts;
using Models;
using UnityEngine;

namespace Controllers
{
    public class PopupController : MonoBehaviour
    {
        private static PopupController _instance = null;
        public static PopupController Instance => _instance;
        private bool _isDestroying;
        private Dictionary<Type, GameObject> PopupsDictionary { get; set; } = new Dictionary<Type, GameObject>();
        public bool IsAnyPopupShown => PopupsDictionary.Count > 0;
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        private void OnDestroy()
        {
            _instance = null;
            foreach (var popupGameObject in PopupsDictionary.Values)
            {
                try
                {
                    Destroy(popupGameObject);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            PopupsDictionary.Clear();
        }
        public void ShowPopup(Type popupController, GameObject popupGameObject, PopupData popupData)
        {
            StartCoroutine(ShowPopupWithDelay(popupController, popupGameObject, popupData));
        }
        public void ClosePopup(Type popupController)
        {
            StartCoroutine(ClosePopupWithDelay(popupController));
        }
        private IEnumerator ClosePopupWithDelay(Type popupController)
        {
            _isDestroying = true;
            yield return new WaitForSeconds(0.1f);
            GameObject popup;
            if (PopupsDictionary.TryGetValue(popupController, out popup))
            {
                try
                {
                    Destroy(popup);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                PopupsDictionary.Remove(popupController);
            }
            _isDestroying = false;
        }
        private IEnumerator ShowPopupWithDelay(Type popupController, GameObject popupGameObject, PopupData popupData)
        {
            while (_isDestroying)
            {
                yield return null;
            }
            if (popupGameObject)
            {
                var instantiatedGameObject = Instantiate(popupGameObject, transform);
                var controller = instantiatedGameObject.GetComponent<IPopupController>();
                if (controller != null)
                {
                    controller.InitializePopup(popupData);
                    PopupsDictionary.Add(popupController, instantiatedGameObject);
                }
                else
                {
                    Debug.LogError("Can't Find Popup GameObject");
                }
            }
            else
            {
                Debug.LogError("Popup GameObject Is Null");
            }
        }
    }
}
