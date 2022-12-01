//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

namespace M4u
{
    /// <summary>
	/// SceneDemo5. EventBinding Demo
    /// </summary>
	public class SceneDemo5 : MonoBehaviour
	{
        public M4uContextRoot ContextRoot;
		public Transform EventTrigger;

		private M4uProperty<List<MonsterData>> monsters = new M4uProperty<List<MonsterData>>(new List<MonsterData>());

		public List<MonsterData> Monsters { get { return monsters.Value; } set { monsters.Value = value; } }

        void Awake()
        {
			DemoContext.Instance.Demo5 = this;
            ContextRoot.Context = DemoContext.Instance;
        }

		void Start()
		{
			for (int i = 0; i < 5; i++)
			{
				var monster = new MonsterData ();
				monster.Name = "Goblin" + i;
				monster.Texture = Resources.Load("Texture/monster") as Texture;
				Monsters.Add (monster);
			}
		}

		void OnButton()
		{
			Debug.Log ("OnButton");
		}

		void OnToggle(bool value)
		{
			Debug.Log ("OnToggle:" + value);
		}

		void OnSlider(float value)
		{
			Debug.Log ("OnSlider:" + value);
		}

		void OnScrollbar(float value)
		{
			Debug.Log ("OnScrollbar:" + value);
		}

		void OnDropdown(int value)
		{
			Debug.Log ("OnDropdown:" + value);
		}

		void OnInputField(string value)
		{
			Debug.Log ("OnInputField:" + value);
		}

		void OnScrollView(Vector2 value)
		{
			Debug.Log ("OnScrollView:" + value);
		}

		void OnEventTriggerPointerDown(BaseEventData value)
		{
			Debug.Log ("OnEventTriggerPointerDown:" + value);
		}

		void OnEventTriggerDrag(BaseEventData value)
		{
			Debug.Log ("OnEventTriggerDrag:" + value);
			EventTrigger.transform.position = Input.mousePosition;
		}

		void OnEventTriggerPointerUp(BaseEventData value)
		{
			Debug.Log ("OnEventTriggerPointerUp:" + value);
		}
	}
}