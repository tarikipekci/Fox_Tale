using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Variables")] public string startScene;

        [Header("Objects")] public GameObject mainMenu;
        public AudioManager audioManager;
        public SettingsData data;

        public void StartGame()
        {
            SceneManager.LoadScene(startScene);
            DataPersistenceManager.instance.LoadGame();
        }

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quitting Game");
            DataPersistenceManager.instance.SaveGame();
        }

        private void Awake()
        {
            audioManager.bgm.volume = data.soundLevelForMusic;
            
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < audioManager.soundEffects.Length; i++)
            {
                audioManager.soundEffects[i].volume = data.ambientSoundLevel;
            }
        }
    }
}