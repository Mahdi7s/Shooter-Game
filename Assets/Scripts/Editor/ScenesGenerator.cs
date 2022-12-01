using System;
using System.IO;
using Models.Constants;
using UnityEditor;
using UnityEngine;

public class ScenesGenerator
{
    private const string SceneEnumName = "Scenes";

    [MenuItem(StaticValues.MenuItemGenerateRoot + "Scenes")]
    public static void GenerateSceneEnum()
    {
        var fileContent = string.Format("public enum {2}{0}{1}{0}", Environment.NewLine, "{", SceneEnumName);
        var undefinedScene = "Undefined";
        fileContent += $"{undefinedScene} = {0},{Environment.NewLine}";
        foreach (var item in Directory.GetFiles(StaticValues.DirectoryScenes, "scn_*.unity", SearchOption.AllDirectories))
        {
            var fileName = GetFileName(item);
            fileContent += $"{fileName} = {fileName.GetHashCode()},{Environment.NewLine}";
        }
        fileContent += "}";
        try
        {
            File.WriteAllText($"{StaticValues.DirectoryEnums}{SceneEnumName}.cs", fileContent);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        GenerateSceneEditorLoader();
        AssetDatabase.Refresh();
    }
    private static void GenerateSceneEditorLoader()
    {
        var fileContent =
            string.Format("using UnityEditor;{0}using UnityEditor.SceneManagement;{0}using UnityEngine;{0}public class SceneEditorLoader : Editor{0}{1}{0}", Environment.NewLine, "{");
        foreach (var item in Directory.GetFiles(StaticValues.DirectoryScenes, "scn_*.unity", SearchOption.AllDirectories))
        {
            var fileName = GetFileName(item);
            fileContent +=
                string.Format("[MenuItem(\"{0} {1}\")]{2}static void Open_{1}(){2}{3}EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();{2}EditorSceneManager.OpenScene({5}.{1}.GetPath());{2}{4}{2}",
                StaticValues.MenuItemSceneEditorLoader, fileName, Environment.NewLine, "{", "}", SceneEnumName);
        }
        fileContent += "}";
        try
        {
            File.WriteAllText($"{StaticValues.DirectoryEditors}SceneEditorLoader.cs", fileContent);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
    private static string GetFileName(string filePath)
    {
        return Path.GetFileNameWithoutExtension(filePath);
    }
}
