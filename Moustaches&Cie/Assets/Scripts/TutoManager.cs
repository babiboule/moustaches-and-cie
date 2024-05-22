using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutoManager : MonoBehaviour
{
    // UI
    // Panel
    [SerializeField] private GameObject deskPanel;
    
    // Circles
    [SerializeField] private GameObject familyCircle;
    [SerializeField] private GameObject catCircle;
    [SerializeField] private GameObject postItCircle;
    [SerializeField] private GameObject memoCircle;
    [SerializeField] private GameObject stampCircle;

    // Buttons
    [SerializeField] private Button skipButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    
    //Sfx
    [SerializeField] private AudioClip buttonSfx;


    private void Awake()
    {
        // Add listeners on buttons
        skipButton.onClick.AddListener(SkipButtonClicked);
        yesButton.onClick.AddListener(YesButtonClicked);
        noButton.onClick.AddListener(NoButtonClicked);
    }

    /*
     * Call Skip()
     */
    private void SkipButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        StartCoroutine(Skip());
    }

    // Update is called once per frame
    void Update()
    {
        // Active/Disable panels and circle depending on the sentence
        deskPanel.SetActive(DialogueController.GetIndex() > 3 && DialogueController.GetIndex() < 18);
        
        familyCircle.SetActive(DialogueController.GetIndex() == 5);
        catCircle.SetActive(DialogueController.GetIndex() == 6);
        postItCircle.SetActive(DialogueController.GetIndex() == 7);
        memoCircle.SetActive(DialogueController.GetIndex() == 8);
        stampCircle.SetActive(DialogueController.GetIndex() == 9);
        yesButton.gameObject.SetActive(DialogueController.GetIndex() == 18);
        noButton.gameObject.SetActive(DialogueController.GetIndex() == 18);
        DialogueController.SetWaitInput(DialogueController.GetIndex() == 18);

        // Launch the game at the end of the tuto
        if (DialogueController.GetIndex() == 20)
        {
            SceneManager.LoadScene("Level");
        }
    }

    /*
     * Skip the tuto
     */
    private IEnumerator Skip()
    {
        while (DialogueController.GetIsWriting())
            yield return null;
            
        DialogueController.SetIndexTo(19);
    }

    /*
     * Launch the game
     */
    private void YesButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        DialogueController.SetIndexTo(19);
        DialogueController.SetWaitInput(false);
    }

    /*
     * Restart the tuto
     */
    private void NoButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        DialogueController.SetIndexTo(4);
        DialogueController.SetWaitInput(false);
    }
}