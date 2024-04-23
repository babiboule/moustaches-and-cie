using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIManager : MonoBehaviour
{
    // UI 
    [SerializeField] private TMP_Text goodAdoptionTMP;
    [SerializeField] private TMP_Text badAdoptionTMP;
    [SerializeField] private TMP_Text detailsTMP;
    [SerializeField] private TMP_Text levelTMP;
    [SerializeField] private Button nextDayButton;
    [SerializeField] private Button toTitleButton;
    
    // Daily stats
    private int m_GoodAdoptions;
    private int m_BadAdoptions;
    private string m_CatName;
    private string m_CatPb;
    private string m_FamilyPb;

    private void Awake()
    {
        nextDayButton.onClick.AddListener(NextDayButtonClicked);
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_GoodAdoptions = StatsManager.instance.GoodAdoptions;
        m_BadAdoptions = StatsManager.instance.BadAdoptions;
    }
    
    private void NextDayButtonClicked()
    {
        Debug.Log("JE CLIQUE");
        switch (StatsManager.instance.level)
        {
            case 1:
                GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level1);
                break;
            case 2:
                GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level2);
                break;
            case 3 :
                GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level3);
                break;
            case 4 :
                GameManager.instance.UpdateGameLevel(GameManager.GameLevel.LevelMax);
                break;
            default:
                Debug.Log("LEVEL PROBLEM");
                break;
        }
        
    }

    private void ToTitleButtonClicked()
    {
        Debug.Log("LA AUSSI JE CLIQUE");
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }
}
