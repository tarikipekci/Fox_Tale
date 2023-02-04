using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [Header("Scripts")]
    public static AudioManager instance;

    [Header("Components")]
    public AudioSource[] soundEffects;
    public AudioSource bgm, levelEndMusic;
        
    private void Awake()
    {
        instance = this;
    }
    
    public void PlaySfx(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();
        soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.1f);
        soundEffects[soundToPlay].Play();
    }
}
