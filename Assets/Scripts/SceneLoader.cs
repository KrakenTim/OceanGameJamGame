using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityEngine.UI
{
    public class SceneLoader : MonoBehaviour
    {
        [Tooltip("All Menuitems that aren't the loadingscreen. When disabled they can't capture inputs while loading is in progress")] public GameObject menu;
        [Tooltip("Gameobject that will cover the screen while loading")] public GameObject loadingInterface;
        [Tooltip("A UI Image used to visualize the progress on load")] public Image LoadingProgressBar;

        List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

        public void changeScene(string scene)
        {
            HideMenu();
            ShowLoadingScreen();
            scenesToLoad.Add(SceneManager.LoadSceneAsync(scene));
            StartCoroutine(LoadingScreen());
        }
        IEnumerator LoadingScreen()
        {
            float totalProgress = 0;
            for (int i = 0; i < scenesToLoad.Count; ++i)
            {
                while (!scenesToLoad[i].isDone)
                {
                    totalProgress += scenesToLoad[i].progress;
                    LoadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                    yield return null;
                }
                yield return new WaitForEndOfFrame();   // wait one more frame to make sure awake and start are called
            }
        }
        public void ShowLoadingScreen()
        {
            loadingInterface.SetActive(true);
        }

        public void HideMenu()
        {
            menu.SetActive(false);
        }
        public void HideLoadingScreen()
        {
            loadingInterface.SetActive(false);
        }

        public void ShowMenu()
        {
            menu.SetActive(true);
        }
        private void OnDisable()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}