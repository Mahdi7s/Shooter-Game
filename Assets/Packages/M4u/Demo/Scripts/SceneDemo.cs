//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;

namespace M4u
{
    /// <summary>
    /// SceneDemo. Easy Bind Demo
    /// </summary>
	public class SceneDemo : MonoBehaviour
	{
		public M4uContextRoot Root;

		private MonsterData monster = new MonsterData ();

		void Awake()
		{
			Root.Context = monster;
		}

		void Start()
		{
			SetParameter ();
		}

		private void SetParameter()
		{
			monster.Name = "Goblin";
			monster.Texture = Resources.Load ("Texture/monster") as Texture;
			monster.Hp = 750;
			monster.Atk = 400;
			monster.Skill = "Fire Ball!";
		}
	}
}