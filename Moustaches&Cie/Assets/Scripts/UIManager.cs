using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Panels
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject memoPage1;
    public static GameObject MemoPage2;
    
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
    
    // UI
    public static GameObject ColleaguePanel;
    public static GameObject CatsArrows;
    public static GameObject MemoArrows;
    public static GameObject FamilyPanel;
    public static GameObject CatsPanel;
    public static GameObject MemoPanel;
    public static GameObject StampPanel;
    public static Button SkipButton;
    
    public GameObject colleaguePanel;
    public GameObject catsArrows;
    public GameObject memoArrows;
    public GameObject familyPanel;
    public GameObject catsPanel;
    public GameObject memoPanel;
    public GameObject stampPanel;
    public GameObject memoPage2;
    public Button skipButton;
    
    
    
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
        
        ColleaguePanel = colleaguePanel;
        CatsArrows = catsArrows;
        MemoArrows = memoArrows;
        FamilyPanel = familyPanel;
        CatsPanel = catsPanel;
        MemoPanel = memoPanel;
        StampPanel = stampPanel;
        MemoPage2 = memoPage2;
        SkipButton = skipButton;
        
        skipButton.gameObject.SetActive(false);
        
        // Active medals depending on the level
        bronzeMedal.SetActive(StatsManager.instance.GetLevel() > 1);
        silverMedal.SetActive(StatsManager.instance.GetLevel() > 2);
        goldMedal.SetActive(StatsManager.instance.GetLevel() > 3);

        _postIt = postIt;
        _postItBack = postItBack;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged; //Unsubscribe to the OnStateChange event
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
    
    /*
     * Active the pause panel if the game state changed to Pause
     */
    private void GameManagerOnOnGameStateChanged(GameManager.GameState state)
    {
        pausePanel.SetActive(state == GameManager.GameState.Pause);
    }

    /*
     * Pause the game and display the pause menu
     */
    private void PauseButtonClicked()
    {   // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        pauseButton.gameObject.SetActive(false);
        GameManager.instance.UpdateGameState(GameManager.GameState.Pause);
    }
    
    /*
     * Resume the game
     */
    private void PlayButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        pauseButton.gameObject.SetActive(true);
        GameManager.instance.UpdateGameState(GameManager.GameState.Play);
    }

    /*
     * Go to the options
     */
    private void OptionsButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        optionsPanel.SetActive(true);
    }
    
    /*
     * Go to the title screen
     */
    private void ToTitleButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }

    /*
     * Switch the memo page
     */
    private void MemoButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(pagingSfx);
        
        memoPage1.SetActive(MemoPage2.activeSelf);
        MemoPage2.SetActive(!memoPage1.activeSelf);
    }
    
    /*
     * Print the next cat
     */
    private void NextCatButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(pagingSfx);
        
        DayManager.NextIndex();
        var indexMax = DayManager.GetIndexMax();
        var index = DayManager.GetIndex();
        
        // Set post-it in front if the printed cat is the one asked by the family
        if (DayManager.GetCurrentCats()[index].name != DayManager.GetCurrentFamily().Cat.name)
        {
            SwitchPostIt();
            postItButton.gameObject.SetActive(true);
        }
        else
        {
            SwitchPostIt();
            postItButton.gameObject.SetActive(false);
        }
        
        // End of the game if no cat left
        if (indexMax >= 0)
        {
            CatManager.PrintCatInfos(DayManager.GetCurrentCats()[index]);
        }
        else 
            GameManager.instance.UpdateGameState(GameManager.GameState.GameOver);
    }

    /*
     * Print the previous cat
     */
    private void PreviousCatButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(pagingSfx);
        
        DayManager.PreviousIndex();
        var indexMax = DayManager.GetIndexMax();
        var index = DayManager.GetIndex();

        // Set post-it in front if the printed cat is the one asked by the family
        if (DayManager.GetCurrentCats()[index].name != DayManager.GetCurrentFamily().Cat.name)
        {
            SwitchPostIt();
            postItButton.gameObject.SetActive(true);
        }
        else
        {
            SwitchPostIt();
            postItButton.gameObject.SetActive(false);
        }

        // End of the game if no cat left
        if(indexMax >= 0)
            CatManager.PrintCatInfos(DayManager.GetCurrentCats()[index]);
        else 
            GameManager.instance.UpdateGameState(GameManager.GameState.GameOver);
    }
    
    /*
     * Set the current cat to the one asked by the family
     */
    private void PostItButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(pagingSfx);
        
        for (var i = 0 ; i < DayManager.GetCurrentCats().Count; i++)
        {
            if (DayManager.GetCurrentCats()[i].name == DayManager.GetCurrentFamily().Cat.name)
            {
                DayManager.SetIndex(i);
                CatManager.PrintCatInfos(DayManager.GetCurrentCats()[i]);
                SwitchPostIt();
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