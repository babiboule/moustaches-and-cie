using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Panels
    [FormerlySerializedAs("options_Panel")] [SerializeField] private GameObject optionsPanel;
    
    // Settings components
    [SerializeField] private Button quitOptionsButton;
    [SerializeField] private Button saveOptionsButton;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    
    public PointerEventData MusicVolume;
    public PointerEventData SfxVolume;
    
    //Sfx
    [SerializeField] private AudioClip buttonSfx;

    private void Awake()
    {
        // Settings Menu Buttons Listeners
        quitOptionsButton.onClick.AddListener(QuitOptionsButtonClicked);
        musicVolumeSlider.onValueChanged.AddListener(MusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(SfxVolumeChanged);
        saveOptionsButton.onClick.AddListener(SaveOptionsButtonClicked);
    }

    private void SaveOptionsButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        GameManager.instance.SavePrefs();
    }

    private void MusicVolumeChanged(float vol)
    {
        StatsManager.instance.SetMusicVolume(vol);
        MusicManager.instance.bossaMeowa.volume = vol;
        MusicManager.instance.bossaMeowaBg.volume = vol;
    }

    private void SfxVolumeChanged(float vol)
    {
        StatsManager.instance.SetSfxVolume(vol);
    }

    private void QuitOptionsButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        optionsPanel.SetActive(false);
    }
}
