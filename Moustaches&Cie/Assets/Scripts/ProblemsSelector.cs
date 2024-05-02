using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemsSelector : MonoBehaviour
{
    // Buttons
    [SerializeField] private Button ageButton;
    [SerializeField] private Button jobButton;
    [SerializeField] private Button incomeButton;
    [SerializeField] private Button childButton;
    [SerializeField] private Button outdoorButton;
    [SerializeField] private Button animalsButton;
    [SerializeField] private Button commentButton;
    
    // Images
    [SerializeField] private GameObject ageCircle;
    [SerializeField] private GameObject jobCircle;
    [SerializeField] private GameObject incomeCircle;
    [SerializeField] private GameObject childCircle;
    [SerializeField] private GameObject outdoorCircle;
    [SerializeField] private GameObject animalsCircle;
    [SerializeField] private GameObject commentCircle;
    
    private static GameObject _ageCircle;
    private static GameObject _jobCircle;
    private static GameObject _incomeCircle;
    private static GameObject _childCircle;
    private static GameObject _outdoorCircle;
    private static GameObject _animalsCircle;
    private static GameObject _commentCircle;

    private void Awake()
    {
        ageButton.onClick.AddListener(AgeButtonClicked);
        jobButton.onClick.AddListener(JobButtonClicked);
        incomeButton.onClick.AddListener(IncomeButtonClicked);
        childButton.onClick.AddListener(ChildButtonClicked);
        outdoorButton.onClick.AddListener(OutdoorButtonClicked);
        animalsButton.onClick.AddListener(AnimalsButtonClicked);
        commentButton.onClick.AddListener(CommentButtonClicked);
        
        _ageCircle = ageCircle;
        _jobCircle = jobCircle;
        _incomeCircle = incomeCircle;
        _childCircle = childCircle;
        _outdoorCircle = outdoorCircle;
        _animalsCircle = animalsCircle;
        _commentCircle = commentCircle;
    }

    public static void ResetCircles()
    {
        _ageCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
    }

    public static bool IsSelected()
    {
        return _ageCircle.activeSelf || _jobCircle.activeSelf || _incomeCircle.activeSelf || _childCircle.activeSelf ||
               _outdoorCircle.activeSelf || _animalsCircle.activeSelf || _commentCircle.activeSelf;
    }

    private void AgeButtonClicked()
    {
        _ageCircle.SetActive(!_ageCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_ageCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_ageCircle.activeSelf);
        
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
    }

    private void JobButtonClicked()
    {
        _ageCircle.SetActive(false);
        
        _jobCircle.SetActive(!_jobCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_jobCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_jobCircle.activeSelf);
        
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
    }

    private void IncomeButtonClicked()
    {
        _ageCircle.SetActive(false);
        _jobCircle.SetActive(false);
        
        _incomeCircle.SetActive(!_incomeCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_incomeCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_incomeCircle.activeSelf);
        
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
    }

    private void ChildButtonClicked()
    {
        _ageCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
         
        _childCircle.SetActive(!_childCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_childCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_childCircle.activeSelf);
        
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
    }

    private void OutdoorButtonClicked()
    {
        _ageCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        
        _outdoorCircle.SetActive(!_outdoorCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_outdoorCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_outdoorCircle.activeSelf);
        
        _animalsCircle.SetActive(false);
        _commentCircle.SetActive(false);
    }

    private void AnimalsButtonClicked()
    {
        _ageCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        
        _animalsCircle.SetActive(!_animalsCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_animalsCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_animalsCircle.activeSelf);
        
        _commentCircle.SetActive(false);
    }

    private void CommentButtonClicked()
    {
        _ageCircle.SetActive(false);
        _jobCircle.SetActive(false);
        _incomeCircle.SetActive(false);
        _childCircle.SetActive(false);
        _outdoorCircle.SetActive(false);
        _animalsCircle.SetActive(false);
       
        _commentCircle.SetActive(!_commentCircle.activeSelf);
        DayManager.SetAcceptButtonActive(!_commentCircle.activeSelf);
        DayManager.SetDeclineButtonActive(_commentCircle.activeSelf);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
