using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Panels
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    
    // Pause components
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button toTitleButton;
    
    // Gameplay components
    [SerializeField] private Button nextCatButton;
    [SerializeField] private Button previousCatButton;

    
    
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged; //Subscribe to the OnStateChange event
        
        // Pause Buttons Listeners
        pauseButton.onClick.AddListener(PauseButtonClicked);
        playButton.onClick.AddListener(PlayButtonClicked);
        optionsButton.onClick.AddListener(OptionsButtonClicked);
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
        
        nextCatButton.onClick.AddListener(NextCatButtonClicked);
        previousCatButton.onClick.AddListener(PreviousCatButtonClicked);
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged; //Subscribe to the OnStateChange event
    }

    private void Start()
    {

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

    private void PauseButtonClicked()
    {
        pauseButton.gameObject.SetActive(false);
        GameManager.instance.UpdateGameState(GameManager.GameState.Pause);
    }
    private void PlayButtonClicked()
    {
        pauseButton.gameObject.SetActive(true);
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
    
    private void NextCatButtonClicked()
    {
        DayManager.NextIndex();
        int indexMax = DayManager.GetIndexMax();
        int index = DayManager.GetIndex();

        if (indexMax > 0)
        {
            CatManager.PrintCatInfos(DayManager.GetCurrentCats()[index]);
        }
        else 
            GameManager.instance.UpdateGameState(GameManager.GameState.GameOver);
    }

    private void PreviousCatButtonClicked()
    {
        DayManager.PreviousIndex();
        int indexMax = DayManager.GetIndexMax();
        int index = DayManager.GetIndex();
        
        if(indexMax > 0)
            CatManager.PrintCatInfos(DayManager.GetCurrentCats()[index]);
        else 
            GameManager.instance.UpdateGameState(GameManager.GameState.GameOver);
    }
}
