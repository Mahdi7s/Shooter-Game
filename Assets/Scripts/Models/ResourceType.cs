using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class ResourceType
    {
        [SerializeField] private string _type = string.Empty;

        [Tooltip("Separate with | if more than one ")]
        [SerializeField] private string _extension = string.Empty;

        [Tooltip("Separate with | if more than one ")]
        [SerializeField] private string _prefix = string.Empty;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Extension
        {
            get { return _extension; }
            set { _extension = value; }
        }

        public string Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }
    }
}
