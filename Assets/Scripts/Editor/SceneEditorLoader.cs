using Extensions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
public class SceneEditorLoader : Editor
{
[MenuItem("Scenes/ scn_Gameplay")]
static void Open_scn_Gameplay()
{EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
EditorSceneManager.OpenScene(Scenes.scn_Gameplay.GetPath());
}
[MenuItem("Scenes/ scn_Loading")]
static void Open_scn_Loading()
{EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
EditorSceneManager.OpenScene(Scenes.scn_Loading.GetPath());
}
[MenuItem("Scenes/ scn_Menu")]
static void Open_scn_Menu()
{EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
EditorSceneManager.OpenScene(Scenes.scn_Menu.GetPath());
}
}