//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace M4u
{
    /// <summary>
	/// SceneDemo4. CollectionBinding Demo
    /// </summary>
    public class SceneDemo4 : MonoBehaviour
	{
        public M4uContextRoot ContextRoot;

		private M4uProperty<int> monsterCount = new M4uProperty<int>();
		private M4uProperty<string[]> monsterNames = new M4uProperty<string[]> ();
        private M4uProperty<List<MonsterData>> monsters = new M4uProperty<List<MonsterData>>(new List<MonsterData>());
		private List<GameObject> monsterList = new List<GameObject>();
		private Dictionary<MonsterData, GameObject> monsterDictionary = new Dictionary<MonsterData, GameObject>();

		public int MonsterCount { get { return monsterCount.Value; } set { monsterCount.Value = value; } }
		public string[] MonsterNames { get { return monsterNames.Value; } set { monsterNames.Value = value; } }
		public List<MonsterData> Monsters { get { return monsters.Value; } set { monsters.Value = value; } }
		public List<GameObject> MonsterList { get { return monsterList; } set { monsterList = value; } }
		public Dictionary<MonsterData, GameObject> MonsterDictionary { get { return monsterDictionary; } set { monsterDictionary = value; } }

        void Awake()
        {
            DemoContext.Instance.Demo4 = this;
            ContextRoot.Context = DemoContext.Instance;
            OnClickUpdate();
        }

        void OnClickUpdate()
        {
			MonsterCount = M4uUtil.Random(0, 6);
			MonsterNames = new string[MonsterCount];
			Monsters.Clear ();
			for(int i = 0; i < MonsterCount; i++)
            {
				MonsterNames[i] = "Goblin" + i;

                var monster = new MonsterData();
				monster.Name = MonsterNames [i];
                monster.Texture = Resources.Load("Texture/monster") as Texture;
                Monsters.Add(monster);
            }
        }

		void OnChangedMonsterList()
		{
			Debug.Log ("MonsterList = " + MonsterList.Count);
		}

		void OnChangedMonsterDictionary()
		{
			Debug.Log ("MonsterDictionary = " + MonsterDictionary.Count);
		}
	}
}