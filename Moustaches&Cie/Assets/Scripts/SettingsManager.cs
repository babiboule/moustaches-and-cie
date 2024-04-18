using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Panels
    [SerializeField] private GameObject pause_Panel;
    [SerializeField] private GameObject options_Panel;
    
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
        options_Panel.SetActive(false);
        pause_Panel.SetActive(true);
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
