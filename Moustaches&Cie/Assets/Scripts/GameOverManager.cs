using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // UI
    public Button toTitleButton;

    [SerializeField] private AudioClip buttonSfx;

    private void Awake()
    {
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
        GameManager.instance.ResetSave();
        GameManager.instance.LoadGame();
    }

    private void ToTitleButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        GameManager.instance.UpdateGameState(GameManager.GameState.Play);
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }
}
