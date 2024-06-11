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
        Colleague,
        Level,
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
    private void Start()
    {
        UpdateGameState(GameState.Play);
        UpdateGameLevel(GameLevel.Title);
    }

    /*
     * Update the Param newState of the Game
     */
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
        OnGameStateChanged?.Invoke(newState); // Invoke event if something has subscribed to it
    }

    /*
     * Update the Param newLevel of the game
     */
    public void UpdateGameLevel(GameLevel newLevel)
    {
        level = newLevel;

        switch (newLevel)
        {
            case GameLevel.Title:
                HandleTitle();
                break;
            case GameLevel.Colleague:
                HandleColleague();
                break;
            case GameLevel.Level:
                HandleLevel();
                break;
            case GameLevel.ScoreLevel:
                HandleScoreLevel();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newLevel), newLevel, null);
        }
    }
    
    /*
     * Reset the game stats from the PlayerPrefs doc
     */
    public void ResetSave()
    {
        PlayerPrefs.SetInt("IsSave", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Exp", 0);
        PlayerPrefs.SetInt("AdoptedCats", 0);
        PlayerPrefs.SetInt("Date", 1);
        PlayerPrefs.SetInt("Day", 0);
        PlayerPrefs.SetInt("Tuto", 1);
        StatsManager.instance.ClearAdoptedCat();
        
        PlayerPrefs.Save();
    }

    /*
     * Save the current stats into the PlayerPrefs doc
     */
    public void SaveGame()
    {
        PlayerPrefs.SetInt("IsSave", 1);
        PlayerPrefs.SetInt("Level", StatsManager.instance.GetLevel());
        PlayerPrefs.SetInt("Exp", StatsManager.instance.GetExp());
        PlayerPrefs.SetInt("AdoptedCats", StatsManager.instance.GetAdoptedCats().Count);
        PlayerPrefs.SetInt("Date", StatsManager.instance.GetDate());
        PlayerPrefs.SetInt("Day", StatsManager.instance.day);
        PlayerPrefs.SetInt("Tuto", StatsManager.instance.GetTutoLvl());
        for (int i = 0; i<StatsManager.instance.GetAdoptedCats().Count; i++)
        {
            PlayerPrefs.SetString("Cat"+i, StatsManager.instance.GetAdoptedCats(i));
        }
        for (int i = 0; i<StatsManager.instance.GetAlbumCats().Count; i++)
        {
            PlayerPrefs.SetString("CatAlbum"+i, StatsManager.instance.GetAlbumCats(i));
        }
        
        if(PlayerPrefs.GetInt("MaxLevel") < StatsManager.instance.GetLevel())
            PlayerPrefs.SetInt("MaxLevel", StatsManager.instance.GetLevel());
        
        PlayerPrefs.Save();
    }

    /*
     * Load the last saved stats from the PlayerPrefs doc
     */
    public void LoadGame()
    {
        StatsManager.instance.SetLevel(PlayerPrefs.GetInt("Level", 1));
        StatsManager.instance.SetExp(PlayerPrefs.GetInt("Exp", 0));
        int nCats = PlayerPrefs.GetInt("AdoptedCats", 0);
        StatsManager.instance.SetDate(PlayerPrefs.GetInt("Date", 1));
        StatsManager.instance.ClearAdoptedCat();
        StatsManager.instance.day = PlayerPrefs.GetInt("Day", 0);
        StatsManager.instance.SetTutoLevel(PlayerPrefs.GetInt("Tuto", 1));
        for (int i = 0; i < nCats ; i++)
        {
            StatsManager.instance.AddAdoptedCat(PlayerPrefs.GetString("Cat" + i));
        }
        for (int i = 0; i<StatsManager.instance.GetAlbumCats().Count; i++)
        {
            if(!StatsManager.instance.GetAlbumCats().Contains(PlayerPrefs.GetString("CatAlbum"+i)))
                StatsManager.instance.AddAlbumCat(PlayerPrefs.GetString("CatAlbum"+i));
        }
    }
    
    /*
     * Save the player prefs into the PlayerPrefs doc
     */
    public void SavePrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume", StatsManager.instance.GetMusicVolume());
        PlayerPrefs.SetFloat("SfxVolume", StatsManager.instance.GetSfxVolume());
        PlayerPrefs.Save();
    }
 
    /*
     * Load the saved player prefs from the PlayerPrefs doc
     */
    public void LoadPrefs()
    {
        StatsManager.instance.SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1));
        StatsManager.instance.SetSfxVolume(PlayerPrefs.GetFloat("SfxVolume", 1));
    }

    
  /**********************  Handle the changing states  **********************/
  
    private void HandleColleague()
    {
        // Load the colleague scene
        SceneManager.LoadSceneAsync("Colleague");
    }
    private void HandleTitle()
    {
        // Load the title screen scene
        SceneManager.LoadSceneAsync("Title screen");
    }

    private void HandlePlay()
    {
        // Remove the fx from the music
        MusicManager.instance.BgBossaNova(false);
    }

    private void HandlePause()
    {
        // Add the fx on the music
        MusicManager.instance.BgBossaNova(true);
    }

    private void HandleGameOver()
    {
        // Add the fx on the music
        MusicManager.instance.BgBossaNova(true);
        // Load the game over scene
        SceneManager.LoadSceneAsync("Game Over");
    }
    
    // Handle the new Levels
    private void HandleLevel()
    {
        // Remove the fx on the music
        MusicManager.instance.BgBossaNova(false);
        // Load the level scene
        SceneManager.LoadSceneAsync("Level");
    }
    
    private void HandleScoreLevel()
    {
        // Add the fx on the music
        MusicManager.instance.BgBossaNova(true);
        // Load the score screen scene
        SceneManager.LoadSceneAsync("Score screen");
    }
}