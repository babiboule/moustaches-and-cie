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
    [SerializeField] private Button postItButton;
    [SerializeField] private GameObject postIt;
    [SerializeField] private GameObject postItBack;
    
    
    // Medals
    [SerializeField] private GameObject bronzeMedal;
    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject goldMedal;
    
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
        postItButton.onClick.AddListener(PostItButtonClicked);
        
        bronzeMedal.SetActive(StatsManager.instance.GetLevel() > 1);
        silverMedal.SetActive(StatsManager.instance.GetLevel() > 2);
        goldMedal.SetActive(StatsManager.instance.GetLevel() > 3);
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
        
        if (DayManager.GetCurrentCats()[index].name != DayManager.GetCurrentFamily().Cat.name)
        {
            postIt.SetActive(false);
            postItBack.SetActive(true);
            postItButton.gameObject.SetActive(true);
        }
        else
        {
            postIt.SetActive(true);
            postItBack.SetActive(false);
            postItButton.gameObject.SetActive(false);
        }
        
        if (indexMax >= 0)
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

        if (DayManager.GetCurrentCats()[index].name != DayManager.GetCurrentFamily().Cat.name)
        {
            postIt.SetActive(false);
            postItBack.SetActive(true);
            postItButton.gameObject.SetActive(true);
        }
        else
        {
            postIt.SetActive(true);
            postItBack.SetActive(false);
            postItButton.gameObject.SetActive(false);
        }

        if(indexMax >= 0)
            CatManager.PrintCatInfos(DayManager.GetCurrentCats()[index]);
        else 
            GameManager.instance.UpdateGameState(GameManager.GameState.GameOver);
    }
    
    private void PostItButtonClicked()
    {
        for (var i = 0 ; i < DayManager.GetCurrentCats().Count; i++)
        {
            if (DayManager.GetCurrentCats()[i].name == DayManager.GetCurrentFamily().Cat.name)
            {
                DayManager.SetIndex(i);
                CatManager.PrintCatInfos(DayManager.GetCurrentCats()[i]);
                postIt.SetActive(true);
                postItBack.SetActive(false);
                postItButton.gameObject.SetActive(false);
                return;
            }
        }
    }
}
