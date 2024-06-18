using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class UIManager : MonoBehaviour
{
    // Panels
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject memoPage1;
    public static GameObject MemoPage2;
    [SerializeField] private GameObject memoPage3;
    public GameObject phonePanel;
    [SerializeField] private GameObject assetsLvl1Panel;
    [SerializeField] private GameObject assetsLvl2Panel;
    
    // Pause components
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button toTitleButton;
    
    // Gameplay components
    [SerializeField] private Button nextMemoButton;
    [SerializeField] private Button previousMemoButton;
    
    // Medals
    [SerializeField] private GameObject bronzeMedal;
    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject goldMedal;
    
    // Sfx
    [SerializeField] private AudioClip buttonSfx;
    [SerializeField] private AudioClip pagingSfx;
    
    // Vfx
    [SerializeField] private Animator animBronzeReflection;
    [SerializeField] private Animator animArgentReflection;
    [SerializeField] private Animator animOrReflection;

    private float _delayReflection = 10f;
    private float _lastTime = 0;
    
    // UI
    public static GameObject ColleaguePanel;
    public static GameObject MemoArrows;
    public static GameObject FamilyPanel;
    public static GameObject CatsPanel;
    public static GameObject MemoPanel;
    public static GameObject StampPanel;
    public static GameObject PhonePanel;
    public static GameObject PhoneOn;
    public static GameObject PhoneOff;
    public static Button PhoneButton;
    public static Button FakePhoneButton;
    public static Button SkipButton;
    
    public GameObject colleaguePanel;
    public GameObject memoArrows;
    public GameObject familyPanel;
    public GameObject catsPanel;
    public GameObject memoPanel;
    public GameObject stampPanel;
    public GameObject memoPage2;

    public GameObject phoneOn;
    public GameObject phoneOff;
    public Button phoneButton;
    public Button fakePhoneButton;
    public Button skipButton;
    private static readonly int OnClick = Animator.StringToHash("OnClick");


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
        
        ColleaguePanel = colleaguePanel;
        MemoArrows = memoArrows;
        FamilyPanel = familyPanel;
        CatsPanel = catsPanel;
        MemoPanel = memoPanel;
        StampPanel = stampPanel;
        MemoPage2 = memoPage2;
        PhonePanel = phonePanel;
        PhoneOff = phoneOff;
        PhoneOn = phoneOn;
        PhoneButton = phoneButton;
        FakePhoneButton = fakePhoneButton;
        SkipButton = skipButton;
        
        skipButton.gameObject.SetActive(false);
        
        // Active medals depending on the level
        bronzeMedal.SetActive(StatsManager.instance.GetLevel() > 1);
        silverMedal.SetActive(StatsManager.instance.GetLevel() > 2);
        goldMedal.SetActive(StatsManager.instance.GetLevel() > 3);
        
        // Active assets depending on the level
        assetsLvl1Panel.SetActive(StatsManager.instance.GetLevel()==1);
        assetsLvl2Panel.SetActive(StatsManager.instance.GetLevel()==2);
        phonePanel.SetActive(StatsManager.instance.GetLevel()>2);
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
        
        // Medals Animation
        if (bronzeMedal.activeSelf && Time.time > _delayReflection + _lastTime)
        {
            animBronzeReflection.Play("Reflection_Bronze");
        }
        if (silverMedal.activeSelf && Time.time > _delayReflection + .2 + _lastTime)
        {
            animArgentReflection.Play("argent_medal");
        }
        if (goldMedal.activeSelf && Time.time > _delayReflection + .4 + _lastTime)
        {
            _lastTime = Time.time;
            animOrReflection.Play("Or_Medal");
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

        // Reset the tuto if running
        if (StatsManager.instance.GetTuto())
            Destroy(TutoManager.instance);
        
        // Launch the title scene
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }

    /*
     * Switch the memo page
     */
    private void MemoButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(pagingSfx);
        
        if(StatsManager.instance.GetLevel()<3)
        {
            memoPage1.SetActive(MemoPage2.activeSelf);
            MemoPage2.SetActive(!memoPage1.activeSelf);
            memoPage3.SetActive(false);
        }
        else
        {
            memoPage1.SetActive(memoPage3.activeSelf);
            MemoPage2.SetActive(false);
            memoPage3.SetActive(!memoPage1.activeSelf);
        }
    }
}