using Models.Constants;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class GameplayEnumsGenerator
{
    private const string PatternFormat = @"^({0})_.*\.({1})$";

    [MenuItem(StaticValues.MenuItemGenerateRoot + "Gameplay Enums")]
    public static void GenerateEnums()
    {
        #region Audios

        try
        {
            foreach (var item in Directory.GetDirectories($"{StaticValues.DirectoryResources}AudioClips"))
            {
                var dirName = item.Split('\\').Last();

                if (dirName.Equals("GameplayMusics") || dirName.Equals("GameplayEnvironments"))
                {
                    var enumName = dirName;
                    var fileContent = $"namespace Models.Constants{Environment.NewLine}{{{Environment.NewLine}    public enum {enumName}{Environment.NewLine}    {{{Environment.NewLine}        Undefined = 0,{Environment.NewLine}";
                    foreach (var file in Directory.GetFiles(item))
                    {
                        if (Regex.IsMatch(Path.GetFileName(file).ToLower(), string.Format(PatternFormat, "sfx", "mp3")))
                        {
                            fileContent += $"        {Path.GetFileNameWithoutExtension(file)} = {Path.GetFileNameWithoutExtension(file).GetHashCode()},{Environment.NewLine}";
                        }
                    }
                    fileContent += $"    }}{Environment.NewLine}}}";
                    try
                    {
                        File.WriteAllText($"{StaticValues.DirectoryEnums}{enumName}.cs", fileContent);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex.Message);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        #endregion Audios

        #region Backgrounds

        try
        {
            foreach (var item in Directory.GetDirectories($"{StaticValues.DirectoryResources}GameObjects"))
            {
                var dirName = item.Split('\\').Last();

                if (dirName.Equals("GameplayBackgrounds"))
                {
                    var enumName = dirName;
                    var fileContent = $"namespace Models.Constants{Environment.NewLine}{{{Environment.NewLine}    public enum {enumName}{Environment.NewLine}    {{{Environment.NewLine}        Undefined = 0,{Environment.NewLine}";
                    foreach (var file in Directory.GetFiles(item))
                    {
                        if (Regex.IsMatch(Path.GetFileName(file).ToLower(), string.Format(PatternFormat, "pre", "prefab")))
                        {
                            fileContent += $"        {Path.GetFileNameWithoutExtension(file)} = {Path.GetFileNameWithoutExtension(file).GetHashCode()},{Environment.NewLine}";
                        }
                    }
                    fileContent += $"    }}{Environment.NewLine}}}";
                    try
                    {
                        File.WriteAllText($"{StaticValues.DirectoryEnums}{enumName}.cs", fileContent);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex.Message);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        #endregion Backgrounds
    }
}
