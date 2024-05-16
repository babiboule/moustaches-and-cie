using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private GameObject deskPanel;
    
    [SerializeField] private GameObject familyCircle;
    [SerializeField] private GameObject catCircle;
    [SerializeField] private GameObject postItCircle;
    [SerializeField] private GameObject memoCircle;
    [SerializeField] private GameObject stampCircle;

    [SerializeField] private Button skipButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    
    //Sfx
    [SerializeField] private AudioClip buttonSfx;


    private void Awake()
    {
        skipButton.onClick.AddListener(SkipButtonClicked);
        yesButton.onClick.AddListener(YesButtonClicked);
        noButton.onClick.AddListener(NoButtonClicked);
    }

    private void SkipButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        StartCoroutine(Skip());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deskPanel.SetActive(DialogueController.GetIndex() > 3 && DialogueController.GetIndex() < 18);
        
        familyCircle.SetActive(DialogueController.GetIndex() == 5);
        catCircle.SetActive(DialogueController.GetIndex() == 6);
        postItCircle.SetActive(DialogueController.GetIndex() == 7);
        memoCircle.SetActive(DialogueController.GetIndex() == 8);
        stampCircle.SetActive(DialogueController.GetIndex() == 9);
        yesButton.gameObject.SetActive(DialogueController.GetIndex() == 18);
        noButton.gameObject.SetActive(DialogueController.GetIndex() == 18);
        DialogueController.SetWaitInput(DialogueController.GetIndex() == 18);

        if (DialogueController.GetIndex() == 20)
        {
            SceneManager.LoadScene("Level");
        }
    }

    private IEnumerator Skip()
    {
        while (DialogueController.GetIsWriting())
            yield return null;
            
        DialogueController.SetIndexTo(19);
    }

    private void YesButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        DialogueController.SetIndexTo(19);
        DialogueController.SetWaitInput(false);
    }

    private void NoButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        DialogueController.SetIndexTo(4);
        DialogueController.SetWaitInput(false);
    }
}
