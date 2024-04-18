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
    [SerializeField] private Button toTitleButton;
    
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged; //Subscribe to the OnStateChange event
        
        playButton.onClick.AddListener(PlayButtonClicked);
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameManager.GameState.Play);
    }
    
    private void ToTitleButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameManager.GameState.Title);
    }
}
