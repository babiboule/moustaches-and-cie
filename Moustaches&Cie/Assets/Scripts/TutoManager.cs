using System;
using System.Collections;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
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
    private static bool _isPhoneOk;
    
    // Sfx
    [SerializeField] private AudioClip phoneCall;
    [SerializeField] private AudioClip phoneDown;
    
    // Coroutines
    public Coroutine TutoCo;

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
        else Destroy(gameObject);
        DontDestroyOnLoad(_instance);
    }

    /*
     * Call Skip()
     */
    private void SkipButtonClicked()
    {
        UIManager.SkipButton.gameObject.SetActive(false);
        StartCoroutine(Skip());
    }
    

    /*
     * Skip the tuto
     */
    private IEnumerator Skip()
    {
        
        // Wait for the last line to print
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // Stop coroutine and end the tuto
        StopCoroutine(TutoCo);
        switch (StatsManager.instance.GetTutoLvl())
        {
            case 1:
                StartCoroutine(EndTuto1());
                break;
            case 2:
                StartCoroutine(EndTuto2());
                break;
            case 3:
                StartCoroutine(EndTuto3());
                break;
        }
    }

    /*
     * Start a coroutine depending on the Param i number of tutorial
     */
    public void LaunchTuto(int i)
    {
        UIManager.SkipButton.onClick.AddListener(SkipButtonClicked);
        UIManager.FakePhoneButton.onClick.AddListener(PhoneButtonClicked);
        UIManager.SkipButton.gameObject.SetActive(true);
        
        // Launch the good tutorial
        TutoCo = i switch
        {
            1 => StartCoroutine(Tuto1()),
            2 => StartCoroutine(Tuto2()),
            3 => StartCoroutine(Tuto3()),
            _ => TutoCo
        };
    }

    public void StopTuto()
    {
        StopCoroutine(TutoCo);
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
        CatManager.PrintCatInfos(_family.Cat);
        
        UIManager.MemoArrows.SetActive(false);
        UIManager.FamilyPanel.SetActive(false);
        UIManager.CatsPanel.SetActive(false);
        UIManager.MemoPanel.SetActive(false);
        UIManager.StampPanel.SetActive(false);
        

        // Start of the tutorial
        UIManager.ColleaguePanel.SetActive(true);
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[0]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[1]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[2]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[3]));
        while (DialogueController.GetIsWriting())
            yield return null;    
        
        // Desk view
        UIManager.ColleaguePanel.SetActive(false);
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[4]));
        while (DialogueController.GetIsWriting())
            yield return null;
            
        // Print family folder and continue
        UIManager.FamilyPanel.SetActive(true);
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[5]));
        while (DialogueController.GetIsWriting())
            yield return null;
            
        // Print cats folder and continue
        UIManager.CatsPanel.SetActive(true);
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[6]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[7]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        UIManager.MemoPanel.SetActive(true);
            
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[8]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[9]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[10]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        UIManager.MemoArrows.SetActive(true);
        while(!UIManager.MemoPage2.activeSelf)
            yield return null;
            
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[11]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[12]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[13]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        UIManager.StampPanel.SetActive(true);

        // Wait for good stamp
        while (!_declineStamp)
            yield return null;

        StartCoroutine(EndTuto1());
    }

    private IEnumerator Tuto2()
    {
        _family = new FamilyManager.Family()
        {
            Name = "TUTO",
            Forename = "Fabrice",
            Age = 27,
            Budget = 170,
            Car = true,
            Cat = DayManager.GetCurrentCats().Last(),
            Cats = false,
            Child = true,
            Comment = familyInfos.listComments[1],
            FreeTime = 2,
            Home = familyInfos.listHomes[0],
            Outdoor = FamilyInfosScriptableObject.Outdoor.Aucun,
            Guarantor = true,
            JobName = "Testeur"
        };
        _family.Picture = FamilyManager.GenerateFamilyPicture(_family, familyPicture);
        FamilyManager.PrintFamily(_family);
        CatManager.PrintCatInfos(_family.Cat);
        
        // Start of the tutorial
        UIManager.StampPanel.SetActive(false);
        UIManager.ColleaguePanel.SetActive(true);
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[0]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[1]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[2]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[3]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // Desk view
        UIManager.ColleaguePanel.SetActive(false);
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[4]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[5]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[6]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[7]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[8]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // Wait for the good circle
        while (!ProblemsSelector.OutdoorCircle.activeSelf)
            yield return null;
        
        // Continue
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[9]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[10]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[11]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[12]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // Wait for good stamp
        UIManager.StampPanel.SetActive(true);
        while (!_declineStamp)
            yield return null;

        // Different dialog depending on the circle
        UIManager.ColleaguePanel.SetActive(true);
        if (ProblemsSelector.OutdoorCircle.activeSelf)
        {
            StartCoroutine(DialogueController.WriteDialog(tuto2Str[13]));
            while (DialogueController.GetIsWriting())
                yield return null;
        }
        else
        {
            StartCoroutine(DialogueController.WriteDialog(tuto2Str[14]));
            while (DialogueController.GetIsWriting())
                yield return null;
        }

        StartCoroutine(EndTuto2());
    }
    
    private IEnumerator Tuto3()
    {
        _family = new FamilyManager.Family()
        {
            Name = "TUTO",
            Forename = "Riley",
            Age = 43,
            Budget = 120,
            Car = true,
            Cat = DayManager.GetCurrentCats().Last(),
            Cats = false,
            Child = false,
            Comment = familyInfos.listComments[2],
            FreeTime = 1,
            Home = familyInfos.listHomes[0],
            Outdoor = FamilyInfosScriptableObject.Outdoor.Fermé,
            Guarantor = true,
            JobName = "Testeur"
        };
        _family.Picture = FamilyManager.GenerateFamilyPicture(_family, familyPicture);
        FamilyManager.PrintFamily(_family);
        CatManager.PrintCatInfos(_family.Cat);
        
        // Start of the tutorial
        UIManager.StampPanel.SetActive(false);
        UIManager.PhonePanel.SetActive(true);
        UIManager.FakePhoneButton.gameObject.SetActive(false);
        UIManager.PhoneButton.interactable = false;
        UIManager.ColleaguePanel.SetActive(true);
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[0]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[1]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[2]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // Enumeration of new directives 
        UIManager.ColleaguePanel.SetActive(false);
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[3]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[4]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[5]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[6]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // Explains phone
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[7]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[8]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[9]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[10]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[11]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[12]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[13]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[14]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // Wait for good call
        while (!_isPhoneOk)
        {
            UIManager.FakePhoneButton.gameObject.SetActive(UIManager.PhoneButton.gameObject.activeSelf);
            yield return null;
        }
        UIManager.FakePhoneButton.gameObject.SetActive(false);
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // UI & Sfx
        UIManager.PhoneOff.SetActive(true);
        UIManager.PhoneOn.SetActive(false);
        SfxManager.instance.PlaySfxClip(phoneDown);
        yield return new WaitForSeconds(2);
        
        // Continue
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[15]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[16]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // Wait for good stamp
        UIManager.StampPanel.SetActive(true);
        while (!_declineStamp)
            yield return null;

        StartCoroutine(EndTuto3());
    }

    private IEnumerator EndTuto1()
    {
        UIManager.ColleaguePanel.SetActive(true);
        StartCoroutine(DialogueController.WriteDialog(tuto1Str[new Range(14,16)]));
        while (DialogueController.GetIsWriting())
            yield return null;
            
        // End of the tutorial
        UIManager.MemoArrows.SetActive(true);
        UIManager.FamilyPanel.SetActive(true);
        UIManager.CatsPanel.SetActive(true);
        UIManager.MemoPanel.SetActive(true);
        UIManager.StampPanel.SetActive(true);
        UIManager.SkipButton.gameObject.SetActive(false);
        UIManager.ColleaguePanel.SetActive(false);
        StatsManager.instance.SetTuto(false);
    }
    
    private IEnumerator EndTuto2()
    {
        UIManager.ColleaguePanel.SetActive(true);
        StartCoroutine(DialogueController.WriteDialog(tuto2Str[new Range(15,17)]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // End of the tutorial
        DayManager.SetAcceptButtonActive(true);
        DayManager.SetDeclineButtonActive(false);
        ProblemsSelector.ResetCircles();
        UIManager.StampPanel.SetActive(true);
        UIManager.SkipButton.gameObject.SetActive(false);
        UIManager.ColleaguePanel.SetActive(false);
        StatsManager.instance.SetTuto(false);
    }
    
    private IEnumerator EndTuto3()
    {
        UIManager.ColleaguePanel.SetActive(true);
        StartCoroutine(DialogueController.WriteDialog(tuto3Str[new Range(17,19)]));
        while (DialogueController.GetIsWriting())
            yield return null;
        
        // End of the tutorial
        DayManager.SetAcceptButtonActive(true);
        DayManager.SetDeclineButtonActive(false);
        ProblemsSelector.ResetCircles();
        UIManager.FakePhoneButton.gameObject.SetActive(false);
        UIManager.PhoneButton.gameObject.SetActive(false);
        UIManager.PhoneButton.interactable = true;
        UIManager.StampPanel.SetActive(true);
        UIManager.SkipButton.gameObject.SetActive(false);
        UIManager.ColleaguePanel.SetActive(false);
        StatsManager.instance.SetTuto(false);
    }

    private void PhoneButtonClicked()
    {
        StartCoroutine(PhoneCall());
    }

    private IEnumerator PhoneCall()
    {
        var s1 = "Allô ?";
        var s2 = "[...]";
        var s3 = "Non, je ne peux pas télétravailler...";
        var str = new []{s1, s2, s3};

        if (ProblemsSelector.JobCircle.activeSelf)
        {
            // UI & Sfx
            UIManager.PhoneOff.SetActive(false);
            UIManager.PhoneOn.SetActive(true);
            UIManager.FakePhoneButton.gameObject.SetActive(false);
            SfxManager.instance.PlaySfxClip(phoneCall);
            yield return new WaitForSeconds(3);
            
            StartCoroutine(DialogueController.WriteDialog(str));
            _isPhoneOk = true;
        }
        else
        {
            StartCoroutine(DialogueController.WriteDialog("Ce n'est pas pour ça qu'on appelle..."));
        }
    }

    public static IEnumerator SetDeclineStampClicked()
    {
        _declineStamp = true;
        yield return new WaitForSeconds(1);
        _declineStamp = false;
    }
}