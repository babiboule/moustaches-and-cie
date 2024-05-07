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

            detailsTMP.text += problem.PbCat switch
            {
                LogicManager.PbCat.Kitten => " - chaton",
                LogicManager.PbCat.Sick => " - malade",
                LogicManager.PbCat.Shy => " - peureux.se",
                LogicManager.PbCat.Lover => " - pot de colle",
                LogicManager.PbCat.Outdoor => " - besoin d'extérieur",
                LogicManager.PbCat.NoOkAnimals => " - pas ok chats",
                LogicManager.PbCat.Disable => " - handicap",
                LogicManager.PbCat.NeedAnimals => " - besoin d'un autre chat",
                LogicManager.PbCat.None => "",
                _ => throw new ArgumentOutOfRangeException()
            };

            detailsTMP.text += problem.PbFamily switch
            {
                LogicManager.PbFamily.TooOld => " - trop vieux.ieille",
                LogicManager.PbFamily.TooYoung => " - mineur.e",
                LogicManager.PbFamily.TooPoor => " - budget trop faible",
                LogicManager.PbFamily.TooBusy => " - trop occupé.e",
                LogicManager.PbFamily.Child => " - a un enfant",
                LogicManager.PbFamily.NoOutdoor => " - pas d'extérieur",
                LogicManager.PbFamily.Animals => " - a un autre chat",
                LogicManager.PbFamily.Comment => " - commentaire pas ok",
                LogicManager.PbFamily.TooFar => " - pas dans le Bas-Rhin",
                LogicManager.PbFamily.NoCar => " - pas de voiture",
                LogicManager.PbFamily.OpenOutdoor => " - extérieur pas sécurisé",
                LogicManager.PbFamily.Outdoor => " - pas d'extérieur",
                LogicManager.PbFamily.NoAnimals => " - pas d'autre chat",
                _ => throw new ArgumentOutOfRangeException()
            };

            foreach (string cat in StatsManager.instance.GetAdoptedCats())
            {
                if (cat == problem.Cat.name)
                {
                    adoptedTemp.Add(cat);
                }
            }
        }

        foreach (var t in adoptedTemp)
        {
            StatsManager.instance.RemoveAdoptedCat(t);
        }
    }
    
    private static void NextDayButtonClicked()
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

    private static void ToTitleButtonClicked()
    {
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }
}
