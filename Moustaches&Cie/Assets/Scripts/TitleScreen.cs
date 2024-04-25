using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    // Panels 
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject mainButtons;
    [SerializeField] private GameObject extrasButtons;
    
    // UI Components
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button extrasButton;
    [SerializeField] private Button albumButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button toDesktopButton;
    

    private void Awake()
    {
        // Title Screen Buttons Listeners
        newGameButton.onClick.AddListener(NewGameButtonClicked);
        continueButton.onClick.AddListener(ContinueButtonClicked);
        optionsButton.onClick.AddListener(OptionsButtonClicked);
        extrasButton.onClick.AddListener(ExtrasButtonClicked);
        albumButton.onClick.AddListener(AlbumButtonClicked);
        creditsButton.onClick.AddListener(CreditsButtonClicked);
        returnButton.onClick.AddListener(ReturnButtonClicked);
        toDesktopButton.onClick.AddListener(ToDesktopButtonClicked);

        continueButton.interactable = false;
        if (PlayerPrefs.GetInt("IsSave") == 1)
        {
            continueButton.interactable = true;
        }
    }

    private void NewGameButtonClicked()
    {
        GameManager.instance.ResetSave();
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level1);
    }

    private void ContinueButtonClicked()
    {
        GameManager.instance.LoadGame();
        switch (StatsManager.instance.GetLevel())
       {
           case 1:
               GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level1);
               break;
           case 2:
               GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level2);
               break;
           case 3 :
               GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level3);
               break;
           case 4 :
               GameManager.instance.UpdateGameLevel(GameManager.GameLevel.LevelMax);
               break;
           default:
               Debug.Log("LEVEL PROBLEM");
               break;
       }
    }

    private void OptionsButtonClicked()
    {
        optionsPanel.SetActive(true);
    }
    
    private void ExtrasButtonClicked()
    {
        extrasButtons.gameObject.SetActive(true);
        mainButtons.gameObject.SetActive(false);
    }
    
    private void AlbumButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void CreditsButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void ReturnButtonClicked()
    {
        extrasButtons.gameObject.SetActive(false);
        mainButtons.gameObject.SetActive(true);
    }

    private void ToDesktopButtonClicked()
    {
        Application.Quit();
    }

}
