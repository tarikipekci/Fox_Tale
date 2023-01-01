using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Variables")]
    public string startScene;
    
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
}
