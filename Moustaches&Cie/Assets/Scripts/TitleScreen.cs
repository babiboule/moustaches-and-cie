using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    // Panels 
    //[SerializeField] private GameObject titleScreenPanel;
    
    // UI Components
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button extrasButton;
    [SerializeField] private Button toDesktopButton;

    private void Awake()
    {
        // Title Screen Buttons Listeners
        newGameButton.onClick.AddListener(NewGameButtonClicked);
        continueButton.onClick.AddListener(ContinueButtonClicked);
        optionsButton.onClick.AddListener(OptionsButtonClicked);
        extrasButton.onClick.AddListener(ExtrasButtonClicked);
        toDesktopButton.onClick.AddListener(ToDesktopButtonClicked);
    }
    
    private void NewGameButtonClicked()
    {
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level1);
    }

    private void ContinueButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void OptionsButtonClicked()
    {
        throw new NotImplementedException();
    }
    
    private void ExtrasButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void ToDesktopButtonClicked()
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        continueButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
