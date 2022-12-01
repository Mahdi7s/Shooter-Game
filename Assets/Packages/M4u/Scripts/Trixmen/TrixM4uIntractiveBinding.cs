using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace M4u
{
    /// <summary>
    /// TrixM4uColorBinding. Bind Color without child color bind
    /// </summary>
    [AddComponentMenu("M4u/TrixIntractiveBinding")]
    public class TrixM4uIntractiveBinding : M4uBindingSingle
    {
        private Transform ui = null;

        public override void Start()
        {
            base.Start();

            ui = transform;
            OnChange();
        }

        public override void OnChange()
        {
            base.OnChange();

            SetInteractable(ui, (bool)Values[0]);
        }

        private void SetInteractable(Transform t, bool interactable)
        {
            var g = t.GetComponent<Button>();
            if (g != null)
            {
                g.interactable = interactable;
            }
        }

        public override string ToString()
        {
            return "Interactable=" + GetBindStr(Path);
        }
    }
}