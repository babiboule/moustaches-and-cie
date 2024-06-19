using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TitleScreen : MonoBehaviour
{
    /***************** UI Components *****************/
    
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

    // Scriptable Object
    [SerializeField] private CatsScriptableObject cat;
    
    // Sfx
    [SerializeField] private AudioClip buttonSfx;

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

        // Active continue button if there is a save
        continueButton.interactable = PlayerPrefs.GetInt("IsSave") == 1;
        
        GameManager.instance.LoadGame();
    }

    private void Start()
    {
        // Set random pictures of cats in the bg
        var index = Random.Range(0, cat.cats.Count);
        catPicture1.sprite = cat.cats[index].picture;
        var index2 = Random.Range(0, cat.cats.Count);
        while (index2==index)
        {
            index2 = Random.Range(0, cat.cats.Count);
        }
        catPicture2.sprite = cat.cats[index2].picture;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("BIP BOUP");
            PlayerPrefs.DeleteAll();
            SceneManager.LoadSceneAsync("Scenes/Main/Title screen");
        }
    }

    /*
     * Start a new game
     */
    private void NewGameButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        // Reset stats and load the colleague scene
        GameManager.instance.ResetSave();
        GameManager.instance.LoadGame();
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level);
    }

    /*
     * Continue the game
     */
    private void ContinueButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        // Load the stats and load the level scene
        GameManager.instance.LoadGame();
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level);
    }

    /*
     * Print options panel
     */
    private void OptionsButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        optionsPanel.SetActive(true);
    }
    
    /*
     * Set extra buttons and hide the main ones
     */
    private void ExtrasButtonClicked()
    {
        // Sfx 
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        extrasButtons.gameObject.SetActive(true);
        mainButtons.gameObject.SetActive(false);
    }
    
    /*
     * Print the album panel
     */
    private void AlbumButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        // Add sfx on the music
        MusicManager.instance.BgBossaNova(true);
        
        albumPanel.SetActive(true);
    }

    /*
     * Print credits
     */
    private void CreditsButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        GameManager.instance.UpdateGameState(GameManager.GameState.GameOver);
    }

    /*
     * Active the main buttons and hide the extra ones
     */
    private void ReturnButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        extrasButtons.gameObject.SetActive(false);
        mainButtons.gameObject.SetActive(true);
    }

    /*
     * Quit the application
     */
    private void ToDesktopButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        Application.Quit();
    }
}