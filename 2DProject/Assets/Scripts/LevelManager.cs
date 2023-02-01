using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, IDataPersistence
{
    [Header("Scripts")] public static LevelManager instance;

    [Header("Variables")] public static int gemsCollected;
    public string levelToLoad;
    public string nextLevel;
    private bool musicPlayed;
    public static int gemCounter;

    private void Awake()
    {
        instance = this;
    }

    public void LoadData(GameData data)
    {
        gemsCollected = data.gems;
    }
    public void SaveData(ref GameData data)
    {
        data.gems = gemsCollected;
    }
    private void Update()
    {
        UIController.instance.UpdateGemCount();
        if (PlayerHealthController.instance.currentHealth > 0 || !Input.GetKeyDown(KeyCode.R)) return;
        UIController.instance.deathScreen.gameObject.SetActive(false);
        StartCoroutine(RespawnCoolDown());
    }


    private IEnumerator RespawnCoolDown()
    {
        var color = PlayerController.instance.spriteRenderer.color;
        color = new Color(color.r,
            color.g, color.b, 1f);
        PlayerController.instance.spriteRenderer.color = color;
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds(1f / UIController.instance.fadeSpeed + 0.2f);
        UIController.instance.FadeFromBlack();
        PlayerController.instance.gameObject.SetActive(true);
        MuzzleFlash.instance.isShooting = false;
        PlayerController.instance.canDash = false;
        PlayerController.instance.isDashing = false;
        PlayerController.instance.transform.position = CheckPointController.instance.spawnPoint;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealth();

        yield return new WaitForSeconds(1f);
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    private IEnumerator EndLevelCo()
    {
        if (musicPlayed) yield break;
        musicPlayed = true;
        gemCounter = gemsCollected;
        AudioManager.instance.bgm.Stop();
        AudioManager.instance.levelEndMusic.Play();
        CameraController.instance.stopFollow = true;
        UIController.instance.levelCompleteText.SetActive(true);
        yield return new WaitForSeconds(UIController.instance.fadeSpeed + 1f);
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds(1f / UIController.instance.fadeSpeed + .25f);
        SceneManager.LoadScene(nextLevel);
    }
}