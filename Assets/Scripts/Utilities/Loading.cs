using Controllers;
using Infrastructure;
using Models.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace Utilities
{
    public class Loading : MonoBehaviour
    {
        public Image FadeImage;
        [SerializeField] private Slider _loadingSlider;
        private AsyncOperation LoadSceneAsync { get; set; }
        public Slider LoadingSlider
        {
            get { return _loadingSlider; }
            set { _loadingSlider = value; }
        }

        private void Awake()
        {
            if (LoadingSlider)
                LoadingSlider.DOValue(50, 6);
        }

        private void Update()
        {
            if (LoadSceneAsync != null)
            {
                LoadingSlider.value = 50 + (LoadSceneAsync.progress / 2);
            }
        }

        public void StartLoading()
        {
            if (ES3.KeyExists(StaticValues.PreventGoogleSave))
            {
                GameManager.Instance.PreventGoogleSave = ES3.Load<bool>(StaticValues.PreventGoogleSave);
            }
            else
            {
                GameManager.Instance.PreventGoogleSave = false;
            }

            if (GameManager.Instance.PreventGoogleSave || !GameManager.Instance.CanUseGooglePlay)
            {
                GameManager.Instance.Initialize(() =>
                {
                    LoadSceneAsync = SceneManager.LoadSceneAsync(Scenes.scn_Menu.ToString());
                });
            }
            else
            {
                //ProTODO: Add timeout
                GooglePlayController.Instance.TryLogin(succeed =>
                {
                    GameManager.Instance.Initialize(() =>
                    {
                        LoadSceneAsync = SceneManager.LoadSceneAsync(Scenes.scn_Menu.ToString());
                    });
                });
            }
        }
    }
}
