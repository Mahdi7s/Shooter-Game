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
    [AddComponentMenu("M4u/TrixM4uActiveBinding")]
    public class TrixM4uActiveBinding : M4uBindingSingle
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

            SetActive(ui, (bool)Values[0]);
        }

        private void SetActive(Transform t, bool isActive)
        {
           
            if (t != null)
            {
                t.gameObject.SetActive(isActive); 
            }
        }

        public override string ToString()
        {
            return "isActive=" + GetBindStr(Path);
        }
    }
}