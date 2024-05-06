using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TitleScreen : MonoBehaviour
{
    ////////////////// UI Components ///////////////////
    // Panels 
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject albumPanel;
    [SerializeField] private GameObject mainButtons;
    [SerializeField] private GameObject extrasButtons;
    
    // Buttons
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button extrasButton;
    [SerializeField] private Button albumButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button toDesktopButton;
    
    // Images
    [SerializeField] private Image catPicture1;
    [SerializeField] private Image catPicture2;

    [SerializeField] private CatsScriptableObject cat;

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

    private void Start()
    {
        int index = Random.Range(0, cat.cats.Count);
        catPicture1.sprite = cat.cats[index].picture;
        int index2 = Random.Range(0, cat.cats.Count);
        while (index2==index)
        {
            index2 = Random.Range(0, cat.cats.Count);
        }
        catPicture2.sprite = cat.cats[index2].picture;
    }

    private void NewGameButtonClicked()
    {
        GameManager.instance.ResetSave();
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level);
    }

    private void ContinueButtonClicked()
    {
        GameManager.instance.LoadGame();
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level);
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
        albumPanel.SetActive(true);
        MusicManager.instance.SwitchBossaNova(1);
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
