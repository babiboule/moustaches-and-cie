using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIManager : MonoBehaviour
{
    // UI 
    [SerializeField] private TMP_Text goodAdoptionTMP;
    [SerializeField] private TMP_Text badAdoptionTMP;
    [SerializeField] private TMP_Text badDeclineTMP;
    [SerializeField] private TMP_Text detailsTMP;
    [SerializeField] private TMP_Text levelTMP;
    [SerializeField] private Button nextDayButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button toTitleButton;
    
    // Daily stats
    private int m_GoodAdoptions;
    private int m_BadAdoptions;
    private int m_BadDecline;
    private string m_CatName;
    private string m_CatPb;
    private string m_FamilyPb;

    private void Awake()
    {
        nextDayButton.onClick.AddListener(NextDayButtonClicked);
        saveButton.onClick.AddListener(SaveButtonClicked);
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        saveButton.interactable = true;
        
        m_GoodAdoptions = StatsManager.instance.GetGoodAdoptions();
        m_BadAdoptions = StatsManager.instance.GetBadAdoptions();
        m_BadDecline = StatsManager.instance.GetBadDecline();

        goodAdoptionTMP.text = "Chats correctement placés : " + m_GoodAdoptions;
        badAdoptionTMP.text = "Chats mal placés : " + m_BadAdoptions;
        badDeclineTMP.text = "Dossiers valides refusés : " + m_BadDecline;
        detailsTMP.text = "Détails :";

        levelTMP.text = "Niveau : " + StatsManager.instance.GetLevel();

        List<string> adoptedTemp = new List<string>();
        
        foreach (LogicManager.Problem problem in StatsManager.instance.GetListProblems())
        {
            detailsTMP.text += "\n" + problem.Cat.name + " - " + problem.PbCat + " - " + problem.PbFamily;

            foreach (string cat in StatsManager.instance.GetAdoptedCats())
            {
                if (cat == problem.Cat.name)
                {
                    adoptedTemp.Add(cat);
                }
            }
        }

        for (int i = 0; i < adoptedTemp.Count ; i++)
        {
            StatsManager.instance.RemoveAdoptedCat(adoptedTemp[i]);
        }
    }
    
    private void NextDayButtonClicked()
    {
        switch (StatsManager.instance.GetLevel())
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
    
    private void SaveButtonClicked()
    {
        GameManager.instance.SaveGame();
        saveButton.interactable = false;
    }

    private void ToTitleButtonClicked()
    {
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }
}
