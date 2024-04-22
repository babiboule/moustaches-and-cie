using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Game States
    public enum GameState
    {
        Title,
        Play,
        Pause
    }
    public GameState state;
    
    // Game Levels
    public enum GameLevel
    {
        Level1,
        Level2,
        Level3,
        ScoreLevel
    }
    public GameLevel level;
    
    // Events
    public static event Action<GameState> OnGameStateChanged ;
    
    // Singleton
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is NULL !");

            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else Destroy(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Play);
        UpdateGameLevel(GameLevel.Level1);
    }

    // Update is called once per frame
    void Update()
    {
        // Pause or resume the game with Escape key
        if (Input.GetKeyDown("escape"))
        {
            if (state == GameState.Pause)
                UpdateGameState(GameState.Play);

            else if (state == GameState.Play)
                UpdateGameState(GameState.Pause);
        }
    }
    
    // Update the state of the Game
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Title:
                HandleTitle();
                break;
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.Pause:
                HandlePause();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    // Update the Level of the game
    public void UpdateGameLevel(GameLevel newLevel)
    {
        level = newLevel;

        switch (newLevel)
        {
            case GameLevel.Level1:
                HandleLevel1();
                break;
            case GameLevel.Level2:
                HandleLevel2();
                break;
            case GameLevel.Level3:
                HandleLevel3();
                break;
            case GameLevel.ScoreLevel:
                HandleScoreLevel();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newLevel), newLevel, null);
        }
    }

    // Handle the new States
    private void HandleTitle()
    {
        SceneManager.LoadScene("Title screen");
    }

    private void HandlePlay()
    {
        Time.timeScale = 1.0f;
    }

    private void HandlePause()
    {
        Time.timeScale = 0.0f;
    }
    
    // Handle the new Levels
    private void HandleLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    private void HandleLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    private void HandleLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
    
    private void HandleScoreLevel()
    {
        SceneManager.LoadScene("Score screen");
    }
}
