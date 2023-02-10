using Level;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SettingsController : MonoBehaviour
    {
        [Header("Components")] public Text resolutionLevel;
        public Text soundLevel, ambientLevel, musicLevel;
        public AudioManager audioManager;

        [Header("Variables")] private static int _defaultIndex = 5;
        public SettingsData data;
        
        // ReSharper disable once InconsistentNaming
        [Header("Script")] public static SettingsController instance;

        private void Awake()
        {
            instance = this;
            data.generalSound = (data.ambientSoundLevel + data.soundLevelForMusic) / 2f;

            audioManager.bgm.volume = data.soundLevelForMusic;
            
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < audioManager.soundEffects.Length; i++)
            {
                audioManager.soundEffects[i].volume = data.ambientSoundLevel;
            }
        }

        public int[] width = { 800, 1024, 1280, 1360, 1366, 1920 };
        public int[] height = { 600, 768, 720, 768, 768, 1080 };

        private void Update()
        {
            data.ambientSoundLevel = audioManager.soundEffects[0].volume;
            data.soundLevelForMusic = audioManager.bgm.volume;
            data.generalSound = (data.ambientSoundLevel + data.soundLevelForMusic) / 2f;
            
            resolutionLevel.text = width[_defaultIndex] + "x" + height[_defaultIndex];
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            soundLevel.text = Mathf.Round(data.generalSound * 100).ToString();
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            ambientLevel.text = Mathf.Round((data.ambientSoundLevel) * 100).ToString();
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            musicLevel.text = Mathf.Round((data.soundLevelForMusic) * 100).ToString();
        }

        public void IncreaseResolution()
        {
            Screen.SetResolution(width[_defaultIndex + 1], height[_defaultIndex + 1],
                FullScreenMode.ExclusiveFullScreen);
            _defaultIndex++;
        }

        public void DecreaseResolution()
        {
            Screen.SetResolution(width[_defaultIndex - 1], height[_defaultIndex - 1],
                FullScreenMode.ExclusiveFullScreen);
            _defaultIndex--;
        }

        public void IncreaseGeneralSound()
        {
            audioManager.bgm.volume += 0.01f;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < audioManager.soundEffects.Length; i++)
            {
                audioManager.soundEffects[i].volume += 0.01f;
            }
        }

        public void DecreaseGeneralSound()
        {
            audioManager.bgm.volume -= 0.01f;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < audioManager.soundEffects.Length; i++)
            {
                audioManager.soundEffects[i].volume -= 0.01f;
            }
        }

        public void IncreaseAmbientSound()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < audioManager.soundEffects.Length; i++)
            {
                audioManager.soundEffects[i].volume += 0.01f;
            }
        }

        public void DecreaseAmbientSound()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < audioManager.soundEffects.Length; i++)
            {
                audioManager.soundEffects[i].volume -= 0.01f;
            }
        }

        public void IncreaseMusicSound()
        {
            audioManager.bgm.volume += 0.01f;
        }

        public void DecreaseMusicSound()
        {
            audioManager.bgm.volume -= 0.01f;
        }
    }
}