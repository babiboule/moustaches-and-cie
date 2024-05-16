using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Panels
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject memoPage1;
    [SerializeField] private GameObject memoPage2;
    
    // Pause components
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button toTitleButton;
    
    // Gameplay components
    [SerializeField] private Button nextMemoButton;
    [SerializeField] private Button previousMemoButton;
    [SerializeField] private Button nextCatButton;
    [SerializeField] private Button previousCatButton;
    [SerializeField] private Button postItButton;
    [SerializeField] private GameObject postIt;
    [SerializeField] private GameObject postItBack;
    private static GameObject _postIt;
    private static GameObject _postItBack;
    
    // Medals
    [SerializeField] private GameObject bronzeMedal;
    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject goldMedal;
    
    // Sfx
    [SerializeField] private AudioClip buttonSfx;
    [SerializeField] private AudioClip pagingSfx;
    
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged; //Subscribe to the OnStateChange event
        
        // Pause Buttons Listeners
        pauseButton.onClick.AddListener(PauseButtonClicked);
        playButton.onClick.AddListener(PlayButtonClicked);
        optionsButton.onClick.AddListener(OptionsButtonClicked);
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
        
        nextMemoButton.onClick.AddListener(MemoButtonClicked);
        previousMemoButton.onClick.AddListener(MemoButtonClicked);
        nextCatButton.onClick.AddListener(NextCatButtonClicked);
        previousCatButton.onClick.AddListener(PreviousCatButtonClicked);
        postItButton.onClick.AddListener(PostItButtonClicked);
        
        bronzeMedal.SetActive(StatsManager.instance.GetLevel() > 1);
        silverMedal.SetActive(StatsManager.instance.GetLevel() > 2);
        goldMedal.SetActive(StatsManager.instance.GetLevel() > 3);

        _postIt = postIt;
        _postItBack = postItBack;
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
        SfxManager.instance.PlaySfxClip(buttonSfx);
        pauseButton.gameObject.SetActive(false);
        GameManager.instance.UpdateGameState(GameManager.GameState.Pause);
    }
    private void PlayButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        pauseButton.gameObject.SetActive(true);
        GameManager.instance.UpdateGameState(GameManager.GameState.Play);
    }

    private void OptionsButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        optionsPanel.SetActive(true);
    }
    
    private void ToTitleButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }

    private void MemoButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(pagingSfx);
        memoPage1.SetActive(memoPage2.activeSelf);
        memoPage2.SetActive(!memoPage1.activeSelf);
    }
    
    private void NextCatButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(pagingSfx);
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
        SfxManager.instance.PlaySfxClip(pagingSfx);
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
        SfxManager.instance.PlaySfxClip(pagingSfx);
        for (var i = 0 ; i < DayManager.GetCurrentCats().Count; i++)
        {
            if (DayManager.GetCurrentCats()[i].name == DayManager.GetCurrentFamily().Cat.name)
            {
                DayManager.SetIndex(i);
                CatManager.PrintCatInfos(DayManager.GetCurrentCats()[i]);
                postIt.SetActive(true);
                postItBack.SetActive(false);
                //postItButton.gameObject.SetActive(false);
                return;
            }
        }
    }

    public static void SwitchPostIt()
    {
            if (DayManager.GetCurrentCats()[DayManager.GetIndex()].name == DayManager.GetCurrentFamily().Cat.name)
            {
                _postIt.SetActive(true);
                _postItBack.SetActive(false);
            }
            else
            {
                _postIt.SetActive(false);
                _postItBack.SetActive(true);
            }
    }
}
