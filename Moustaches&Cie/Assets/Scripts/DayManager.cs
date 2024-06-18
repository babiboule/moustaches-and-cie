using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    // Scriptable Objects
    [SerializeField] private FamilyPictureScriptableObject familyPicture;
    [SerializeField] private FamilyInfosScriptableObject familyInfos;
    [SerializeField] private CatsScriptableObject cats;

    // UI
    [SerializeField] private TMP_Text nFolderTMP;
    [SerializeField] private TMP_Text dayTMP;
    [SerializeField] private Button acceptStampButton;
    [SerializeField] private Button declineStampButton;
    private static Button _acceptStampButton;
    private static Button _declineStampButton;
    
    // Lists
    private static readonly List<CatsScriptableObject.Cat> CurrentCats = new();
    private List<bool> _validFolders = new();
    
    // Private variables
    private int _level;
    private int _nFoldersMax;
    private int _nFolder;
    private LogicManager.Problem _problem;
    private static FamilyManager.Family _family;
    
    // Sfx
    [SerializeField] private AudioClip stampSfx;
    [SerializeField] private AudioClip goodSfx;
    [SerializeField] private AudioClip badSfx;
    
    // Coroutine
    private Coroutine _coTuto;

    private void Awake()
    {
        // Assign listeners to buttons
        acceptStampButton.onClick.AddListener(AcceptStampButtonClicked);
        declineStampButton.onClick.AddListener(DeclineStampButtonClicked);

        // Assign the correspondences
        _acceptStampButton = acceptStampButton;
        _declineStampButton = declineStampButton;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Reset Daily stats
        StatsManager.instance.ResetDailyStats();
        
        // Set the level and the day
        _level = StatsManager.instance.GetLevel();
        dayTMP.text = StatsManager.instance.GetDate() + "/" + StatsManager.instance.dayMax;

        // Set the number of folders depending on the level
        switch (_level)
        {
            case 1:
                _nFoldersMax = 5;
                break;
            case 2:
                _nFoldersMax = 6;
                break;
            case 3:
                _nFoldersMax = 7;
                break;
            case 4:
                _nFoldersMax = 7;
                break;
            default:
                Debug.Log("NOT ON A LEVEL");
                break;
        }
        
        // Get list of cats that are not adopted yet
        CurrentCats.Clear();
        CatManager.InitialiseCurrentCats(_level, cats, CurrentCats);
        if (CurrentCats.Count <= _nFoldersMax)
        {
            _nFoldersMax = CurrentCats.Count - 1;
        }
        
        // Set the variables
        _validFolders = SetValidFolders();
        _nFolder = 1 ;
        
        // Set the first cat page
        CatManager.PrintCatInfos(CurrentCats[0]);
        
        // Disable the decline stamp for level 2 or more
        if(StatsManager.instance.GetLevel()>1)
            SetDeclineButtonActive(false);
        
        // Launch the tuto if needed or print the first folder of the day
        if(StatsManager.instance.GetTuto() || StatsManager.instance.GetDate() == 1)
        {
            _coTuto = StartCoroutine(Tutorial());
        }
        else
        {
            StartCoroutine(NextFolder());
        }
    }

    private IEnumerator Tutorial()
    {
        TutoManager.instance.LaunchTuto(StatsManager.instance.GetTutoLvl());
        while(StatsManager.instance.GetTuto())
            yield return null;
        StopCoroutine(_coTuto);
        StartCoroutine(NextFolder());
    }

    /*
     * Return the list of the valid folders randomly shuffled
     * Out : list of booleans (false = invalid folder)
     */
    private List<bool> SetValidFolders()
    {
        // Define the value of valid folders for the day
        var tempList = new List<bool>();
        var list = new List<bool>();
        var nbValid = Random.Range((int)(_nFoldersMax / 2.0f), (int)(_nFoldersMax / 2.0f)+2);
        
        // Set a temp bool list with x false and n-x true
        for (var i = 0; i < _nFoldersMax-nbValid; i++)
            tempList.Add(false);
        for (var i = 0; i < nbValid; i++)
            tempList.Add(true);
        
        // Shuffle the list
        while (tempList.Count > 0)
        {
            var rand = Random.Range(0, tempList.Count);
            var temp = tempList[rand];
            list.Add(temp);
            tempList.Remove(temp);
        }
        
        return list;
    }

    /*
     * Generate a clean family which would accept any cat and add modifiers one by one until there is a problem
     * if the folder is a valid one, it will take the iteration n-1, else it will take the iteration n
     */
    private IEnumerator NextFolder()
    {
        // Update n° folder text
        nFolderTMP.text = "Dossier n°" + _nFolder + " / " + _nFoldersMax; 
        
        // Get list of cats that are not adopted yet
        CurrentCats.Clear();
        CatManager.InitialiseCurrentCats(_level, cats, CurrentCats);
        
        // Generate Family
        _family = FamilyManager.GenerateFamily(familyPicture, familyInfos, CurrentCats);
        
        // Check the validity of the first generated family
        _problem = LogicManager.CheckProblem(_family, _family.Cat);
        // Generate a new family if it has to not be problematic but is
        if (_validFolders[_nFolder - 1])
            while (_problem.Exists)
            {
                _family = FamilyManager.GenerateFamily(familyPicture, familyInfos, CurrentCats);
                _problem = LogicManager.CheckProblem(_family, _family.Cat);
                yield return null;
            }
        
        // Add constraints until there is a conflict
        var tempFamily = _family;
        var tempProblem = _problem;
        while (!tempProblem.Exists)
        {
            yield return null;
            tempFamily = FamilyManager.AddConstraint(tempFamily, familyInfos);
            tempProblem = LogicManager.CheckProblem(tempFamily, tempFamily.Cat);
            if (tempProblem.Exists)
            {
                if (!_validFolders[_nFolder - 1])
                {
                    _problem = tempProblem;
                    _family = tempFamily;
                }
            }
            else
            {
                _family = tempFamily;
                _problem = tempProblem;
            }
        }
        
        CatManager.PrintCatInfos(_family.Cat);
        
        // Print the family infos 
        FamilyManager.PrintFamily(_family);
        
        /******** DEBUG ********/
        Debug.Log(_validFolders[_nFolder-1]);
        if(StatsManager.instance.GetLevel()>1 && _problem.Exists)
            Debug.Log(_problem.PbFamily);
    }
    
    /*
     * Return the list of the current cats available
     */
    public static List<CatsScriptableObject.Cat> GetCurrentCats()
    {
        return CurrentCats;
    }
    
    /*
     * Return the cat index max
     */
    public static int GetIndexMax()
    {
        return CurrentCats.Count - 1;
    }
    
    /*
     * Change stats depending on the validity of the folder and go to the next folder
     */
    private void AcceptStampButtonClicked()
    {
        if (StatsManager.instance.GetTuto())
        {
            switch (StatsManager.instance.GetTutoLvl())
            {
                case 1:
                    StartCoroutine(DialogueController.WriteDialog("Non, celui-là c'est pour valider le dossier..."));
                    return;
                case 2:
                    StartCoroutine(DialogueController.WriteDialog("Tu dois refuser le dossier, pas le valider..."));
                    return;
                case 3:
                    StartCoroutine(DialogueController.WriteDialog("Tu dois refuser le dossier, pas le valider..."));
                    return;
            }
        }
        
        // Sfx
        SfxManager.instance.PlaySfxClip(stampSfx);
        
        // Reset circles and stamps if on level 2 or more
        if (StatsManager.instance.GetLevel() > 1)
        {
            ProblemsSelector.ResetCircles();
            SetAcceptButtonActive(true);
            SetDeclineButtonActive(false);
        }

        // If folder accepted but there is a problem, it's a bad answer
        if (_problem.Exists)
        {
            StatsManager.instance.AddProblemToList(_problem);
            StatsManager.instance.AddBadAdoptions();
            SfxManager.instance.PlaySfxClip(badSfx);
        }
        // Else it's a good one
        else
        {
           
            StatsManager.instance.AddGoodAdoptions();
            if(!StatsManager.instance.GetAlbumCats().Contains(_problem.Cat.name))
            {
                StatsManager.instance.AddAlbumCat(_problem.Cat.name);
                StatsManager.instance.newAdoptions ++;
            }
            SfxManager.instance.PlaySfxClip(goodSfx);
        }
        
        // The cat is added to the adopted cats to not see it again in the next folders
        StatsManager.instance.AddAdoptedCat(_problem.Cat.name);

        // Next folder if not the end of the day
        if (_nFolder < _nFoldersMax  && CurrentCats.Count > 1)
        {
            _nFolder++;
            StartCoroutine(NextFolder());
        }
        else
        {
            GameManager.instance.UpdateGameLevel(GameManager.GameLevel.ScoreLevel);
        }
    }

    /*
     * Change stats depending on the validity of the folder and go to the next folder
     */
    private void DeclineStampButtonClicked()
    {
        if (StatsManager.instance.GetTuto())
        {
            StartCoroutine(TutoManager.SetDeclineStampClicked());
            return;
        }
        
        // Sfx
        SfxManager.instance.PlaySfxClip(stampSfx);
        
        // If decline folder but it is valid, bad answer
        if (!_problem.Exists)
        {
            StatsManager.instance.AddBadDecline();
            SfxManager.instance.PlaySfxClip(badSfx);
        }
        else
        {
            if (StatsManager.instance.GetLevel() > 1)
            {
                if(!ProblemsSelector.CheckProblemSelected(_problem))
                {
                    StatsManager.instance.AddBadDecline();
                    SfxManager.instance.PlaySfxClip(badSfx);
                }
                else
                {
                    StatsManager.instance.AddGoodDecline();
                    SfxManager.instance.PlaySfxClip(goodSfx);
                }
            }
            else
            {
                SfxManager.instance.PlaySfxClip(goodSfx);
            }
            
        }
        
        // Reset circles and stamps if on level 2 or more
        if (StatsManager.instance.GetLevel() > 1)
        {
            ProblemsSelector.ResetCircles();
            SetAcceptButtonActive(true);
            SetDeclineButtonActive(false);
        }
        
        // Next folder if not the end of the day
        if (_nFolder < _nFoldersMax)
        {
            _nFolder++;
            StartCoroutine(NextFolder());
        }
        else
        {
            GameManager.instance.UpdateGameLevel(GameManager.GameLevel.ScoreLevel);
        }
    }

    /*
     * Set the accept button to Param a
     */
    public static void SetAcceptButtonActive(bool a)
    {
        _acceptStampButton.interactable = a;
    }
    
    /*
     * Set the decline button to Param a
     */
    public static void SetDeclineButtonActive(bool a)
    {
        _declineStampButton.interactable = a;
    }

    /*
     * Return the current family at the screen
     */
    public static FamilyManager.Family GetCurrentFamily()
    {
        return _family;
    }
}