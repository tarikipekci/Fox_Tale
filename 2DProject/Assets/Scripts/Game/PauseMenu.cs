using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Variables")] public string mainMenu;
    public bool isPaused;

    [Header("Scripts")] public static PauseMenu instance;

    [Header("Game Objects")] public GameObject pauseScreen;

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
        LevelManager.gemsCollected = LevelManager.gemCounter;
        SceneManager.LoadScene(LevelManager.instance.levelToLoad);
        UIController.instance.UpdateGemCount();
        Time.timeScale = 1f;
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
        LevelManager.gemsCollected = 0;
        UIController.instance.UpdateGemCount();
        SceneManager.LoadScene("Testing2");
    }
}