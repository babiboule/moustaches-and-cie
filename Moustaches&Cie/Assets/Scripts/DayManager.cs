using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public FamilyPictureScriptableObject familyPicture;
    public FamilyInfosScriptableObject familyInfos;
    public CatsScriptableObject cats;
    
    private static List<CatsScriptableObject.Cat> _currentCats = new List<CatsScriptableObject.Cat>();
    private static int _index;
    private static int _indexMax;

    private int nFoldersMax;
    private int nFolder;

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
                nFoldersMax = 5;
                break;
            case GameManager.GameLevel.Level2:
                nFoldersMax = 7;
                break;
            case GameManager.GameLevel.Level3:
                nFoldersMax = 8;
                break;
            case GameManager.GameLevel.LevelMax:
                nFoldersMax = 8;
                break;
            default:
                Debug.Log("NOT ON A LEVEL");
                break;
        }
        
        // Generate and print the first family folder
        FamilyManager.Family family = FamilyManager.GenerateFamily(familyPicture, familyInfos, cats);
        
        // Get list of cats that are not adopted yet
        CatManager.InitialiseCurrentCats(m_Level, cats, _currentCats);
        _index = 0;
        _indexMax = GetIndexMax();
        CatManager.PrintCatInfos(_currentCats[0]);
        
     
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
