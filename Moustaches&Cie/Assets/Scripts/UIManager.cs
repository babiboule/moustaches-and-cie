using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Panels
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    
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

    private void Update()
    {
        // Pause or resume the game with Escape key
        if (Input.GetKeyDown("escape"))
        {
            if (GameManager.instance.state == GameManager.GameState.Pause)
                GameManager.instance.UpdateGameState(GameManager.GameState.Play);

            else if (GameManager.instance.state == GameManager.GameState.Play)
                GameManager.instance.UpdateGameState(GameManager.GameState.Pause);
        }
    }

    private void GameManagerOnOnGameStateChanged(GameManager.GameState state)
    {
        pausePanel.SetActive(state == GameManager.GameState.Pause);
    }

    private void PlayButtonClicked()
    {
        GameManager.instance.UpdateGameState(GameManager.GameState.Play);
    }

    private void OptionsButtonClicked()
    {
        optionsPanel.SetActive(true);
    }
    
    private void ToTitleButtonClicked()
    {
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }
}
