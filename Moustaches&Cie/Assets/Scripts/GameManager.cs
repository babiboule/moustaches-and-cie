using System;
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
        PlayerPrefs.SetInt("Date", 1);
        StatsManager.instance.ClearAdoptedCat();
        
        PlayerPrefs.Save();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("IsSave", 1);
        PlayerPrefs.SetInt("Level", StatsManager.instance.GetLevel());
        PlayerPrefs.SetInt("Exp", StatsManager.instance.GetExp());
        PlayerPrefs.SetInt("AdoptedCats", StatsManager.instance.GetAdoptedCats().Count);
        PlayerPrefs.SetInt("Date", StatsManager.instance.GetDate());
        for (int i = 0; i<StatsManager.instance.GetAdoptedCats().Count; i++)
        {
            PlayerPrefs.SetString("Cat"+i, StatsManager.instance.GetAdoptedCats(i));
        }
        for (int i = 0; i<StatsManager.instance.GetAlbumCats().Count; i++)
        {
            PlayerPrefs.SetString("CatAlbum"+i, StatsManager.instance.GetAlbumCats(i));
        }
        
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        StatsManager.instance.SetLevel(PlayerPrefs.GetInt("Level", 1));
        StatsManager.instance.SetExp(PlayerPrefs.GetInt("Exp", 0));
        int nCats = PlayerPrefs.GetInt("AdoptedCats", 0);
        StatsManager.instance.SetDate(PlayerPrefs.GetInt("Date", 1));
        StatsManager.instance.ClearAdoptedCat();
        for (int i = 0; i < nCats ; i++)
        {
            StatsManager.instance.AddAdoptedCat(PlayerPrefs.GetString("Cat" + i));
        }
        for (int i = 0; i<StatsManager.instance.GetAlbumCats().Count; i++)
        {
            StatsManager.instance.AddAlbumCat(PlayerPrefs.GetString("CatAlbum"+i));
        }
    }
    
    public void SavePrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume", StatsManager.instance.GetMusicVolume());
        PlayerPrefs.SetFloat("SfxVolume", StatsManager.instance.GetSfxVolume());
        PlayerPrefs.Save();
    }
 
    public void LoadPrefs()
    {
        StatsManager.instance.SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1));
        StatsManager.instance.SetSfxVolume(PlayerPrefs.GetFloat("SfxVolume", 1));
    }

    // Handle the new States
    private void HandleTitle()
    {
        SceneManager.LoadSceneAsync("Title screen");
    }

    private void HandlePlay()
    {
        MusicManager.instance.SwitchBossaNova(0);
    }

    private void HandlePause()
    {
        MusicManager.instance.SwitchBossaNova(1);
    }

    private void HandleGameOver()
    {MusicManager.instance.SwitchBossaNova(1);
        SceneManager.LoadSceneAsync("Game Over");
        
    }
    
    // Handle the new Levels
    private void HandleLevel1()
    {MusicManager.instance.SwitchBossaNova(0);
        SceneManager.LoadSceneAsync("Level 1");
        
    }

    private void HandleLevel2()
    {MusicManager.instance.SwitchBossaNova(0);
        SceneManager.LoadSceneAsync("Level 2");
        
    }

    private void HandleLevel3()
    {MusicManager.instance.SwitchBossaNova(0);
        SceneManager.LoadSceneAsync("Level 3");
        
    }

    private void HandleLevelMax()
    {MusicManager.instance.SwitchBossaNova(0);
        SceneManager.LoadSceneAsync("Level Max");
        
    }
    
    private void HandleScoreLevel()
    {MusicManager.instance.SwitchBossaNova(1);
        SceneManager.LoadSceneAsync("Score screen");
        
    }
}
