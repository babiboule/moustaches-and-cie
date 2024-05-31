using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProblemsSelector : MonoBehaviour
{
    // Buttons
    [SerializeField] private Button ageButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button jobButton;
    [SerializeField] private Button incomeButton;
    [SerializeField] private Button childButton;
    [SerializeField] private Button outdoorButton;
    [SerializeField] private Button animalsButton;
    [SerializeField] private Button commentButton;
    
    // Images
    [SerializeField] private GameObject ageCircle;
    [SerializeField] private GameObject homeCircle;
    [SerializeField] private GameObject jobCircle;
    [SerializeField] private GameObject incomeCircle;
    [SerializeField] private GameObject childCircle;
    [SerializeField] private GameObject outdoorCircle;
    [SerializeField] private GameObject animalsCircle;
    [SerializeField] private GameObject commentCircle;
    
    private static GameObject _ageCircle;
    private static GameObject _homeCircle;
    public static GameObject JobCircle;
    private static GameObject _incomeCircle;
    private static GameObject _childCircle;
    public static GameObject OutdoorCircle;
    private static GameObject _animalsCircle;
    private static GameObject _commentCircle;
    
    // Dialog
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text dialogTMP;
    
    // Sfx
    [SerializeField] private AudioClip circleSfx;

    private void Awake()
    {
        // Add listeners on buttons
        ageButton.onClick.AddListener(AgeButtonClicked);
        homeButton.onClick.AddListener(HomeButtonClicked);
        jobButton.onClick.AddListener(JobButtonClicked);
        incomeButton.onClick.AddListener(IncomeButtonClicked);
        childButton.onClick.AddListener(ChildButtonClicked);
        outdoorButton.onClick.AddListener(OutdoorButtonClicked);
        animalsButton.onClick.AddListener(AnimalsButtonClicked);
        commentButton.onClick.AddListener(CommentButtonClicked);
        UIManager.PhoneButton.onClick.AddListener(PhoneButtonClicked);
        
        // Assign correspondences
        _ageCircle = ageCircle;
        _homeCircle = homeCircle;
        JobCircle = jobCircle;
        _incomeCircle = incomeCircle;
        _childCircle = childCircle;
        OutdoorCircle = outdoorCircle;
        _animalsCircle = animalsCircle;
        _commentCircle = commentCircle;
        
        // Set the circle interaction ON if on level 2 or more
        if (StatsManager.instance.GetLevel() > 1)
        {
            ageButton.interactable = true;
            homeButton.interactable = true;
            jobButton.interactable = true;
            incomeButton.interactable = true;
            childButton.interactable = true;
            outdoorButton.interactable = true;
            animalsButton.interactable = true;
            commentButton.interactable = true;
        }
        else
        {
            ageButton.interactable = false;
            homeButton.interactable = false;
            jobButton.interactable = false;
            incomeButton.interactable = false;
            childButton.interactable = false;
            outdoorButton.interactable = false;
            animalsButton.interactable = false;
            commentButton.interactable = false;
        }
        
        // Set the phone button ON if on level 3 or more
        if(StatsManager.instance.GetLevel() > 2 && !StatsManager.instance.GetTuto())
        {
            UIManager.PhonePanel.gameObject.SetActive(true);
            UIManager.PhoneButton.gameObject.SetActive(false);
            UIManager.PhoneOff.SetActive(true);
            UIManager.PhoneOn.SetActive(false);
        }
    }

    /*
     * Disable all the circles on the screen
     */
    public static void ResetCircles()
    {
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        JobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        OutdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        if(StatsManager.instance.GetLevel() > 2)
        {
            UIManager.PhonePanel.gameObject.SetActive(true);
            UIManager.PhoneButton.gameObject.SetActive(false);
            UIManager.PhoneOff.SetActive(true);
            UIManager.PhoneOn.SetActive(false);
        }
    }

    /*
     * Check if the selected information is the reason of the Param problem and return true if it is
     */
    public static bool CheckProblemSelected(LogicManager.Problem problem)
    {
        // If there is no problem, this method shouldn't be called (so it's a bad answer)
        if (!problem.Exists)
            return false;
        return problem.PbFamily switch
        {
            LogicManager.PbFamily.TooOld => _ageCircle.activeSelf,
            LogicManager.PbFamily.TooYoung => _ageCircle.activeSelf,
            LogicManager.PbFamily.TooPoor => _incomeCircle.activeSelf,
            LogicManager.PbFamily.TooBusy => JobCircle.activeSelf,
            LogicManager.PbFamily.Child => _childCircle.activeSelf,
            LogicManager.PbFamily.NoOutdoor => OutdoorCircle.activeSelf,
            LogicManager.PbFamily.Animals => _animalsCircle.activeSelf,
            LogicManager.PbFamily.Comment => _commentCircle.activeSelf,
            LogicManager.PbFamily.NoCar => _homeCircle.activeSelf,
            LogicManager.PbFamily.TooFar => _homeCircle.activeSelf,
            _ => false
        };
    }

    private void PhoneButtonClicked()
    {
        StartCoroutine(Call());
    }

    private IEnumerator Call()
    {
        // UI
        UIManager.PhoneOff.SetActive(false);
        UIManager.PhoneOn.SetActive(true);
        UIManager.PhoneButton.gameObject.SetActive(false);
        
        var family = DayManager.GetCurrentFamily();
        
        if (_ageCircle.activeSelf)
        {
            if (family.Age < 70)
            {
                StartCoroutine(DialogueController.WriteDialog("Pourquoi j'aurais besoin d'un garant ?..."));
                
                while (DialogueController.GetIsWriting())
                    yield return null;
                UIManager.PhoneOff.SetActive(true);
                UIManager.PhoneOn.SetActive(false);
                UIManager.PhoneButton.gameObject.SetActive(true);
            }
            else
            {
                StartCoroutine(family.Guarantor
                    ? DialogueController.WriteDialog("Oui, je possède un garant.")
                    : DialogueController.WriteDialog("Non, je n'ai pas de garant, pourquoi ?"));
               
                while (DialogueController.GetIsWriting())
                    yield return null;
                UIManager.PhoneOff.SetActive(true);
                UIManager.PhoneOn.SetActive(false);
                UIManager.PhoneButton.gameObject.SetActive(true);
            }
        }

        if (_homeCircle.activeSelf)
        {
            StartCoroutine(family.Car
                ? DialogueController.WriteDialog("Oui, je possède une voiture.")
                : DialogueController.WriteDialog("Non je n'ai pas de voiture..."));
            
            while (DialogueController.GetIsWriting())
                yield return null;
            UIManager.PhoneOff.SetActive(true);
            UIManager.PhoneOn.SetActive(false);
            UIManager.PhoneButton.gameObject.SetActive(true);
        }

        if (JobCircle.activeSelf)
        {
            switch (family.FreeTime)
            {
                case 1:
                    StartCoroutine(DialogueController.WriteDialog("Non je n'ai pas la possibilité d'exercer mon métier en télétravail..."));
                    while (DialogueController.GetIsWriting())
                        yield return null;
                    UIManager.PhoneOff.SetActive(true);
                    UIManager.PhoneOn.SetActive(false);
                    UIManager.PhoneButton.gameObject.SetActive(true);
                    break;
                case 2:
                    StartCoroutine(DialogueController.WriteDialog("Oui, je peux facilement travailler depuis chez moi !"));
                    while (DialogueController.GetIsWriting())
                        yield return null;
                    UIManager.PhoneOff.SetActive(true);
                    UIManager.PhoneOn.SetActive(false);
                    UIManager.PhoneButton.gameObject.SetActive(true);
                    break;
                case 3:
                    StartCoroutine(DialogueController.WriteDialog("Euuuh... C'est une blague ?"));
                    while (DialogueController.GetIsWriting())
                        yield return null;
                    UIManager.PhoneOff.SetActive(true);
                    UIManager.PhoneOn.SetActive(false);
                    UIManager.PhoneButton.gameObject.SetActive(true);
                    break;
            }
        }
    }



    /****************** Select/Deselect the information and deselect all the others ******************/
    private void AgeButtonClicked()
    {
        // Phone button
        UIManager.PhoneButton.gameObject.SetActive(!_ageCircle.activeSelf);

        // Circles
        _ageCircle.SetActive(!_ageCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_ageCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_ageCircle.activeSelf);
        
        _homeCircle.SetActive(false);
        JobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        OutdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }
    
    private void HomeButtonClicked()
    {
        // Phone button
        UIManager.PhoneButton.gameObject.SetActive(!_homeCircle.activeSelf);
        
        // Circles
        _ageCircle.SetActive(false);
        
        _homeCircle.SetActive(!_homeCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_homeCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_homeCircle.activeSelf);
        
        JobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        OutdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void JobButtonClicked()
    {
        // Phone button
        UIManager.PhoneButton.gameObject.SetActive(!JobCircle.activeSelf);
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        
        JobCircle.SetActive(!JobCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!JobCircle.activeSelf);
        DayManager.SetDeclineButtonActive(JobCircle.activeSelf);
        
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        OutdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void IncomeButtonClicked()
    {
        // Phone button
        UIManager.PhoneButton.gameObject.SetActive(false);
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        JobCircle.SetActive(false);
        
        _incomeCircle.SetActive(!_incomeCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_incomeCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_incomeCircle.activeSelf);
        
        _childCircle.SetActive(false);
        OutdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void ChildButtonClicked()
    {
        // Phone button
        UIManager.PhoneButton.gameObject.SetActive(false);
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        JobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        
        _childCircle.SetActive(!_childCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_childCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_childCircle.activeSelf);
        
        OutdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void OutdoorButtonClicked()
    {
        // Phone button
        UIManager.PhoneButton.gameObject.SetActive(false);
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        JobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        
        OutdoorCircle.SetActive(!OutdoorCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!OutdoorCircle.activeSelf);
        DayManager.SetDeclineButtonActive(OutdoorCircle.activeSelf);
        
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void AnimalsButtonClicked()
    {
        // Phone button
        UIManager.PhoneButton.gameObject.SetActive(false);
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        JobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        OutdoorCircle.SetActive(false);
        
        _animalsCircle.SetActive(!_animalsCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_animalsCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_animalsCircle.activeSelf);
        
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void CommentButtonClicked()
    {
        // Phone button
        UIManager.PhoneButton.gameObject.SetActive(false);
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        JobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        OutdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
       
        _commentCircle.SetActive(!_commentCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_commentCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_commentCircle.activeSelf);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }
    
}