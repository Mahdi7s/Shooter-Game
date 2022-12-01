//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;

namespace M4u
{
    /// <summary>
    /// MonsterData
    /// </summary>
	public class MonsterData : M4uContext
	{
		private M4uProperty<string> name = new M4uProperty<string> ();
		private M4uProperty<Texture> texture = new M4uProperty<Texture> ();
		private M4uProperty<int> hp = new M4uProperty<int> ();
		private M4uProperty<int> atk = new M4uProperty<int> ();
		private M4uProperty<string> skill = new M4uProperty<string> ();

		public string Name { get { return name.Value; } set { name.Value = value; } }
		public Texture Texture { get { return texture.Value; } set { texture.Value = value; } }
		public int Hp { get { return hp.Value; } set { hp.Value = value; } }
		public int Atk { get { return atk.Value; } set { atk.Value = value; } }
		public string Skill { get { return skill.Value; } set { skill.Value = value; } }

		void OnMonsterCheck()
		{
			Debug.Log ("Monster is " + Name);
		}
	}
}