using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource[] SFXSource;
    public AudioSource BGMSource;

    public Slider BGMSlider;  // UI 슬라이더
    public Slider SFXSlider;  // UI 슬라이더

    private const string BGM_VOLUME_KEY = "BGMVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";

    protected override void Awake()
    {
        base.Awake();
        LoadVolumes();
        InitializeSliders();
    }

    private void LoadVolumes()
    {
        float bgmVolume = LoadSoundVolume(BGM_VOLUME_KEY);
        float sfxVolume = LoadSoundVolume(SFX_VOLUME_KEY);

        Debug.Log("Loaded BGM Volume: " + bgmVolume);
        Debug.Log("Loaded SFX Volume: " + sfxVolume);

        BGMSource.volume = bgmVolume;
        foreach (var sfx in SFXSource)
        {
            sfx.volume = sfxVolume;
        }

        // 슬라이더 값 설정
        if (BGMSlider != null)
        {
            BGMSlider.value = bgmVolume;
        }

        if (SFXSlider != null)
        {
            SFXSlider.value = sfxVolume;
        }
    }

    private void InitializeSliders()
    {
        if (BGMSlider != null)
        {
            BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (SFXSlider != null)
        {
            SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetBGMVolume(float volume)
    {
        Debug.Log("Setting BGM Volume: " + volume);
        BGMSource.volume = volume;
        SaveSoundVolume(BGM_VOLUME_KEY, volume);
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log("Setting SFX Volume: " + volume);
        foreach (var sfx in SFXSource)
        {
            sfx.volume = volume;
        }
        SaveSoundVolume(SFX_VOLUME_KEY, volume);
    }

    private void SaveSoundVolume(string key, float volume)
    {
        PlayerPrefs.SetFloat(key, volume);
        PlayerPrefs.Save();
        Debug.Log("Saved " + key + " with volume: " + volume);
    }

    private float LoadSoundVolume(string key)
    {
        float volume = PlayerPrefs.GetFloat(key, 1.0f);
        Debug.Log("Loaded " + key + " with volume: " + volume);
        return volume;
    }
}
