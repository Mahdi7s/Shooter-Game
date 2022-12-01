using M4u;
using System;
using System.IO;
using System.Linq;
using Models.Constants;
using UnityEditor;
using UnityEngine;

namespace Packages.M4u.Scripts.Trixmen.Editor
{
    public class TrixM4UManager : EditorWindow
    {
        private string ViewName { get; set; } = string.Empty;
        private bool Model { get; set; } = true;
        private bool ViewModel { get; set; } = true;
        private bool Controller { get; set; } = true;
        private bool Context { get; set; } = true;
        private string ModelNameSpace { get; set; } = string.Empty;
        private string ViewModelNameSpace { get; set; } = string.Empty;
        private string ControllerNameSpace { get; set; } = string.Empty;
        private string ContextNameSpace { get; set; } = string.Empty;
        private string ModelName { get; set; } = string.Empty;
        private string ViewModelName { get; set; } = string.Empty;
        private string ControllerName { get; set; } = string.Empty;
        private string ContextName { get; set; } = string.Empty;
        [MenuItem("Trixmen/M4u Manager")]
        public static void OpenM4UManager()
        {
            var window = GetWindow<TrixM4UManager>("M4u Manager");
            window.minSize = new Vector2(600, 400);
        }

        private void OnGUI()
        {
            GUI.backgroundColor = Color.cyan;
            EditorGUILayout.HelpBox("Directories in StaticValues script", MessageType.Info);
            EditorGUILayout.LabelField($"Contexts Location: {StaticValues.DirectoryM4UContexts}");
            EditorGUILayout.LabelField($"Controllers Location: {StaticValues.DirectoryM4UController}");
            EditorGUILayout.LabelField($"ViewModels Location: {StaticValues.DirectoryM4UViewModels}");
            EditorGUILayout.LabelField($"Models Location: {StaticValues.DirectoryM4UModels}");
            EditorGUILayout.Space();

            GUI.backgroundColor = Color.white;
            ViewName = EditorGUILayout.TextField("View Name: ", ViewName);

            EditorGUILayout.LabelField("Generate:", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            Model = EditorGUILayout.Toggle("Model", Model);
            ViewModel = EditorGUILayout.Toggle("ViewModel", ViewModel);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            Controller = EditorGUILayout.Toggle("Controller", Controller);
            Context = EditorGUILayout.Toggle("Context", Context);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.green;
            GUIStyle generateButtonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold, normal = { textColor = Color.black, background = Texture2D.whiteTexture }, active = { textColor = Color.red, background = Texture2D.whiteTexture } };
            if (GUILayout.Button("Generate", generateButtonStyle))
            {
                GenerateScripts();
                AssetDatabase.Refresh();
            }
            GUI.backgroundColor = EditorApplication.isCompiling ? Color.gray : Color.yellow;
            GUIStyle attachButtonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold, normal = { textColor = Color.black, background = Texture2D.whiteTexture }, active = { textColor = Color.blue, background = Texture2D.whiteTexture } };
            if (GUILayout.Button("Attach", attachButtonStyle) && !EditorApplication.isCompiling)
            {
                AttachToGameObject();
            }
            EditorGUILayout.EndHorizontal();
            this.Repaint();
        }

        private void GenerateScripts()
        {
            if (string.IsNullOrEmpty(ViewName))
            {
                Debug.LogError("Please enter View Name!");
                return;
            }
            if (!Model && !ViewModel && !Controller && !Context)
            {
                Debug.LogError("Nothing selected to generate!");
                return;
            }
            ModelName = $"{ViewName}Model";
            ViewModelName = $"{ViewName}BindingViewModel";
            ControllerName = $"{ViewName}Controller";
            ContextName = $"{ViewName}BindingContext";

            ModelNameSpace = $"{string.Join(".", StaticValues.DirectoryM4UModels.Split('/').Skip(2)).TrimEnd('.')}";
            ViewModelNameSpace = $"{string.Join(".", StaticValues.DirectoryM4UViewModels.Split('/').Skip(2)).TrimEnd('.')}";
            ControllerNameSpace = $"{string.Join(".", StaticValues.DirectoryM4UController.Split('/').Skip(2)).TrimEnd('.')}";
            ContextNameSpace = $"{string.Join(".", StaticValues.DirectoryM4UContexts.Split('/').Skip(2)).TrimEnd('.')}";

            if (Model)
            {
                if (File.Exists($"{StaticValues.DirectoryM4UModels}{ModelName}.cs"))
                {
                    Debug.LogError($"{ModelName}.cs exist in {StaticValues.DirectoryM4UModels}");
                }
                else
                {
                    var classContent = $"namespace {ModelNameSpace}{Environment.NewLine}{{{Environment.NewLine}    public class {ModelName} : ModelBase{Environment.NewLine}    {{{Environment.NewLine}        public override void ToViewModel<TViewModel>(TViewModel viewModel){Environment.NewLine}        {{{Environment.NewLine}            {Environment.NewLine}        }}{Environment.NewLine}    }}{Environment.NewLine}}}";
                    File.WriteAllText($"{StaticValues.DirectoryM4UModels}{ModelName}.cs", classContent);
                }
            }

            if (ViewModel)
            {
                if (File.Exists($"{StaticValues.DirectoryM4UViewModels}{ViewModelName}.cs"))
                {
                    Debug.LogError($"{ViewModelName}.cs exist in {StaticValues.DirectoryM4UViewModels}");
                }
                else
                {
                    var classContent = $"using {ControllerNameSpace};{Environment.NewLine}using Packages.M4u.Scripts.Trixmen;{Environment.NewLine}using UnityEngine;{Environment.NewLine}{Environment.NewLine}namespace {ViewModelNameSpace}{Environment.NewLine}{{{Environment.NewLine}    /*[RequireComponent(typeof({ControllerName}))]*/{Environment.NewLine}    public class {ViewModelName} : TrixViewModel<{ControllerName}>{Environment.NewLine}    {{{Environment.NewLine}{GetViewModelContents()}{Environment.NewLine}}}{Environment.NewLine}}}";
                    //var classCotent = $"using {ControllerNameSpace};{Environment.NewLine}using Packages.M4u.Scripts.Trixmen;{Environment.NewLine}{Environment.NewLine}namespace {ViewModelNameSpace}{Environment.NewLine}{{{Environment.NewLine}    public class {ViewModelName} : TrixViewModel<{ControllerName}>{Environment.NewLine}    {{{Environment.NewLine}{Environment.NewLine}    }}{Environment.NewLine}}}";
                    File.WriteAllText($"{StaticValues.DirectoryM4UViewModels}{ViewModelName}.cs", classContent);
                }
            }

            if (Controller)
            {
                if (File.Exists($"{StaticValues.DirectoryM4UController}{ControllerName}.cs"))
                {
                    Debug.LogError($"{ControllerName}.cs exist in {StaticValues.DirectoryM4UController}");
                }
                else
                {
                    var classContent = $"namespace {ControllerNameSpace}{Environment.NewLine}{{{Environment.NewLine}    public class {ControllerName} : ControllerBase{Environment.NewLine}    {{{Environment.NewLine}{GetControllerContents()}{Environment.NewLine}}}{Environment.NewLine}}}";
                    File.WriteAllText($"{StaticValues.DirectoryM4UController}{ControllerName}.cs", classContent);
                }
            }


            if (Context)
            {
                if (File.Exists($"{StaticValues.DirectoryM4UContexts}{ContextName}.cs"))
                {
                    Debug.LogError($"{ContextName}.cs exist in {StaticValues.DirectoryM4UContexts}");
                }
                else
                {
                    var classContent = $"using {ViewModelNameSpace};{Environment.NewLine}using Packages.M4u.Scripts.Trixmen;{Environment.NewLine}{Environment.NewLine}namespace {ContextNameSpace}{Environment.NewLine}{{{Environment.NewLine}    public class {ContextName} : TrixContextBehaviour<{ViewModelName}> {Environment.NewLine}    {{{Environment.NewLine}            {Environment.NewLine}    }}{Environment.NewLine}}}";
                    File.WriteAllText($"{StaticValues.DirectoryM4UContexts}{ContextName}.cs", classContent);
                }
            }
        }

        private string GetViewModelContents()
        {
            return $"         protected override void OnBackPressed(){Environment.NewLine}        {{{Environment.NewLine}        }}";
        }

        private string GetControllerContents()
        {
            return $"        protected internal  override bool IsSingleView {{ get; set; }}{Environment.NewLine}        protected internal  override bool IsPopup {{ get; set; }}";
        }
        private void AttachToGameObject()
        {
            if (Selection.gameObjects.Length > 1)
            {
                Debug.LogError("Select just one gameobject!");
                return;
            }

            if (Selection.gameObjects.Length <= 0)
            {
                Debug.LogError("Select one gameobject!");
                return;
            }

            if (string.IsNullOrEmpty(ContextNameSpace) || string.IsNullOrEmpty(ControllerNameSpace) || string.IsNullOrEmpty(ViewModelNameSpace) ||
                string.IsNullOrEmpty(ContextName) || string.IsNullOrEmpty(ControllerName) || string.IsNullOrEmpty(ViewModelName))
            {
                Debug.LogError("Enter Generate First");
                return;
            }
            var gameObject = Selection.gameObjects[0].gameObject;

            var contextType = Type.GetType($"{ContextNameSpace.Replace(" ", string.Empty)}.{ContextName}, {typeof(M4uContextRoot).Assembly.FullName}");
            var controllerType = Type.GetType($"{ControllerNameSpace.Replace(" ", string.Empty)}.{ControllerName}, {typeof(M4uContextRoot).Assembly.FullName}");
            var viewModelType = Type.GetType($"{ViewModelNameSpace.Replace(" ", string.Empty)}.{ViewModelName}, {typeof(M4uContextRoot).Assembly.FullName}");

            M4uContextRoot root = null;
            M4uContextMonoBehaviour context = null;
            if (!gameObject.GetComponent<M4uContextRoot>())
            {
                root = gameObject.AddComponent<M4uContextRoot>();
            }
            if (!gameObject.GetComponent(controllerType))
            {
                gameObject.AddComponent(controllerType);
            }
            if (!gameObject.GetComponent(viewModelType))
            {
                gameObject.AddComponent(viewModelType);
            }
            if (!gameObject.GetComponent(contextType))
            {
                context = gameObject.AddComponent(contextType) as M4uContextMonoBehaviour;
            }
            if (root && !root.ContextMonoBehaviour) root.ContextMonoBehaviour = context;
        }
    }
}
