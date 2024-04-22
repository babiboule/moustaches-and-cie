using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Panels
    [FormerlySerializedAs("pause_Panel")] [SerializeField] private GameObject pausePanel;
    [FormerlySerializedAs("options_Panel")] [SerializeField] private GameObject optionsPanel;
    
    // Settings components
    [SerializeField] private Button toPauseMenuButton;
    [SerializeField] private Button toGeneralSettingsButton;
    [SerializeField] private Button toGraphicsSettingsButton;
    [SerializeField] private Button toAudioSettingsButton;
    

    private void Awake()
    {
        // Settings Menu Buttons Listeners
        toPauseMenuButton.onClick.AddListener(ToPauseMenuButtonClicked);
        toGeneralSettingsButton.onClick.AddListener(ToGeneralSettingsButtonClicked);
        toGraphicsSettingsButton.onClick.AddListener(ToGraphicsSettingsButtonClicked);
        toAudioSettingsButton.onClick.AddListener(ToAudioSettingsButtonClicked);
    }

    private void ToPauseMenuButtonClicked()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
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
