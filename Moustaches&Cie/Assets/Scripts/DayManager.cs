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
    private List<LogicManager.Problem> m_ListProblems;

    private GameManager.GameLevel m_Level;

    // Start is called before the first frame update
    void Start()
    {
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
        
        // Generate and print the first family folder
        FamilyManager.Family family = FamilyManager.GenerateFamily(familyPicture, familyInfos, cats);
        
        // Get list of cats that are not adopted yet
        CatManager.InitialiseCurrentCats(m_Level, cats, _currentCats);
        _index = 0;
        _indexMax = GetIndexMax();
        CatManager.PrintCatInfos(_currentCats[0]);
        
        // Check the validity of the demand
        m_Problem = LogicManager.CheckProblem(family, family.Cat);
        
        acceptStampButton.onClick.AddListener(AcceptStampButtonClicked);
        declineStampButton.onClick.AddListener(DeclineStampButtonClicked);
        
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
        acceptStampButton.onClick.RemoveListener(IDoNothingLol);
        declineStampButton.onClick.RemoveListener(IDoNothingLol);
        
        if (m_Problem.Exists)
        {
            m_ListProblems.Add(m_Problem);
        }
        
        m_Problem.Cat.adopted = true;

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
        acceptStampButton.onClick.RemoveListener(IDoNothingLol);
        declineStampButton.onClick.RemoveListener(IDoNothingLol);
        
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
