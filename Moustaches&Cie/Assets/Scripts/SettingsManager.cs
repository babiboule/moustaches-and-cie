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
    
    // Sliders
    private PointerEventData _musicVolume;
    private PointerEventData _sfxVolume;
    
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

    /*
     * Save the current options
     */
    private void SaveOptionsButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        GameManager.instance.SavePrefs();
    }

    /*
     * Change the music volume depending on the slider
     */
    private void MusicVolumeChanged(float vol)
    {
        StatsManager.instance.SetMusicVolume(vol);
        MusicManager.instance.bossaMeowa.volume = vol;
        MusicManager.instance.bossaMeowaBg.volume = vol;
    }

    /*
     * Change the sfx volume depending on the slider
     */
    private void SfxVolumeChanged(float vol)
    {
        StatsManager.instance.SetSfxVolume(vol);
    }

    /*
     * Close options panel
     */
    private void QuitOptionsButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        optionsPanel.SetActive(false);
    }
}