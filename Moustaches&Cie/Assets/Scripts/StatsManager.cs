using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Debug settings
    public int dayMax;
    public int upExp;
    public int upLvl1;
    public int upLvl2;
    public int upLvl3;
    
    // Daily stats
    private int _goodAdoptions;
    private int _badAdoptions;
    private int _badDecline;
    private readonly List<LogicManager.Problem> _listProblems = new();
    
    // Adopted cats
    private readonly List<string> _adoptedCats = new();
    private readonly List<string> _albumCats = new();
    
    // General stats
    private int _level = 1;
    private int _exp;
    private int _date = 1;
    
    // Settings
    private float _musicVolume = 1;
    private float _sfxVolume = 1;
    
    // Singleton
    private static StatsManager _instance;
    public static StatsManager instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Stats Manager is NULL !");
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

    private void Start()
    {
        // Load the player prefs
        GameManager.instance.LoadPrefs();
    }

    /*
     * Reset the default stats of the day
     */
    public void ResetDailyStats()
    {
        _goodAdoptions = 0;
        _badAdoptions = 0;
        _badDecline = 0;
        _listProblems.Clear();
    }

    /*
     * Clear the list of adopted cats
     */
    public void ClearAdoptedCat()
    {
        _adoptedCats.Clear();
    }

    /***************** Getters and Setters *****************/
    
    public int GetGoodAdoptions()
    {
        return _goodAdoptions;
    }

    public int GetBadAdoptions()
    {
        return _badAdoptions;
    }

    public int GetBadDecline()
    {
        return _badDecline;
    }
    
    public List<LogicManager.Problem> GetListProblems()
    {
        return _listProblems;
    }

    public List<string> GetAdoptedCats()
    {
        return _adoptedCats;
    }
    
    public string GetAdoptedCats(int i)
    {
        return _adoptedCats[i];
    }
    
    public List<string> GetAlbumCats()
    {
        return _albumCats;
    }

    public string GetAlbumCats(int i)
    {
        return _albumCats[i];
    }

    public int GetExp()
    {
        return _exp;
    }

    public int GetLevel()
    {
        return _level;
    }
    
    public int GetDate()
    {
        return _date;
    }
    
    public float GetMusicVolume()
    {
        return _musicVolume;
    }
    
    public float GetSfxVolume()
    {
        return _sfxVolume;
    }
    
    public void AddGoodAdoptions()
    {
        _goodAdoptions++;
    }

    public void AddBadAdoptions()
    {
        _badAdoptions++;
    }
    
    public void AddBadDecline()
    {
        _badDecline++;
    }
    
    public void AddProblemToList(LogicManager.Problem problem)
    {
        _listProblems.Add(problem);
    }

    public void AddAdoptedCat(string catName)
    {
        _adoptedCats.Add(catName);
    }
    
    public void RemoveAdoptedCat(string catName)
    {
        _adoptedCats.Remove(catName);
    }
    
    public void AddAlbumCat(string catName)
    {
        _albumCats.Add(catName);
    }

    public void SetLevel(int newLevel)
    {
        _level = newLevel;
    }

    public void SetExp(int newExp)
    {
        _exp = newExp;
    }
    
    public void SetDate(int newDate)
    {
        _date = newDate;
    }
    
    public void AddDate()
    {
        _date ++;
    }

    public void SetMusicVolume(float value)
    {
        _musicVolume = value;
    }
    
    public void SetSfxVolume(float value)
    {
        _sfxVolume = value;
    }


}