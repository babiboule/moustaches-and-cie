using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public FamilyPictureScriptableObject familyPicture;
    public FamilyInfosScriptableObject familyInfos;
    public CatsScriptableObject cats;

    public Button acceptStampButton;
    public Button declineStampButton;
    public TMP_Text nFolderTMP;
    
    private static List<CatsScriptableObject.Cat> _currentCats = new List<CatsScriptableObject.Cat>();
    private static int _index;
    private static int _indexMax;

    private int m_NFoldersMax;
    private int m_NFolder;

    private LogicManager.Problem m_Problem;
    private List<LogicManager.Problem> m_ListProblems = new List<LogicManager.Problem>();

    private GameManager.GameLevel m_Level;


    private void Awake()
    {
        acceptStampButton.onClick.AddListener(AcceptStampButtonClicked);
        declineStampButton.onClick.AddListener(DeclineStampButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Reset Daily stats
        StatsManager.instance.ResetDailyStats();
        
        // Set the level of the day
        m_Level = GameManager.instance.level;

        // Set the number of folders depending of the level
        switch (m_Level)
        {
            case GameManager.GameLevel.Level1:
                m_NFoldersMax = 5;
                break;
            case GameManager.GameLevel.Level2:
                m_NFoldersMax = 7;
                break;
            case GameManager.GameLevel.Level3:
                m_NFoldersMax = 8;
                break;
            case GameManager.GameLevel.LevelMax:
                m_NFoldersMax = 8;
                break;
            default:
                Debug.Log("NOT ON A LEVEL");
                break;
        }
        
        m_NFolder = 1 ;
        
        NextFolder();
    }

    public void NextFolder()
    {
        nFolderTMP.text = m_NFolder + " / " + m_NFoldersMax; 
        
        // Get list of cats that are not adopted yet
        _currentCats.Clear();
        CatManager.InitialiseCurrentCats(m_Level, cats, _currentCats);
        _index = 0;
        _indexMax = GetIndexMax();
        CatManager.PrintCatInfos(_currentCats[0]);

        // Generate and print the first family folder
        FamilyManager.Family family = FamilyManager.GenerateFamily(familyPicture, familyInfos, _currentCats);
        
        
        // Check the validity of the demand
        m_Problem = LogicManager.CheckProblem(family, family.Cat);
    }
    
    public static List<CatsScriptableObject.Cat> GetCurrentCats()
    {
        return _currentCats;
    }

    public static int GetIndex()
    {
        return _index;
    }
    
    public static int GetIndexMax()
    {
        return _currentCats.Count - 1;
    }

    public static void NextIndex()
    {
        _index++;
        if (_index > GetIndexMax())
            _index = 0;
    }
    
    public static void PreviousIndex()
    {
        _index--;
        if (_index < 0)
            _index = GetIndexMax();
    }
    
    private void AcceptStampButtonClicked()
    {
        if (m_Problem.Exists)
        {
            StatsManager.instance.AddProblemToList(m_Problem);
            StatsManager.instance.AddBadAdoptions();
        }
        else
        {
            StatsManager.instance.AddGoodAdoptions();
        }
        
        StatsManager.instance.AddAdoptedCat(m_Problem.Cat.name);

        if (m_NFolder < m_NFoldersMax)
        {
            m_NFolder++;
            NextFolder();
        }
        else
        {
            GameManager.instance.UpdateGameLevel(GameManager.GameLevel.ScoreLevel);
        }
    }

    private void DeclineStampButtonClicked()
    {
        if (!m_Problem.Exists)
        {
            StatsManager.instance.AddBadDecline();
        }
        if (m_NFolder < m_NFoldersMax)
        {
            m_NFolder++;
            NextFolder();
        }
        else
        {
            GameManager.instance.UpdateGameLevel(GameManager.GameLevel.ScoreLevel);
        }
    }

    private void IDoNothingLol()
    {
        
    }
    
    
}
