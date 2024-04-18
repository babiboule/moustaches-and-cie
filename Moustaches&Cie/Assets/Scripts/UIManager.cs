using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Panels
    [SerializeField] private GameObject pause_Panel;
    [SerializeField] private GameObject options_Panel;
    
    // Pause components
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button toTitleButton;
    
    
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged; //Subscribe to the OnStateChange event
        
        // Pause Buttons Listeners
        playButton.onClick.AddListener(PlayButtonClicked);
        optionsButton.onClick.AddListener(OptionsButtonClicked);
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged; //Subscribe to the OnStateChange event
    }

    private void GameManagerOnOnGameStateChanged(GameManager.GameState state)
    {
        pause_Panel.SetActive(state == GameManager.GameState.Pause);
    }

    private void PlayButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameManager.GameState.Play);
    }

    private void OptionsButtonClicked()
    {
        pause_Panel.SetActive(false);
        options_Panel.SetActive(true);
    }
    
    private void ToTitleButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameManager.GameState.Title);
    }
}
