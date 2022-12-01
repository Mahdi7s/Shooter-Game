using DG.Tweening;
using Models;
using ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using TrixComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public class WheelsFortuneHandler : MonoBehaviour
    {
        [SerializeField] private Transform _prizeWheel;
        [SerializeField] private int _round;
        [SerializeField] private Ease _easeType = Ease.OutQuart;
        [SerializeField] [Range(1, 10)] private int _endValue;
        [SerializeField] private List<WheelsFortuneItem> _items;
        [SerializeField] private WheelsFortuneDataSet _wheelsFortune;
        private bool _initialized;
        public Transform PrizeWheel
        {
            get { return _prizeWheel; }
            set { _prizeWheel = value; }
        }
        public int Round
        {
            get { return _round; }
            set { _round = value; }
        }
        public Ease EaseType
        {
            get { return _easeType; }
            set { _easeType = value; }
        }
        public int EndValue
        {
            get { return _endValue; }
            set { _endValue = value; }
        }
        public List<WheelsFortuneItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }
        public WheelsFortuneDataSet WheelsFortune
        {
            get { return _wheelsFortune; }
            set { _wheelsFortune = value; }
        }

        public void Spin(Action<WheelsFortuneItem> itemNumber)
        {
            if (!_initialized)
            {
                itemNumber(null);
            }
            else
            {
                EndValue = Random.Range(1, Items.Count + 1);
                var duration = Round * 0.8f;
                var zAmount = Round * 360 + SlotNumberToDegree(EndValue) - PrizeWheel.transform.eulerAngles.z;
                PrizeWheel.rotation = new Quaternion(0, 0, 0, PrizeWheel.rotation.w);
                //TrixSoundManager.Instance.StartPlay(Audios.sfx_spinner_start, Audio.AudioType.UISound);
                PrizeWheel.DORotate(new Vector3(0, 0, zAmount), duration, RotateMode.LocalAxisAdd).SetEase(EaseType).OnComplete(() =>
                {
                    var item = Items.FirstOrDefault(x =>
                        x.ItemId == DegreeToSlotNumber(PrizeWheel.transform.eulerAngles.z));
                    if (item != null)
                    {
                        itemNumber(item);
                    }
                });
            }
        }

        private void Awake()
        {
            FillItems();
            _initialized = true;
        }

        public void FillItems()
        {
            if (!WheelsFortune)
                return;
            if (!PrizeWheel)
                return;
            PrizeWheel.rotation = new Quaternion(0, 0, 0, PrizeWheel.rotation.w);

            Items = new List<WheelsFortuneItem>(WheelsFortune.WheelsFortuneItems.Count);
            for (int i = 0; i < WheelsFortune.WheelsFortuneItems.Count; i++)
            {
                var wheelItem = new WheelsFortuneItem
                {
                    ItemId = i + 1
                };
                var itemData = WheelsFortune.WheelsFortuneItems.FirstOrDefault(x => x.ItemId == wheelItem.ItemId);
                if (itemData != null)
                {
                    var child = PrizeWheel.GetChild(i);
                    if (child)
                    {
                        var childText = child.GetComponentInChildren<TrixText>();
                        if (childText)
                            childText.text = itemData.ItemAmount.ToString();
                        wheelItem.RewardType = itemData.RewardType;
                        wheelItem.ItemAmount = itemData.ItemAmount;
                        Items.Add(wheelItem);
                    }
                }
            }
        }

        private int DegreeToSlotNumber(float degree)
        {
            var range = degree % 360;
            var num = (int)(Math.Ceiling(range / ((float)360 / (Items.Count * 2))) / 2);
            return num + 1;
        }

        private float SlotNumberToDegree(int slotNumber)
        {
            return (slotNumber - 1) * (360 / Items.Count);
        }
    }
}