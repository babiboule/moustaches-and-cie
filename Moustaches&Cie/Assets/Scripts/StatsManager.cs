using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class StatsManager : MonoBehaviour
{
    // Daily stats
    private int m_GoodAdoptions;
    private int m_BadAdoptions;
    private int m_BadDecline;
    private List<LogicManager.Problem> m_ListProblems = new List<LogicManager.Problem>();
    
    // Adopted cats
    private List<string> AdoptedCats = new List<string>();
    
    // General stats
    private int level = 1;
    private int exp = 0;
    
    // Singleton
    private static StatsManager _instance;
    public static StatsManager instance
    {
        get
        {
            if (_instance == null)
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

    public void ResetDailyStats()
    {
        m_GoodAdoptions = 0;
        m_BadAdoptions = 0;
        m_BadDecline = 0;
        m_ListProblems.Clear();
    }

    public void ClearAdoptedCat()
    {
        AdoptedCats.Clear();
    }

    public int GetGoodAdoptions()
    {
        return m_GoodAdoptions;
    }
    
    public int GetBadAdoptions()
    {
        return m_BadAdoptions;
    }

    public int GetBadDecline()
    {
        return m_BadDecline;
    }

    public List<LogicManager.Problem> GetListProblems()
    {
        return m_ListProblems;
    }

    public List<string> GetAdoptedCats()
    {
        return AdoptedCats;
    }
    
    public string GetAdoptedCats(int i)
    {
        return AdoptedCats[i];
    }

    public int GetExp()
    {
        return exp;
    }
    
    public int GetLevel()
    {
        return level;
    }

    public void AddGoodAdoptions()
    {
        m_GoodAdoptions++;
    }
    
    public void AddBadAdoptions()
    {
        m_BadAdoptions++;
    }
    
    public void AddBadDecline()
    {
        m_BadDecline++;
    }

    public void AddProblemToList(LogicManager.Problem problem)
    {
        m_ListProblems.Add(problem);
    }

    public void AddAdoptedCat(string catName)
    {
        AdoptedCats.Add(catName);
    }
    
    public void RemoveAdoptedCat(string catName)
    {
        AdoptedCats.Remove(catName);
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }

    public void SetExp(int newExp)
    {
        exp = newExp;
    }
}
