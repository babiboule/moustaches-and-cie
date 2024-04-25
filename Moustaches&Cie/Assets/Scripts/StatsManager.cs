using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Daily stats
    private int m_GoodAdoptions;
    private int m_BadAdoptions;
    private int m_BadDecline;
    private List<LogicManager.Problem> m_ListProblems = new List<LogicManager.Problem>();
    
    // Adopted cats
    private List<string> m_AdoptedCats = new List<string>();
    
    // General stats
    private int m_Level = 1;
    private int m_Exp;
    
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
        m_AdoptedCats.Clear();
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
        return m_AdoptedCats;
    }
    
    public string GetAdoptedCats(int i)
    {
        return m_AdoptedCats[i];
    }

    public int GetExp()
    {
        return m_Exp;
    }
    
    public int GetLevel()
    {
        return m_Level;
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
        m_AdoptedCats.Add(catName);
    }
    
    public void RemoveAdoptedCat(string catName)
    {
        m_AdoptedCats.Remove(catName);
    }

    public void SetLevel(int newLevel)
    {
        m_Level = newLevel;
    }

    public void SetExp(int newExp)
    {
        m_Exp = newExp;
    }
}
