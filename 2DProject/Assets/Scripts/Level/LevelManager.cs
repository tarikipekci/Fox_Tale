using System.Collections;
using Game;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        [Header("Scripts")] public static LevelManager _instance;

        [Header("Variables")] public static int GemsCollected;
        public string levelToLoad;
        public string nextLevel;
        private bool _musicPlayed;
        public static int GemCounter;

        [Header("Objects")] public SettingsData data;
        
        private void Awake()
        {
            _instance = this;
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
            if (_musicPlayed) yield break;
            _musicPlayed = true;
            GemCounter = GemsCollected;
            
            AudioManager.instance.bgm.Stop();
            AudioManager.instance.levelEndMusic.Play();
            CameraController.instance.stopFollow = true;
            UIController.instance.levelCompleteText.SetActive(true);
            yield return new WaitForSeconds(UIController.instance.fadeSpeed + 1f);
            UIController.instance.FadeToBlack();
            yield return new WaitForSeconds(1f / UIController.instance.fadeSpeed + .25f);
            SceneManager.LoadScene(nextLevel);
            
            SettingsController.instance.audioManager.bgm.volume = data.soundLevelForMusic;
            
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < SettingsController.instance.audioManager.soundEffects.Length; i++)
            {
                SettingsController.instance.audioManager.soundEffects[i].volume = data.ambientSoundLevel;
            }
        }
    }
}