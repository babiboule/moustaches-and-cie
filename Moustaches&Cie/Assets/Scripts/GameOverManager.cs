using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // UI
    [SerializeField] private Button toTitleButton;
    
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
}