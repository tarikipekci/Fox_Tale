using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Settings/SoundSettings",fileName = "New Settings")]
    public class SettingsData : ScriptableObject
    {
       [Header("Sound Setting Variables")]
        public float soundLevelForMusic;
        public float ambientSoundLevel;
        public float generalSound;

        // ReSharper disable once InconsistentNaming
        [Header("Scripts")] public static SettingsData instance;
    }
}
