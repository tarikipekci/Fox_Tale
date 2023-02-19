using Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Scripts")]
        // ReSharper disable once InconsistentNaming
        public static AudioManager instance;

        public SettingsData data;

        [Header("Components")] public AudioSource[] soundEffects;
        public AudioSource bgm, levelEndMusic;

        private void Awake()
        {
            instance = this;
            bgm.volume = data.soundLevelForMusic;
            levelEndMusic.volume = data.soundLevelForMusic;
            
            foreach (var t in soundEffects)
            {
                t.volume = data.ambientSoundLevel;
            }
        }

        public void PlaySfx(int soundToPlay)
        {
            soundEffects[soundToPlay].Stop();
            soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.1f);
            soundEffects[soundToPlay].Play();
        }
    }
}