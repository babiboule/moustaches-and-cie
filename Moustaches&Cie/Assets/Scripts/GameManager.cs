using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Game States
    public enum GameState
    {
        Play,
        Pause,
        GameOver
    }
    public GameState state;
    
    // Game Levels
    public enum GameLevel
    {
        Title,
        Level1,
        Level2,
        Level3,
        LevelMax,
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
            {
                Debug.LogError("Game Manager is NULL !");
            }
            
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(_instance);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Play);
        UpdateGameLevel(GameLevel.Title);
    }
    
    // Update the state of the Game
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.Pause:
                HandlePause();
                break;
            case GameState.GameOver:
                HandleGameOver();
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
            case GameLevel.Title:
                HandleTitle();
                break;
            case GameLevel.Level1:
                HandleLevel1();
                break;
            case GameLevel.Level2:
                HandleLevel2();
                break;
            case GameLevel.Level3:
                HandleLevel3();
                break;
            case GameLevel.LevelMax:
                HandleLevelMax();
                break;
            case GameLevel.ScoreLevel:
                HandleScoreLevel();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newLevel), newLevel, null);
        }
    }
    
    public void ResetSave()
    {
        PlayerPrefs.SetInt("IsSave", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Exp", 0);
        PlayerPrefs.SetInt("AdoptedCats", 0);
        StatsManager.instance.AdoptedCats.Clear();
        
        PlayerPrefs.Save();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("IsSave", 1);
        PlayerPrefs.SetInt("Level", StatsManager.instance.level);
        PlayerPrefs.SetInt("Exp", StatsManager.instance.exp);
        PlayerPrefs.SetInt("AdoptedCats", StatsManager.instance.AdoptedCats.Count);
        for (int i = 0; i<StatsManager.instance.AdoptedCats.Count; i++)
        {
            PlayerPrefs.SetString("Cat"+i, StatsManager.instance.AdoptedCats[i]);
        }
        
        Debug.Log(PlayerPrefs.GetInt("AdoptedCats"));
        Debug.Log(PlayerPrefs.GetString("Cat0"));
        
        PlayerPrefs.Save();

        Debug.Log(StatsManager.instance.AdoptedCats.Count);
        Debug.Log(StatsManager.instance.AdoptedCats[0]);
        Debug.Log(PlayerPrefs.GetInt("AdoptedCats"));
        Debug.Log(PlayerPrefs.GetString("Cat0"));
    }

    public void LoadGame()
    {
        StatsManager.instance.level = PlayerPrefs.GetInt("Level", 1);
        StatsManager.instance.exp = PlayerPrefs.GetInt("Exp", 0);
        int nCats = PlayerPrefs.GetInt("AdoptedCats", 0);
        StatsManager.instance.AdoptedCats.Clear();
        for (int i = 0; i < nCats ; i++)
        {
            StatsManager.instance.AdoptedCats.Add(PlayerPrefs.GetString("Cat" + i));
        }
    }
    
    public void SavePrefs()
    {
        PlayerPrefs.SetInt("Level", StatsManager.instance.level);
        PlayerPrefs.Save();
    }
 
    public void LoadPrefs()
    {
        int volume = PlayerPrefs.GetInt("Level", 1); 
    }

    // Handle the new States
    private void HandleTitle()
    {
        SceneManager.LoadSceneAsync("Title screen");
    }

    private void HandlePlay()
    {
        Time.timeScale = 1.0f;
    }

    private void HandlePause()
    {
        Time.timeScale = 0.0f;
    }

    private void HandleGameOver()
    {
        SceneManager.LoadSceneAsync("GameOver screen");
    }
    
    // Handle the new Levels
    private void HandleLevel1()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    private void HandleLevel2()
    {
        SceneManager.LoadSceneAsync("Level 2");
    }

    private void HandleLevel3()
    {
        SceneManager.LoadSceneAsync("Level 3");
    }

    private void HandleLevelMax()
    {
        SceneManager.LoadSceneAsync("Level Max");
    }
    
    private void HandleScoreLevel()
    {
        SceneManager.LoadSceneAsync("Score screen");
    }
}
