using System;
using System.Collections;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class TutoManager : MonoBehaviour
{
    // UI
    // Panel
    [SerializeField] private GameObject colleaguePanel;
    [SerializeField] private GameObject catsArrows;
    [SerializeField] private GameObject memoArrows;
    [SerializeField] private GameObject memoPage2;
    [SerializeField] private GameObject familyPanel;
    [SerializeField] private GameObject catsPanel;
    [SerializeField] private GameObject memoPanel;
    [SerializeField] private GameObject postItPanel;
    [SerializeField] private GameObject stampPanel;

    // Variables
    [SerializeField] private FamilyPictureScriptableObject familyPicture;
    [SerializeField] private FamilyInfosScriptableObject familyInfos;
    [SerializeField] private CatsScriptableObject cats;

    [SerializeField] private string[] tuto1Str;
    [SerializeField] private string[] tuto2Str;
    [SerializeField] private string[] tuto3Str;
    private FamilyManager.Family _family;
    private int _index;
    private static bool _declineStamp;
    

    // Singleton
    private static TutoManager _instance;
    public static TutoManager instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Tuto Manager is NULL !");
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

    /*
     * Call Skip()
     */
    private void SkipButtonClicked()
    {
        StartCoroutine(Skip());
    }
    

    /*
     * Skip the tuto
     */
    private IEnumerator Skip()
    {
        while (DialogueController.GetIsWriting())
            yield return null;
    }

    /*
     * Launch the game
     */
    private void YesButtonClicked()
    {
        DialogueController.SetWaitInput(false);
    }

    /*
     * Restart the tuto
     */
    private void NoButtonClicked()
    {
        DialogueController.SetWaitInput(false);
    }

    /*
     * Start a coroutine depending on the Param i number of tutorial
     */
    public void LaunchTuto(int i)
    {
        Debug.Log("Je lance le tuto n°" + i);
        
        // Reset the bool and launch the good tutorial
        switch (i)
        {
            case 1:
                StartCoroutine(Tuto1());
                break;
            case 2:
                StartCoroutine(Tuto2());
                break;
            case 3:
                StartCoroutine(Tuto3());
                break;
        }
    }

    private IEnumerator Tuto1()
    {
        _family = new FamilyManager.Family()
        {
            Name = "TUTO",
            Forename = "Titouan",
            Age = 32,
            Budget = 150,
            Car = true,
            Cat = cats.cats[3],
            Cats = true,
            Child = false,
            Comment = familyInfos.listComments[0],
            FreeTime = 2,
            Home = familyInfos.listHomes[0],
            Outdoor = FamilyInfosScriptableObject.Outdoor.Fermé,
            Guarantor = true,
            JobName = "Testeur"
        };
        _family.Picture = FamilyManager.GenerateFamilyPicture(_family, familyPicture);
        FamilyManager.PrintFamily(_family);

        
        catsArrows.SetActive(false);
        memoArrows.SetActive(false);
        familyPanel.SetActive(false);
        catsPanel.SetActive(false);
        memoPanel.SetActive(false);
        postItPanel.SetActive(false);
        stampPanel.SetActive(false);
        
        while (StatsManager.instance.GetTuto())
        {
            // Start of the tutorial
            colleaguePanel.SetActive(true);
            StartCoroutine(DialogueController.WriteDialog(tuto1Str[new Range(0,4)]));
            while (DialogueController.GetIsWriting())
            {
                yield return null;
            }
            
            // Desk view
            colleaguePanel.SetActive(false);
            StartCoroutine(DialogueController.WriteDialog(tuto1Str[4]));
            while (DialogueController.GetIsWriting())
                yield return null;
            familyPanel.SetActive(true);
            
            StartCoroutine(DialogueController.WriteDialog(tuto1Str[5]));
            while (DialogueController.GetIsWriting())
                yield return null;
            catsPanel.SetActive(true);
            
            StartCoroutine(DialogueController.WriteDialog(tuto1Str[new Range(6,9)]));
            while (DialogueController.GetIsWriting())
                yield return null;
            catsArrows.SetActive(true);
            
            // Wait for good cat to be printed
            while (DayManager.GetCurrentCats()[DayManager.GetIndex()].name != _family.Cat.name)
            {
                yield return null;
            }
            
            // Continue 
            StartCoroutine(DialogueController.WriteDialog(tuto1Str[new Range(9,11)]));
            postItPanel.SetActive(true);
            while (DialogueController.GetIsWriting())
            {
                yield return null;
            }
            memoPanel.SetActive(true);
            
            StartCoroutine(DialogueController.WriteDialog(tuto1Str[new Range(11,14)]));
            while (DialogueController.GetIsWriting())
            {
                yield return null;
            }
            memoArrows.SetActive(true);
            while(!memoPage2.activeSelf)
            {
                yield return null;
            }
            
            StartCoroutine(DialogueController.WriteDialog(tuto1Str[new Range(14,17)]));
            while (DialogueController.GetIsWriting())
            {
                yield return null;
            }
            stampPanel.SetActive(true);

            // Wait for good stamp
            while (!_declineStamp)
            {
                yield return null;
            }
            
            // Continue
            colleaguePanel.SetActive(true);
            StartCoroutine(DialogueController.WriteDialog(tuto1Str[new Range(17,19)]));
            while (DialogueController.GetIsWriting())
            {
                yield return null;
            }
            
            // End of the tutorial
            colleaguePanel.SetActive(false);
            StatsManager.instance.SetTuto(false);
        
            yield return null;
        }
    }

    private IEnumerator Tuto2()
    {
        yield return null;
    }
    
    private IEnumerator Tuto3()
    {
        yield return null;
    }

    public static IEnumerator SetDeclineStampClicked()
    {
        _declineStamp = true;
        yield return new WaitForSeconds(1);
        _declineStamp = false;
    }
    
}