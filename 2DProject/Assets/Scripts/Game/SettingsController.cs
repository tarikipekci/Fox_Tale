using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Text resolutionLevel;
    public Text soundLevel;
    public Text ambientLevel;
    public Text musicLevel;
    public AudioManager audioManager;
    static int defaultIndex = 5;
    static int index;
    static float sound;

    public int[] width = { 800, 1024, 1280, 1360, 1366, 1920 };
    public int[] height = { 600, 768, 720, 768, 768, 1080 };
    public static SettingsController instance;

    private void Awake()
    {
        instance = this;
        sound = audioManager.bgm.volume;
        index = (int)sound;
    }

    private void Update()
    {
        resolutionLevel.text = width[defaultIndex].ToString() + "x" + height[defaultIndex].ToString();
        soundLevel.text = (Mathf.Round((audioManager.bgm.volume + audioManager.soundEffects[index].volume) / 2 * 100))
            .ToString();
        ambientLevel.text = Mathf.Round((audioManager.soundEffects[index].volume) * 100).ToString();
        musicLevel.text = Mathf.Round((audioManager.bgm.volume) * 100).ToString();
    }

    public void IncreaseResolution()
    {
        Screen.SetResolution(width[defaultIndex + 1], height[defaultIndex + 1], FullScreenMode.ExclusiveFullScreen);
        defaultIndex++;
    }

    public void DecreaseResolution()
    {
        Screen.SetResolution(width[defaultIndex - 1], height[defaultIndex - 1], FullScreenMode.ExclusiveFullScreen);
        defaultIndex--;
    }

    public void IncreaseGeneralSound()
    {
        audioManager.bgm.volume += 0.01f;

        for (int i = 0; i < audioManager.soundEffects.Length; i++)
        {
            audioManager.soundEffects[i].volume += 0.01f;
        }
    }

    public void DecreaseGeneralSound()
    {
        audioManager.bgm.volume -= 0.01f;

        for (int i = 0; i < audioManager.soundEffects.Length; i++)
        {
            audioManager.soundEffects[i].volume -= 0.01f;
        }
    }

    public void IncreaseAmbientSound()
    {
        for (int i = 0; i < audioManager.soundEffects.Length; i++)
        {
            audioManager.soundEffects[i].volume += 0.01f;
        }
    }

    public void DecreaseAmbientSound()
    {
        for (int i = 0; i < audioManager.soundEffects.Length; i++)
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