using Level;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("Variables")] public string mainMenu;
        public bool isPaused;
        public bool settings;

        // ReSharper disable once InconsistentNaming
        [Header("Scripts")] public static PauseMenu instance;

        [Header("Game Objects")] public GameObject pauseScreen;
        public GameObject settingsScreen;

        private void Awake()
        {
            instance = this;
            pauseScreen.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseOrUnpause();
            }

            if (settings)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseOrUnpause();
                    settings = false;
                    settingsScreen.SetActive(false);
                }
            }
        }

        public void PauseOrUnpause()
        {
            if (isPaused)
            {
                isPaused = false;
                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                if (PlayerHealthController.instance.currentHealth <= 0) return;
                isPaused = true;
                pauseScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        public void LevelSelect()
        {
            LevelManager.GemsCollected = LevelManager.GemCounter;
            SceneManager.LoadScene(LevelManager._instance.levelToLoad);
            UIController.instance.UpdateGemCount();
            Time.timeScale = 1f;
        }

        public void Settings()
        {
            settings = true;
            pauseScreen.gameObject.SetActive(false);
            settingsScreen.gameObject.SetActive(true);
        }

        public void MainMenu()
        {
            //DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadScene(mainMenu);
            Time.timeScale = 1f;
        }

        public void LoadFirstLevel()
        {
            Time.timeScale = 1f;
            LevelManager.GemsCollected = 0;
            UIController.instance.UpdateGemCount();
            SceneManager.LoadScene("Level1");
        }
    }
}