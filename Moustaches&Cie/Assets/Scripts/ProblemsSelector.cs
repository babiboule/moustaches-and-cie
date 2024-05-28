using System.Collections;
using TMPro;
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
    [SerializeField] private Button phoneButton;
    
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
    private static GameObject _jobCircle;
    private static GameObject _incomeCircle;
    private static GameObject _childCircle;
    private static GameObject _outdoorCircle;
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
        phoneButton.onClick.AddListener(PhoneButtonClicked);
        
        // Assign correspondences
        _ageCircle = ageCircle;
        _homeCircle = homeCircle;
        _jobCircle = jobCircle;
        _incomeCircle = incomeCircle;
        _childCircle = childCircle;
        _outdoorCircle = outdoorCircle;
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
        phoneButton.gameObject.SetActive(StatsManager.instance.GetLevel() > 2);
        phoneButton.interactable = false;
    }

    /*
     * Disable all the circles on the screen
     */
    public static void ResetCircles()
    {
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
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
            LogicManager.PbFamily.TooBusy => _jobCircle.activeSelf,
            LogicManager.PbFamily.Child => _childCircle.activeSelf,
            LogicManager.PbFamily.NoOutdoor => _outdoorCircle.activeSelf,
            LogicManager.PbFamily.Animals => _animalsCircle.activeSelf,
            LogicManager.PbFamily.Comment => _commentCircle.activeSelf,
            LogicManager.PbFamily.NoCar => _homeCircle.activeSelf,
            LogicManager.PbFamily.TooFar => _homeCircle.activeSelf,
            _ => false
        };
    }

    private void PhoneButtonClicked()
    {
        var family = DayManager.GetCurrentFamily();
        
        if (_ageCircle.activeSelf)
        {
            if (family.Age < 70)
            {
                StartCoroutine(DialogueController.WriteDialog("Pourquoi j'aurais besoin d'un garant ?..."));
            }
            else
            {
                StartCoroutine(family.Guarantor
                    ? DialogueController.WriteDialog("Oui, je possède un garant.")
                    : DialogueController.WriteDialog("Non, je n'ai pas de garant, pourquoi ?"));
            }
        }

        if (_homeCircle.activeSelf)
        {
            StartCoroutine(family.Car
                ? DialogueController.WriteDialog("Oui, je possède une voiture.")
                : DialogueController.WriteDialog("Non je n'ai pas de voiture..."));
        }

        if (_jobCircle.activeSelf)
        {
            switch (family.FreeTime)
            {
                case 1:
                    StartCoroutine(DialogueController.WriteDialog("Non je n'ai pas la possibilité d'exercer mon métier en télétravail..."));
                    break;
                case 2:
                    StartCoroutine(DialogueController.WriteDialog("Oui, je peux facilement travailler depuis chez moi !"));
                    break;
                case 3:
                    StartCoroutine(DialogueController.WriteDialog("Euuuh... C'est une blague ?"));
                    break;
            }
        }
        
    }



    /****************** Select/Deselect the information and deselect all the others ******************/
    private void AgeButtonClicked()
    {
        // Phone button
        phoneButton.interactable = !_ageCircle.activeSelf && StatsManager.instance.GetLevel()>2;

        // Circles
        _ageCircle.SetActive(!_ageCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_ageCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_ageCircle.activeSelf);
        
        _homeCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }
    
    private void HomeButtonClicked()
    {
        // Phone button
        phoneButton.interactable = !_homeCircle.activeSelf && StatsManager.instance.GetLevel()>2;
        
        // Circles
        _ageCircle.SetActive(false);
        
        _homeCircle.SetActive(!_homeCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_homeCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_homeCircle.activeSelf);
        
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void JobButtonClicked()
    {
        // Phone button
        phoneButton.interactable = !_jobCircle.activeSelf && StatsManager.instance.GetLevel()>2;
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        
        _jobCircle.SetActive(!_jobCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_jobCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_jobCircle.activeSelf);
        
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void IncomeButtonClicked()
    {
        // Phone button
        phoneButton.interactable = false;
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        _jobCircle.SetActive(false);
        
        _incomeCircle.SetActive(!_incomeCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_incomeCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_incomeCircle.activeSelf);
        
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void ChildButtonClicked()
    {
        // Phone button
        phoneButton.interactable = false;
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        
        _childCircle.SetActive(!_childCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_childCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_childCircle.activeSelf);
        
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void OutdoorButtonClicked()
    {
        // Phone button
        phoneButton.interactable = false;
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        
        _outdoorCircle.SetActive(!_outdoorCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_outdoorCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_outdoorCircle.activeSelf);
        
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void AnimalsButtonClicked()
    {
        // Phone button
        phoneButton.interactable = false;
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        
        _animalsCircle.SetActive(!_animalsCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_animalsCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_animalsCircle.activeSelf);
        
        _commentCircle.SetActive(false);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }

    private void CommentButtonClicked()
    {
        // Phone button
        phoneButton.interactable = false;
        
        // Circles
        _ageCircle.SetActive(false);
        _homeCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
       
        _commentCircle.SetActive(!_commentCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_commentCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_commentCircle.activeSelf);
        
        SfxManager.instance.PlaySfxClip(circleSfx);
    }
    
}