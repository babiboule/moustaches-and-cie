using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // UI
    [SerializeField] private Button toTitleButton;
    [SerializeField] private GameObject colleaguePanel;

    [SerializeField] private GameObject teamPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject thanksPanel;

    // First panel txt
    [SerializeField] private TMP_Text txt1;
    [SerializeField] private TMP_Text txt2;
    // Second panel txt
    [SerializeField] private TMP_Text txt3;
    [SerializeField] private TMP_Text txt4;
    [SerializeField] private TMP_Text txt5;
    [SerializeField] private TMP_Text txt6;
    [SerializeField] private TMP_Text txt7;
    [SerializeField] private TMP_Text txt8;
    [SerializeField] private TMP_Text txt9;
    [SerializeField] private TMP_Text txt10;
    // Last panel txt
    [SerializeField] private TMP_Text txt11;
    [SerializeField] private TMP_Text txt12;
    


    private bool _isFading;
    private float _delay = 0.01f;
    
    // Sfx
    [SerializeField] private AudioClip buttonSfx;

    private void Awake()
    {
        // Add listener to button
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
        
        teamPanel.SetActive(false);
        creditsPanel.SetActive(false);
        thanksPanel.SetActive(false);

        txt1.color = new Color(1, 1, 1, 0);
        txt2.color = new Color(1, 1, 1, 0);
        txt3.color = new Color(1, 1, 1, 0);
        txt4.color = new Color(1, 1, 1, 0);
        txt5.color = new Color(1, 1, 1, 0);
        txt6.color = new Color(1, 1, 1, 0);
        txt7.color = new Color(1, 1, 1, 0);
        txt8.color = new Color(1, 1, 1, 0);
        txt9.color = new Color(1, 1, 1, 0);
        txt10.color = new Color(1, 1, 1, 0);
        txt11.color = new Color(1, 1, 1, 0);
        txt12.color = new Color(1, 1, 1, 0);
    }

    private void Start()
    {
        if(StatsManager.instance.gameOver)
        {
            StartCoroutine(GameOver());
        }

        StartCoroutine(Credits());
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            toTitleButton.gameObject.SetActive(true);
        }
    }

    /*
     * Animation of the credits
     */
    private IEnumerator Credits()
    {
        toTitleButton.gameObject.SetActive(false);
        
        // Wait for the game over screen to finish
        while (StatsManager.instance.gameOver)
            yield return null;
        
        // First Panel
        teamPanel.SetActive(true);
        
        //Fade in
        StartCoroutine(FadeText(txt1, true));
        while (_isFading)
            yield return null;
        
        StartCoroutine(FadeText(txt2, true));
        while (_isFading)
            yield return null;

        // Wait
        yield return new WaitForSeconds(5);
        
        // Fade Out
        StartCoroutine(FadeText(txt1, false));
        StartCoroutine(FadeText(txt2, false));
        while (_isFading)
            yield return null;
        
        // Second Panel
        teamPanel.SetActive(false);
        creditsPanel.SetActive(true);
        
        //Fade in
        StartCoroutine(FadeText(txt3, true));
        StartCoroutine(FadeText(txt4, true));
        StartCoroutine(FadeText(txt5, true));
        StartCoroutine(FadeText(txt6, true));
        StartCoroutine(FadeText(txt7, true));
        StartCoroutine(FadeText(txt8, true));
        StartCoroutine(FadeText(txt9, true));
        StartCoroutine(FadeText(txt10, true));
        while (_isFading)
            yield return null;

        // Wait
        yield return new WaitForSeconds(5);
        
        // Fade Out
        StartCoroutine(FadeText(txt3, false));
        StartCoroutine(FadeText(txt4, false));
        StartCoroutine(FadeText(txt5, false));
        StartCoroutine(FadeText(txt6, false));
        StartCoroutine(FadeText(txt7, false));
        StartCoroutine(FadeText(txt8, false));
        StartCoroutine(FadeText(txt9, false));
        StartCoroutine(FadeText(txt10, false));
        while (_isFading)
            yield return null;
        
        // Last panel
        creditsPanel.SetActive(false);
        thanksPanel.SetActive(true);
        
        //Fade in
        StartCoroutine(FadeText(txt11, true));
        while (_isFading)
            yield return null;
        
        StartCoroutine(FadeText(txt12, true));
        while (_isFading)
            yield return null;
        
        toTitleButton.gameObject.SetActive(true);
    }
    
    /*
     * Go to the title screen when clicking on the button
     */
    private void ToTitleButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        // Update the game to switch on the title screen
        GameManager.instance.UpdateGameState(GameManager.GameState.Play);
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }

    private IEnumerator GameOver()
    {
        const string s1 = "Je suis désolé mais on va devoir arrêter ton essai ici.";
        const string s2 = "On a eu trop de retours négatifs et c'est la vie des chats que tu mets en danger...";
        const string s3 = "Réessaie quand tu seras prêt.e.";
        
        const string s4 = "Je suis désolé mais on va devoir arrêter ton essai ici.";
        const string s5 = "Tu as fait du bon travail, mais on a trouvé quelqu'un qui est plus efficace que toi...";
        const string s6 = "Réessaie quand tu seras prêt.e.";
        
        const string s7 = "Félicitations, c'est la fin de ton essai ici.";
        const string s8 = "Tu as fait du bon travail, nous t'en sommes reconnaissant !";
        const string s9 = "N'oublies pas : un chat adopté, c'est deux chats de sauvés !";

        var str = StatsManager.instance.GetLevel() switch
        {
            1 => new[] { s1, s2, s3 },
            2 => new[] { s4, s5, s6 },
            _ => new[] { s7, s8, s9 }
        };

        colleaguePanel.SetActive(true);
        StartCoroutine(DialogueController.WriteDialog(str));
        while (DialogueController.GetIsWriting())
            yield return null;
        colleaguePanel.SetActive(false);
        
        // Reset the stats
        GameManager.instance.ResetSave();
        GameManager.instance.LoadGame();
        StatsManager.instance.gameOver = false;
    }

    private IEnumerator FadeText(TMP_Text txt, bool fadeIn)
    {
        _isFading = true;
        
        if (fadeIn)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                txt.color = new Color(1, 1, 1, i);
                yield return new WaitForSeconds(_delay);
            }
        }
        else
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                txt.color = new Color(1, 1, 1, i);
                yield return new WaitForSeconds(_delay);
            }
        }

        _isFading = false;
    }
}