using Infrastructure;
using Menu.Models;
using Menu.ViewModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Packages.M4u.Scripts.Trixmen
{
    public static class TrixModelToViewModel
    {
        public static void ToViewModels<TModel, TViewModel, TContainerController>(this IEnumerable<TModel> modelEnumerable,
            List<TViewModel> targetViewModels,
            TContainerController containerController,
            Type targetType) where TModel : ModelBase
                                           where TContainerController : Object
                                           where TViewModel : ViewModelBase
        {
            if (targetType != typeof(TViewModel))
            {
                Debug.LogError("Target View Model Type Isn't TrixViewModel Type");
                return;
            }

            var controller = containerController as GameObject;
            if (!controller)
            {
                Debug.LogError("Target Container Controller Isn't a GameObject");
                return;
            }

            var oldViewModels = controller.GetComponents<TViewModel>();
            foreach (var oldViewModel in oldViewModels)
            {
                if (oldViewModel && oldViewModel.IsListView)
                    GameManager.Instance.DestroyMonoBehaviourComponent(oldViewModel);
            }

            targetViewModels.Clear();
            foreach (var model in modelEnumerable)
            {
                var viewModel = controller.AddComponent<TViewModel>();
                if (viewModel.IsListView)
                {
                    model.ToViewModel(viewModel);
                    targetViewModels.Add(viewModel);
                }
            }
        }
    }
}
