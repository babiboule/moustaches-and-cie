using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Panels
    [FormerlySerializedAs("options_Panel")] [SerializeField] private GameObject optionsPanel;
    
    // Settings components
    [SerializeField] private Button quitOptionsButton;
    [SerializeField] private Button toGeneralSettingsButton;
    [SerializeField] private Button toGraphicsSettingsButton;
    [SerializeField] private Button toAudioSettingsButton;
    

    private void Awake()
    {
        // Settings Menu Buttons Listeners
        quitOptionsButton.onClick.AddListener(QuitOptionsButtonClicked);
        toGeneralSettingsButton.onClick.AddListener(ToGeneralSettingsButtonClicked);
        toGraphicsSettingsButton.onClick.AddListener(ToGraphicsSettingsButtonClicked);
        toAudioSettingsButton.onClick.AddListener(ToAudioSettingsButtonClicked);
    }

    private void QuitOptionsButtonClicked()
    {
        optionsPanel.SetActive(false);
    }

    private void ToGeneralSettingsButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void ToGraphicsSettingsButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void ToAudioSettingsButtonClicked()
    {
        throw new NotImplementedException();
    }
}
