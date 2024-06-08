using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // UI
    [SerializeField] private Button toTitleButton;
    [SerializeField] private GameObject colleaguePanel;
    
    // Sfx
    [SerializeField] private AudioClip buttonSfx;

    private void Awake()
    {
        // Add listener to button
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
        
        // Reset the stats
        GameManager.instance.ResetSave();
        GameManager.instance.LoadGame();
    }

    private void Start()
    {
        if(StatsManager.instance.gameOver)
            StartCoroutine(GameOver());
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
        var str = new[] { s1, s2, s3 };
        
        colleaguePanel.SetActive(true);
        StartCoroutine(DialogueController.WriteDialog(str));
        while (DialogueController.GetIsWriting())
            yield return null;
        colleaguePanel.SetActive(false);
    }
}