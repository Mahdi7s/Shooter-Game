using UnityEditor;
using UnityEditor.SceneManagement;

namespace Packages.M4u.Scripts.Trixmen.Editor
{
    [InitializeOnLoad]
    public static class TrixM4UReflectionInitialize
    {
        static TrixM4UReflectionInitialize()
        {
            EditorSceneManager.sceneOpening += (arg0, arg1) =>
            {
                TrixM4UReflection.Reset();
            };
        }
    }
}
