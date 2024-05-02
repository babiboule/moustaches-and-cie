using System;
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
        
        StatsManager.instance.AddDate();
        saveButton.interactable = true;
        
        m_GoodAdoptions = StatsManager.instance.GetGoodAdoptions();
        m_BadAdoptions = StatsManager.instance.GetBadAdoptions();
        m_BadDecline = StatsManager.instance.GetBadDecline();
        
        goodAdoptionTMP.text = "Chats correctement placés : " + m_GoodAdoptions;
        badAdoptionTMP.text = "Chats mal placés : " + m_BadAdoptions;
        badDeclineTMP.text = "Dossiers valides refusés : " + m_BadDecline;
        detailsTMP.text = "Détails :";

        UpExp();
        levelTMP.text = "Niveau : " + StatsManager.instance.GetLevel();
        
        PrintProblems();
        
        ///////////       DEBUG         ////////////////////////////////
        Debug.Log(StatsManager.instance.GetLevel());
        Debug.Log(StatsManager.instance.GetExp());
    }

    private void UpExp()
    {
        StatsManager.instance.SetExp(StatsManager.instance.GetExp()+StatsManager.instance.upExp*m_GoodAdoptions);
        var exp = StatsManager.instance.GetExp();
        if (exp >= StatsManager.instance.upLvl3)
        {
            StatsManager.instance.SetLevel(4);
        }
        else if (exp >= StatsManager.instance.upLvl2)
        {
            StatsManager.instance.SetLevel(3);
        }
        else if (exp >= StatsManager.instance.upLvl1)
        {
            StatsManager.instance.SetLevel(2);
        }
    }
    
    private void PrintProblems()
    {
        List<string> adoptedTemp = new List<string>();
        
        foreach (LogicManager.Problem problem in StatsManager.instance.GetListProblems())
        {
            detailsTMP.text += "\n" + problem.Cat.name;

            switch (problem.PbCat)
            {
                case LogicManager.PbCat.Kitten:
                    detailsTMP.text += " - chaton";
                    break;
                case LogicManager.PbCat.Sick:
                    detailsTMP.text += " - malade";
                    break;
                case LogicManager.PbCat.Shy:
                    detailsTMP.text += " - peureux.se";
                    break;
                case LogicManager.PbCat.Agressive:
                    detailsTMP.text += " - agressif.ve";
                    break;
                case LogicManager.PbCat.Outdoor:
                    detailsTMP.text += " - besoin d'extérieur";
                    break;
                case LogicManager.PbCat.Animals:
                    detailsTMP.text += " - pas ok animaux";
                    break;
                case LogicManager.PbCat.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (problem.PbFamily)
            {
                case LogicManager.PbFamily.TooOld:
                    detailsTMP.text += " - trop vieux.ieille";
                    break;
                case LogicManager.PbFamily.TooYoung:
                    detailsTMP.text += " - mineur.e";
                    break;
                case LogicManager.PbFamily.TooPoor:
                    detailsTMP.text += " - trop peu d'argent";
                    break;
                case LogicManager.PbFamily.TooBusy:
                    detailsTMP.text += " - trop occupé.e";
                    break;
                case LogicManager.PbFamily.Child:
                    detailsTMP.text += " - a un enfant";
                    break;
                case LogicManager.PbFamily.NoOutdoor:
                    detailsTMP.text += " - pas d'extérieur";
                    break;
                case LogicManager.PbFamily.Animals:
                    detailsTMP.text += " - a un animal";
                    break;
                case LogicManager.PbFamily.Comment:
                    detailsTMP.text += " - commentaire pas ok";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

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
        if (StatsManager.instance.GetDate() > StatsManager.instance.dayMax)
        {
            GameManager.instance.UpdateGameState(GameManager.GameState.GameOver);
        }
        else
        {
            switch (StatsManager.instance.GetLevel())
            {
                case 1:
                    GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level1);
                    break;
                case 2:
                    GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level2);
                    break;
                case 3:
                    GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level3);
                    break;
                case 4:
                    GameManager.instance.UpdateGameLevel(GameManager.GameLevel.LevelMax);
                    break;
                default:
                    Debug.Log("LEVEL PROBLEM");
                    break;
            }
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
