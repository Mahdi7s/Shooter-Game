using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Models;
using Models.Constants;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

public class ResourceGenerator
{
    private const string ResourceLoadClassName = "TrixResource";
    private static readonly string[] UsingNameSpaces = { "Infrastructure", "Models.Constants", "System", "System.Collections.Generic", "UnityEngine" };
    private const string PatternFormat = @"^({0})_.*\.({1})$";
    private static Dictionary<Type, ResourceType> ResourceTypeDictionary { get; set; } = new Dictionary<Type, ResourceType>();

    [MenuItem(StaticValues.MenuItemGenerateRoot + "Resources")]
    public static void Generate()
    {
        LoadResourceTypes();
        GenerateResourceClass();
        AssetDatabase.Refresh();
    }

    public static void LoadResourceTypes()
    {
        try
        {
            var resourceTypeDataSet = Resources.Load<ResourceTypeDataSet>(typeof(ResourceTypeDataSet).Name);
            if (resourceTypeDataSet)
            {
                foreach (var item in resourceTypeDataSet.ResourceTypes)
                {
                    try
                    {
                        var type = Type.GetType($"UnityEngine.{item.Type}, UnityEngine");
                        if (type != null)
                        {
                            if (ResourceTypeDictionary.ContainsKey(type))
                            {
                                ResourceTypeDictionary[type] = item;
                            }
                            else
                            {
                                ResourceTypeDictionary.Add(type, item);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Debug.LogError($"Unknown Type {item.Type}");
                    }
                }
                foreach (var key in ResourceTypeDictionary.Keys)
                {
                    var path = $"{StaticValues.DirectoryResources}{key.Name}s";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public static void GenerateResourceClass()
    {
        var nameSpace = $"{string.Join(".", StaticValues.DirectoryUtilities.Split('/').Skip(2)).TrimEnd('.')}";
        try
        {
            string text = string.Empty;
            InjectUsing(ref text);
            text = $"{text}{Environment.NewLine}namespace {nameSpace}{Environment.NewLine}{{{Environment.NewLine}public class {ResourceLoadClassName}{Environment.NewLine}{{{Environment.NewLine}{InjectResourceProperties()}{Environment.NewLine}{InjectResourceClasses()}}}}}";
            File.WriteAllText($"{StaticValues.DirectoryUtilities}{ResourceLoadClassName}.cs", text);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
    private static string InjectResourceProperties()
    {
        try
        {
            var result = string.Empty;
            foreach (var type in ResourceTypeDictionary.Keys)
            {
                result = $"{result}{Environment.NewLine}public static {GetTypeClassName(type)} {type.Name}s {{ get; }} = new {GetTypeClassName(type)}();{Environment.NewLine}";
            }
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return string.Empty;
        }
    }
    private static string InjectResourceClasses()
    {
        try
        {
            var result = string.Empty;
            foreach (var type in ResourceTypeDictionary.Keys)
            {
                result = $"{result}{Environment.NewLine}public class {GetTypeClassName(type)}{Environment.NewLine}{{{Environment.NewLine}" +
                       //$"private readonly Dictionary<string, {type.Name}> {GetDictionaryName(type, false)} = new Dictionary<string, {type.Name}>();{Environment.NewLine}" +
                       $"{InjectEnumPathDictionary(type)}{Environment.NewLine}{InjectEnumGetMethod(type)}{Environment.NewLine}{InjectProperties(type)}{Environment.NewLine}}}";
            }
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return string.Empty;
        }
    }

    private static string InjectEnumPathDictionary(Type type)
    {
        return
            $"private readonly Dictionary<string, string> {GetEnumPathDictionaryName(type)} = new Dictionary<string, string>{Environment.NewLine}{{{Environment.NewLine}" +
            InjectEnumPathPro(type) + Environment.NewLine + "};";
    }
    private static string InjectEnumGetMethod(Type type)
    {
        return $"public void GetByEnum(Enum enumValue, string path, Action<{type.Name}> callback){Environment.NewLine}{{{Environment.NewLine}" +
                        "if (string.Equals(enumValue.ToString(), StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase))" +
                        $"{Environment.NewLine}{{{Environment.NewLine}ResourceManager.Instance.LoadResourceAsync(StaticValues.UrlKey, path, callback);{Environment.NewLine}return;" +
                        $"{Environment.NewLine}}}{Environment.NewLine}var key = $\"{{enumValue.GetType().Name}}_{{enumValue}}\";{Environment.NewLine}string pathValue;{Environment.NewLine}" +
                        $"if ({GetEnumPathDictionaryName(type)}.TryGetValue(key, out pathValue)){Environment.NewLine}{{{Environment.NewLine}ResourceManager.Instance.LoadResourceAsync(pathValue, string.Empty, callback);" +
                        $"{Environment.NewLine}return;{Environment.NewLine}}}{Environment.NewLine}callback(null);{Environment.NewLine}}}";
    }
    private static string InjectEnumPathPro(Type type)
    {
        try
        {
            var str = string.Empty;
            foreach (var item in Directory.GetDirectories($"{StaticValues.DirectoryResources}{type.Name}s"))
            {
                var fileContent = string.Empty;
                var dirName = item.Split('\\').Last();
                if (dirName.StartsWith("Enum"))
                {
                    var enumName = $"{dirName.Replace("Enum", string.Empty)}{type.Name}";
                    fileContent = $"{fileContent}public enum {enumName}{Environment.NewLine}{{{Environment.NewLine}{StaticValues.UrlKey} = 0,{Environment.NewLine}";
                    foreach (var file in Directory.GetFiles(item))
                    {
                        if (Regex.IsMatch(Path.GetFileName(file).ToLower(), string.Format(PatternFormat, ResourceTypeDictionary[type].Prefix, ResourceTypeDictionary[type].Extension)))
                        {
                            fileContent += $"{Path.GetFileNameWithoutExtension(file)} = {Path.GetFileNameWithoutExtension(file).GetHashCode()},{Environment.NewLine}";
                            str += $"{{\"{enumName}_{Path.GetFileNameWithoutExtension(file)}\",\"{GetResourcePath(type, file)}\"}},{ Environment.NewLine}";
                            //EnumProperties = $"{EnumProperties}{Environment.NewLine}{InjectPropertiesByPath(Path.GetFileNameWithoutExtension(file), GetResourcePath(type, file), type)}";
                        }
                    }
                    fileContent += "}";
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
            return str;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return string.Empty;
        }
    }
    private static string GetResourcePath(Type type, string file)
    {
        try
        {
            var typePath = $"{type.Name}s";
            var typeIndex = file.IndexOf(typePath, StringComparison.Ordinal);
            var extensionIndex = file.IndexOf(Path.GetFileName(file), StringComparison.Ordinal) + Path.GetFileNameWithoutExtension(file).Length;
            var path = file.Remove(extensionIndex).Substring(typeIndex);
            return path.Replace("\\", "/");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return string.Empty;
        }
    }
    private static string InjectPropertiesByPath(string name, string path, Type type)
    {
        return $"public {type.Name} {name} => ResourceManager.LoadResource<{type.Name}>(\"{path}\");{Environment.NewLine}" +
        $"public void {name}_async(Action<{type.Name}> callback) => ResourceManager.Instance.LoadResourceAsync(\"{path}\", string.Empty, callback); ";
    }
    private static string InjectProperties(Type type)
    {
        try
        {
            var result = string.Empty;

            var files = DirectorySearch(GetDirectoryByResourceType(type));

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (fileName != null && Regex.IsMatch(fileName.ToLower(), string.Format(PatternFormat, ResourceTypeDictionary[type].Prefix, ResourceTypeDictionary[type].Extension)))
                {
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                    if (fileNameWithoutExtension != null)
                        result =
                            $"{result}{Environment.NewLine}{InjectPropertiesByPath(fileNameWithoutExtension.Replace($"{ResourceTypeDictionary[type].Prefix}_", string.Empty), GetResourcePath(type, file), type)}";
                }
            }
            return result;

        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return string.Empty;
        }
    }

    private static List<string> DirectorySearch(string directory)
    {
        var files = new List<string>();
        try
        {
            foreach (var f in Directory.GetFiles(directory))
            {
                files.Add(f);
            }
            foreach (var innerDirectory in Directory.GetDirectories(directory))
            {
                files.AddRange(DirectorySearch(innerDirectory));
            }
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
        }
        return files;
    }

    private static string GetEnumPathDictionaryName(Type type)
    {
        return $"_{type.Name.ToLower()}EnumPathDictionary";
    }
    private static string GetTypeClassName(Type type)
    {
        return $"Trix{type.Name}";
    }
    private static void InjectUsing(ref string str)
    {
        str = UsingNameSpaces.Aggregate(str, (current, item) => current + $"using {item};{Environment.NewLine}");
    }
    public static string GetDirectoryByResourceType(Type type)
    {
        return $"{StaticValues.DirectoryResources}{type.Name}s";
    }
}