using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreUIManager : MonoBehaviour
{
    // UI 
    [SerializeField] private TMP_Text goodAdoptionTMP;
    [SerializeField] private TMP_Text badAdoptionTMP;
    [SerializeField] private TMP_Text goodDeclineTMP;
    [SerializeField] private TMP_Text badDeclineTMP;
    [SerializeField] private TMP_Text detailsTMP;
    [SerializeField] private TMP_Text levelTMP;
    [SerializeField] private TMP_Text upExpGoodAdoptionTMP;
    [SerializeField] private TMP_Text upExpGoodDeclineTMP;
    [SerializeField] private TMP_Text totalExpTMP;
    [SerializeField] private Button nextDayButton;
    [SerializeField] private Button toTitleButton;
    [SerializeField] private Image xpBar;
    
    // Daily stats
    private int _goodAdoptions;
    private int _badAdoptions;
    private int _goodDecline;
    private int _badDecline;
    private string _catName;
    private string _catPb;
    private string _familyPb;
    
    // Sfx
    [SerializeField] private AudioClip dayEndSfx;
    [SerializeField] private AudioClip typewriterSfx;
    [SerializeField] private AudioClip expUpSfx;
    [SerializeField] private AudioClip lvlUpSfx;
    [SerializeField] private AudioClip buttonSfx;

    private void Awake()
    {
        // Add listeners on buttons
        nextDayButton.onClick.AddListener(NextDayButtonClicked);
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(dayEndSfx);
        
        // Increment the day
        StatsManager.instance.AddDate();
        
        // Get the stats of the day
        _goodAdoptions = StatsManager.instance.GetGoodAdoptions();
        _badAdoptions = StatsManager.instance.GetBadAdoptions();
        _goodDecline = StatsManager.instance.GetGoodDecline();
        _badDecline = StatsManager.instance.GetBadDecline();
        
        // Print the stats of the day
        goodAdoptionTMP.text = "Chats bien placés : " + _goodAdoptions;
        badAdoptionTMP.text = "Chats mal placés : " + _badAdoptions;
        goodDeclineTMP.text = StatsManager.instance.GetLevel() > 1 ? "Refus bien justifiés : " + _goodDecline: "";
        badDeclineTMP.text = StatsManager.instance.GetLevel() > 1 ? "Mauvaises justifications : " + _badDecline : "Bons dossiers refusés : " + _badDecline;

        detailsTMP.text = "Détails : \n";
        upExpGoodAdoptionTMP.text = "+ " + StatsManager.instance.upExp * _goodAdoptions + " exp";
        upExpGoodDeclineTMP.text = StatsManager.instance.GetLevel() > 1 ? "+ " + StatsManager.instance.upExp / 2 * _goodDecline + " exp" : "";

        // Update the gained exp
        UpExp();
        
        // Set the xp text
        totalExpTMP.text = StatsManager.instance.GetLevel() switch
        {
            1 => StatsManager.instance.GetExp() + " / " + StatsManager.instance.upLvl1,
            2 => StatsManager.instance.GetExp() + " / " + StatsManager.instance.upLvl2,
            3 => StatsManager.instance.GetExp() + " / " + StatsManager.instance.upLvl3,
            _ => "Exp Max !"
        };
        
        levelTMP.text = "Niveau : " + StatsManager.instance.GetLevel();
        
        // Print the detailed problems
        PrintProblems();
        
        // Save the game
        GameManager.instance.SaveGame();
    }

    /*
     * Add exp points depending on the good adoptions made on the day
     */
    private void UpExp()
    {
        // Set xp
        StatsManager.instance.SetExp(StatsManager.instance.GetExp()+StatsManager.instance.upExp*_goodAdoptions+StatsManager.instance.upExp/2*_goodDecline);
        float exp = StatsManager.instance.GetExp();
        
        // Switch level if enough xp and fill the xp bar
        if (exp >= StatsManager.instance.upLvl3)
        {
            StatsManager.instance.SetLevel(4);
            xpBar.fillAmount = 1;
        }
        else if (exp >= StatsManager.instance.upLvl2)
        {
            StatsManager.instance.SetLevel(3);
            if(StatsManager.instance.GetTutoLvl() == 2)
            {
                StatsManager.instance.SetTuto(true);
                StatsManager.instance.SetTutoLevel(3);
            }
            xpBar.fillAmount = (exp - StatsManager.instance.upLvl2) / (StatsManager.instance.upLvl3-StatsManager.instance.upLvl2);
        }
        else if (exp >= StatsManager.instance.upLvl1)
        {
            StatsManager.instance.SetLevel(2);
            if(StatsManager.instance.GetTutoLvl() == 1)
            {
                StatsManager.instance.SetTuto(true);
                StatsManager.instance.SetTutoLevel(2);
            }
            xpBar.fillAmount = (exp - StatsManager.instance.upLvl1) / (StatsManager.instance.upLvl2-StatsManager.instance.upLvl1);
        }
        else
        {
            xpBar.fillAmount = exp / StatsManager.instance.upLvl1;
        }

    }
    
    /*
     * Print the details of the cat adopted by bad families
     */
    private void PrintProblems()
    {
        var adoptedTemp = new List<string>();
        
        // For each problem, print the cat name, the cat problem and the family problem
        foreach (var problem in StatsManager.instance.GetListProblems())
        {
            detailsTMP.text += "\n" + problem.Cat.name;

            detailsTMP.text += problem.PbCat switch
            {
                LogicManager.PbCat.Kitten => " : chaton",
                LogicManager.PbCat.Sick => " : malade",
                LogicManager.PbCat.Shy => " : peureux.se",
                LogicManager.PbCat.Lover => " : pot de colle",
                LogicManager.PbCat.Outdoor => " : besoin d'extérieur",
                LogicManager.PbCat.NoOkAnimals => " : pas ok chats",
                LogicManager.PbCat.Disable => " : handicap",
                LogicManager.PbCat.NeedAnimals => " : besoin d'un autre chat",
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
                LogicManager.PbFamily.OpenOutdoor => " - extérieur ouvert",
                LogicManager.PbFamily.Outdoor => " - a un extérieur",
                LogicManager.PbFamily.NoAnimals => " - pas d'autre chat",
                _ => throw new ArgumentOutOfRangeException()
            };
            
            foreach (var cat in StatsManager.instance.GetAdoptedCats())
            {
                if (cat == problem.Cat.name)
                    adoptedTemp.Add(cat);
                
            }
        }

        // If the cat has been adopted by a bad family, it is not adopted anymore and come back in the current cats
        foreach (var t in adoptedTemp)
        {
            StatsManager.instance.RemoveAdoptedCat(t);
        }
    }
    
    /*
     * Go to the next day when the button is clicked
     */
    private void NextDayButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        // End the game if it was the last day, else go on next day 
        if (StatsManager.instance.GetDate() > StatsManager.instance.dayMax)
            GameManager.instance.UpdateGameState(GameManager.GameState.GameOver);
        
        else
            GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Level);
    }

    /*
     * Go to title screen when the button is clicked
     */
    private void ToTitleButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        
        // Switch the game level to the title screen
        GameManager.instance.UpdateGameLevel(GameManager.GameLevel.Title);
    }
}