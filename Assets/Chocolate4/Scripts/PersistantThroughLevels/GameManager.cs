using Chocolate4.Entities;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Chocolate4.UI.MainMenu;

namespace Chocolate4.PersistantThroughLevels
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] 
        private int[] sceneIndexesNeededToPlay;
        private const int _mainMenuSceneIndex = 3;


        private void OnEnable()
        {
            MMController.OnClickPlay += StartNewGame;
            EntityManager.OnAllEnemiesKilled += WinGame;
            Player.OnPlayerKilled += EndGame;
        }
        private void OnDisable()
        {
            MMController.OnClickPlay += StartNewGame;
            EntityManager.OnAllEnemiesKilled -= WinGame;
            Player.OnPlayerKilled -= EndGame;
        }
        #if !UNITY_EDITOR
        private void Start()
        {
            StartCoroutine(
                ToMainMenuNowCo()
            );
        }
        #endif
        public void StartNewGame()
        {
            StartCoroutine(
                NewGameCo()
            );
        }
        private void WinGame()
        {
            StartCoroutine(
                ToMainMenuCo()
            );
        }
        private void EndGame()
        {
            StartCoroutine(
                ToMainMenuCo()
            );
        }
        private IEnumerator ToMainMenuCo()
        {
            yield return new WaitForSeconds(10f);

            yield return ToMainMenuNowCo();
        }
        private IEnumerator ToMainMenuNowCo()
        {
            yield return StartCoroutine(
                UnloadScenesOpenCo()  
            );

            yield return StartCoroutine(
                LoadMainMenuCo()
            );
        }
        private IEnumerator LoadMainMenuCo()
        {
            Scene scene = SceneManager.GetSceneByName("MainMenuScene");
            if (!scene.isLoaded)
            {
                yield return SceneManager.LoadSceneAsync(
                    _mainMenuSceneIndex, LoadSceneMode.Additive
                );
            }
            
            SceneManager.SetActiveScene(
                SceneManager.GetSceneByName("MainMenuScene")
            );
        }
        private IEnumerator UnloadScenesOpenCo()
        {
            int sceneCount = SceneManager.sceneCount;
            int j = 0;
            // when scene gets unloaded scenemanager remaps indexes
            for (int _ = 0; _ < sceneCount; _++)
            {
                Scene scene = SceneManager.GetSceneAt(j);
                if (scene.name == "PersistantScene")
                {
                    j++;
                    continue;
                }

                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }
        private IEnumerator LoadGameScenesCo()
        {
            for (int i = 0; i < sceneIndexesNeededToPlay.Length; i++)
            {
                int index = sceneIndexesNeededToPlay[i];

                yield return SceneManager.LoadSceneAsync(
                    index, LoadSceneMode.Additive
                );
            }
            Scene gameScene = SceneManager.GetSceneByName("LevelScene");
            SceneManager.SetActiveScene(gameScene);
        }
        private IEnumerator NewGameCo()
        {
            // only main menu at start
            yield return UnloadScenesOpenCo();

            yield return LoadGameScenesCo();

        }
    }
}