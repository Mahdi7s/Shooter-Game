using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class SceneItem
    {
        [SerializeField] private int _orderInLayer;
        [SerializeField] private string _id;
        private int _missionId;
        [SerializeField] private string _name;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Quaternion _rotation;
        [SerializeField] private Vector3 _scale;
        public int OrderInLayer
        {
            get { return _orderInLayer; }
            set { _orderInLayer = value; }
        }
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int MissionId
        {
            get { return _missionId; }
            set { _missionId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // So We Can Load it from resources folder
        public GameObject Prefab
        {
            get { return _prefab; }
            set { _prefab = value; }
        }

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Quaternion Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Vector3 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public SceneItem()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
