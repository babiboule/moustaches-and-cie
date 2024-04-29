using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // UI
    public Button toTitleButton;

    private void Awake()
    {
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
        GameManager.instance.ResetSave();
        GameManager.instance.LoadGame();
    }

    private void ToTitleButtonClicked()
    {
        
        GameManager.instance.UpdateGameState(GameManager.GameState.Play);
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }
}
