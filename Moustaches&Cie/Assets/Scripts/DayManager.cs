using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public FamilyPictureScriptableObject familyPicture;
    public FamilyInfosScriptableObject familyInfos;
    public CatsScriptableObject cats;

    [SerializeField] private Button acceptStampButton;
    [SerializeField] private Button declineStampButton;
    private static Button _acceptStampButton;
    private static Button _declineStampButton;
    public TMP_Text nFolderTMP;
    public TMP_Text dayTMP;
    
    private static List<CatsScriptableObject.Cat> _currentCats = new List<CatsScriptableObject.Cat>();
    private static int _index;


    private int m_NFoldersMax;
    private int m_NFolder;
    private List<bool> m_ValidFolders = new List<bool>();

    private LogicManager.Problem m_Problem;
    private int m_Level;

    private void Awake()
    {
        acceptStampButton.onClick.AddListener(AcceptStampButtonClicked);
        declineStampButton.onClick.AddListener(DeclineStampButtonClicked);

        _acceptStampButton = acceptStampButton;
        _declineStampButton = declineStampButton;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Reset Daily stats
        StatsManager.instance.ResetDailyStats();
        
        // Set the level and the day
        m_Level = StatsManager.instance.GetLevel();
        dayTMP.text = StatsManager.instance.GetDate() + "/" + StatsManager.instance.dayMax;

        // Set the number of folders depending of the level
        switch (m_Level)
        {
            case 1:
                m_NFoldersMax = 5;
                break;
            case 2:
                m_NFoldersMax = 7;
                break;
            case 3:
                m_NFoldersMax = 8;
                break;
            case 4:
                m_NFoldersMax = 8;
                break;
            default:
                Debug.Log("NOT ON A LEVEL");
                break;
        }

        m_ValidFolders = SetValidFolders();
        
        m_NFolder = 1 ;
        _index = 0 ;
        
        // Get list of cats that are not adopted yet
        _currentCats.Clear();
        CatManager.InitialiseCurrentCats(m_Level, cats, _currentCats);
        // Set the first cat page
        CatManager.PrintCatInfos(_currentCats[0]);
        
        if(StatsManager.instance.GetLevel()>1)
            SetDeclineButtonActive(false);
        
        NextFolder();
    }

    private List<bool> SetValidFolders()
    {
        // Define the value of valid folders for the day
        List<bool> tempList = new List<bool>();
        List<bool> list = new List<bool>();
        int nbValid = Random.Range(Mathf.FloorToInt(m_NFoldersMax / 2.0f), Mathf.FloorToInt(m_NFoldersMax / 2.0f)*2);
        
        // Set a temp list
        for (int i = 0; i < m_NFoldersMax-nbValid; i++)
        {
            tempList.Add(false);
        }
        for (int i = 0; i < nbValid; i++)
        {
            tempList.Add(true);
        }
        

        // Shuffle the list
        while (tempList.Count > 0)
        {
            int rand = Random.Range(0, tempList.Count);
            bool temp = tempList[rand];
            list.Add(temp);
            tempList.Remove(temp);
        }
        
        return list;
    }

    private void NextFolder()
    {
        ////////////// DEBUG //////////////////
        Debug.Log(m_ValidFolders[m_NFolder-1]);
        
        // Update n° folder text
        nFolderTMP.text = "Dossier n°" + m_NFolder + " / " + m_NFoldersMax; 
        
        // Get list of cats that are not adopted yet
        _currentCats.Clear();
        CatManager.InitialiseCurrentCats(m_Level, cats, _currentCats);
        
        // Generate Family
        FamilyManager.Family family = FamilyManager.GenerateFamily(familyPicture, familyInfos, _currentCats);
        
        // Check the validity of the first generated family
        m_Problem = LogicManager.CheckProblem(family, family.Cat);
        // Generate a new family if it has to not be problematic but is
        if (m_ValidFolders[m_NFolder - 1])
            while (m_Problem.Exists)
            {
                family = FamilyManager.GenerateFamily(familyPicture, familyInfos, _currentCats);
                m_Problem = LogicManager.CheckProblem(family, family.Cat);
            }
        
        // Add constraints until there is a conflict
        FamilyManager.Family tempFamily = family;
        LogicManager.Problem tempProblem = m_Problem;
        while (!tempProblem.Exists)
        {
            tempFamily = FamilyManager.AddConstraint(tempFamily, familyInfos);
            tempProblem = LogicManager.CheckProblem(tempFamily, tempFamily.Cat);
            if (tempProblem.Exists)
            {
                if (!m_ValidFolders[m_NFolder - 1])
                {
                    m_Problem = tempProblem;
                    family = tempFamily;
                }
            }
            else
            {
                family = tempFamily;
                m_Problem = tempProblem;
            }
        }
        
        // Set the cat page on the cat before the one adopted
        if (_index >= _currentCats.Count)
            SetIndex(GetIndexMax());
            
        CatManager.PrintCatInfos(_currentCats[_index]);
        
        // Print the family infos 
        FamilyManager.PrintFamily(family);
    }

    public void OldNextFolder()
    {
        nFolderTMP.text = m_NFolder + " / " + m_NFoldersMax; 
        
        // Get list of cats that are not adopted yet
        _currentCats.Clear();
        CatManager.InitialiseCurrentCats(m_Level, cats, _currentCats);
        _index = 0;

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

    public static void SetIndex(int i)
    {
        _index = i;
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
        if (StatsManager.instance.GetLevel() > 1)
        {
            ProblemsSelector.ResetCircles();
            SetAcceptButtonActive(true);
            SetDeclineButtonActive(false);
        }

        if (m_Problem.Exists)
        {
            StatsManager.instance.AddProblemToList(m_Problem);
            StatsManager.instance.AddBadAdoptions();
        }
        else
        {
            StatsManager.instance.AddGoodAdoptions();
            if(!StatsManager.instance.GetAlbumCats().Contains(m_Problem.Cat.name))
                StatsManager.instance.AddAlbumCat(m_Problem.Cat.name);
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
        if (StatsManager.instance.GetLevel() > 1)
        {
            ProblemsSelector.ResetCircles();
            SetAcceptButtonActive(true);
            SetDeclineButtonActive(false);
        }

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

    public static void SetAcceptButtonActive(bool a)
    {
        _acceptStampButton.interactable = a;
    }
    
    public static void SetDeclineButtonActive(bool a)
    { 
        _declineStampButton.interactable = a;
    }
}
