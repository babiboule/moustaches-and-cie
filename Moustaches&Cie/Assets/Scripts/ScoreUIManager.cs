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
    [SerializeField] private TMP_Text upExpTMP;
    [SerializeField] private TMP_Text totalExpTMP;
    [SerializeField] private Button nextDayButton;
    [SerializeField] private Button toTitleButton;
    [SerializeField] private Image xpBar;
    
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
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        StatsManager.instance.AddDate();
        
        m_GoodAdoptions = StatsManager.instance.GetGoodAdoptions();
        m_BadAdoptions = StatsManager.instance.GetBadAdoptions();
        m_BadDecline = StatsManager.instance.GetBadDecline();
        
        goodAdoptionTMP.text = "Chats bien placés : " + m_GoodAdoptions;
        badAdoptionTMP.text = "Chats mal placés : " + m_BadAdoptions;
        if (StatsManager.instance.GetLevel() > 1)
        {
            badDeclineTMP.text = "Mauvaises justifications : " + m_BadDecline;
        }
        else
        {
            badDeclineTMP.text = "Bons dossiers refusés : " + m_BadDecline;
        }

        detailsTMP.text = "Détails :";
        upExpTMP.text = "+ " + StatsManager.instance.upExp * m_GoodAdoptions + " exp";

        UpExp();
        
        switch (StatsManager.instance.GetLevel())
        {
            case 1:
                totalExpTMP.text = StatsManager.instance.GetExp() + " / " + StatsManager.instance.upLvl1;
                break;
            case 2:
                totalExpTMP.text = StatsManager.instance.GetExp() + " / " + StatsManager.instance.upLvl2;
                break;
            case 3:
                totalExpTMP.text = StatsManager.instance.GetExp() + " / " + StatsManager.instance.upLvl3;
                break;
            default:
                totalExpTMP.text = "Exp Max !";
                break;
        }

        levelTMP.text = "Niveau : " + StatsManager.instance.GetLevel();
        
        PrintProblems();
        
        GameManager.instance.SaveGame();
        
        ///////////       DEBUG         ////////////////////////////////
        Debug.Log(StatsManager.instance.GetLevel());
        Debug.Log(StatsManager.instance.GetExp());
    }

    private void UpExp()
    {
        StatsManager.instance.SetExp(StatsManager.instance.GetExp()+StatsManager.instance.upExp*m_GoodAdoptions);
        float exp = StatsManager.instance.GetExp();
        if (exp >= StatsManager.instance.upLvl3)
        {
            StatsManager.instance.SetLevel(4);
            xpBar.fillAmount = 1;
        }
        else if (exp >= StatsManager.instance.upLvl2)
        {
            StatsManager.instance.SetLevel(3);
            xpBar.fillAmount = (exp - StatsManager.instance.upLvl2) / (StatsManager.instance.upLvl3-StatsManager.instance.upLvl2);
        }
        else if (exp >= StatsManager.instance.upLvl1)
        {
            StatsManager.instance.SetLevel(2);
            xpBar.fillAmount = (exp - StatsManager.instance.upLvl1) / (StatsManager.instance.upLvl2-StatsManager.instance.upLvl1);
        }
        else
        {
            xpBar.fillAmount = exp / StatsManager.instance.upLvl1;
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
            GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level);
        }

    }

    private void ToTitleButtonClicked()
    {
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }
}
