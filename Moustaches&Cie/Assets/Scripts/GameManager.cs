using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Game States
    public enum GameState
    {
        Title,
        Play,
        Pause
    }
    public GameState state;
    
    public enum GameLevel
    {
        Level1,
        Level2,
        Level3,
        ScoreLevel
    }
    public GameLevel level;
    
    //Singleton
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
        UpdateGameState(GameState.Title);
        UpdateGameLevel(GameLevel.Level1);
    }

    // Update is called once per frame
    void Update()
    {
        //Pause or resume the game with Escape key
        if (Input.GetKeyDown("escape"))
        {
            if (state == GameState.Pause)
                UpdateGameState(GameState.Play);

            else if (state == GameState.Play)
                UpdateGameState(GameState.Pause);
        }
    }
    
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
    }

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
    
    private void HandleScoreLevel()
    {
        SceneManager.LoadSceneAsync("Score screen");
    }
}
