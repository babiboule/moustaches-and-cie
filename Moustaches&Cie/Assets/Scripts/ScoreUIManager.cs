using System;
using System.Collections;
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
    [SerializeField] private TMP_Text downExpBadAdoptionTMP;
    [SerializeField] private TMP_Text downExpBadDeclineTMP;
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
    
    // Const 
    private const float BlankTime = 0.2f;
    private const float IncrementTime = 0.005f;

    // Sfx
    [SerializeField] private AudioClip dayEndSfx;
    [SerializeField] private AudioClip lvlUpSfx;
    [SerializeField] private AudioClip buttonSfx;
    
    // Success
    [SerializeField] private GameObject successFrame;
    [SerializeField] private GameObject bronzeMedal;
    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject goldenMedal;
    [SerializeField] private TMP_Text successTxt;
    [SerializeField] private TMP_Text successNewCats;
    

    private void Awake()
    {
        // Add listeners on buttons
        nextDayButton.onClick.AddListener(NextDayButtonClicked);
        toTitleButton.onClick.AddListener(ToTitleButtonClicked);
        
        // Clear texts
        goodAdoptionTMP.text = "";
        badAdoptionTMP.text = "";
        goodDeclineTMP.text = "";
        badDeclineTMP.text = "";
        detailsTMP.text = "";
        upExpGoodAdoptionTMP.text = "";
        upExpGoodDeclineTMP.text = "";
        downExpBadAdoptionTMP.text = "";
        downExpBadDeclineTMP.text = "";
    }

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        nextDayButton.interactable = false;
        toTitleButton.interactable = false;
        
        // Sfx
        SfxManager.instance.PlaySfxClip(dayEndSfx);
        
        // Increment the day
        StatsManager.instance.AddDate();
        StatsManager.instance.day++;
        
        // Get the stats of the day
        _goodAdoptions = StatsManager.instance.GetGoodAdoptions();
        _badAdoptions = StatsManager.instance.GetBadAdoptions();
        _goodDecline = StatsManager.instance.GetGoodDecline();
        _badDecline = StatsManager.instance.GetBadDecline();
        
        // Print last stats (xp + lvl)
        xpBar.fillAmount = StatsManager.instance.GetLevel() switch
        {
            1 => StatsManager.instance.GetExp() / (float)StatsManager.instance.upLvl1,
            2 => (StatsManager.instance.GetExp()-StatsManager.instance.upLvl1) / (float)StatsManager.instance.upLvl2,
            3 => (StatsManager.instance.GetExp()-StatsManager.instance.upLvl2) / (float)StatsManager.instance.upLvl3,
            _ => 1
        };
        totalExpTMP.text = StatsManager.instance.GetExp() + " / " + StatsManager.instance.GetLevel() switch
        {
            1 => StatsManager.instance.upLvl1,
            2 => StatsManager.instance.upLvl2,
            3 => StatsManager.instance.upLvl3,
            _ => StatsManager.instance.upLvl3,
        };
        levelTMP.text = "Niveau : " + StatsManager.instance.GetLevel();
        
        // Print the stats of the day
        yield return PrintDailyStats();
        
        yield return new WaitForSeconds(BlankTime);
        
        // Print the detailed problems
        detailsTMP.text = "Détails : \n";
        PrintProblems();
        
        yield return new WaitForSeconds(BlankTime);
        
        // Update the gained exp
        yield return UpExp();
        
        // Set the xp text
        totalExpTMP.text = StatsManager.instance.GetLevel() switch
        {
            1 => StatsManager.instance.GetExp() + " / " + StatsManager.instance.upLvl1,
            2 => StatsManager.instance.GetExp() + " / " + StatsManager.instance.upLvl2,
            3 => StatsManager.instance.GetExp() + " / " + StatsManager.instance.upLvl3,
            _ => "Exp Max !"
        };
        
        levelTMP.text = "Niveau : " + StatsManager.instance.GetLevel();
        
        // New adoptions
        yield return NewAdoptionsSuccess();
        
        // Save the game
        GameManager.instance.SaveGame();
        nextDayButton.interactable = true;
        toTitleButton.interactable = true;
        StatsManager.instance.newAdoptions = 0;
    }

    private IEnumerator PrintDailyStats()
    {
        yield return new WaitForSeconds(BlankTime);
        
        // Good adoptions
        goodAdoptionTMP.text = "Chats bien placés : " + _goodAdoptions;
        yield return new WaitForSeconds(BlankTime);
        var i = 0;
        while (i < StatsManager.instance.bonusGoodAdoption * _goodAdoptions)
        {
            i++;
            upExpGoodAdoptionTMP.text = "+ " + i + "exp";
            SfxManager.instance.PlaySfxClip(buttonSfx);
            yield return new WaitForSeconds(IncrementTime);
        }
        
        yield return new WaitForSeconds(BlankTime);

        // Good declines
        if (StatsManager.instance.GetLevel() > 1)
        {
            goodDeclineTMP.text = "Refus bien justifiés : " + _goodDecline;
            i = 0;
            while (i < StatsManager.instance.bonusGoodDecline * _goodDecline)
            {
                i++;
                upExpGoodDeclineTMP.text = "+ " + i + "exp";
                SfxManager.instance.PlaySfxClip(buttonSfx);
                yield return new WaitForSeconds(IncrementTime);
            }
        }
        
        yield return new WaitForSeconds(BlankTime);
        
        // Bad Adoptions
        badAdoptionTMP.text = "Chats mal placés : " + _badAdoptions;
        yield return new WaitForSeconds(BlankTime);
        i = 0;
        while (i < StatsManager.instance.malusBadAdoption * _badAdoptions)
        {
            i++;
            downExpBadAdoptionTMP.text = "- " + i + "exp";
            SfxManager.instance.PlaySfxClip(buttonSfx);
            yield return new WaitForSeconds(IncrementTime);
        }
        
        yield return new WaitForSeconds(BlankTime);
        
        // Bad Declines
        if (StatsManager.instance.GetLevel() > 1)
        {
            badDeclineTMP.text = "Mauvais refus : " + _badDecline;
            i = 0;
            while (i <StatsManager.instance.malusBadDecline * _badDecline)
            {
                i++;
                downExpBadDeclineTMP.text = "- " + i + " exp";
                SfxManager.instance.PlaySfxClip(buttonSfx);
                yield return new WaitForSeconds(IncrementTime);
            }
        }
    }

    /*
     * Add exp points depending on the good adoptions made on the day
     */
    private IEnumerator UpExp()
    {
        // Set xp
        StatsManager.instance.SetExp(StatsManager.instance.GetExp() + StatsManager.instance.bonusGoodAdoption*_goodAdoptions
                                     - StatsManager.instance.malusBadAdoption*_badAdoptions);
        if(StatsManager.instance.GetLevel()>1)
            StatsManager.instance.SetExp(StatsManager.instance.GetExp() + StatsManager.instance.bonusGoodDecline*_goodDecline
                                         - StatsManager.instance.malusBadDecline*_badDecline);
        float exp = StatsManager.instance.GetExp();
        
        // Switch level if enough xp and fill the xp bar
        if (exp >= StatsManager.instance.upLvl3)
        {
            yield return(StatsManager.instance.GetLevel() == 3
                ? AnimLevelUp(1)
                : AnimXpBar(1));
            
            StatsManager.instance.SetLevel(4);
            StatsManager.instance.day = 0;
        }
        else if (exp >= StatsManager.instance.upLvl2)
        {
            yield return (StatsManager.instance.GetLevel() == 2
                ? AnimLevelUp((exp - StatsManager.instance.upLvl2) /
                              (StatsManager.instance.upLvl3 - StatsManager.instance.upLvl2))
                : AnimXpBar((exp - StatsManager.instance.upLvl2) /
                            (StatsManager.instance.upLvl3 - StatsManager.instance.upLvl2)));
            
            StatsManager.instance.SetLevel(3);
            StatsManager.instance.day = 0;
            if(StatsManager.instance.GetTutoLvl() == 2)
            {
                StatsManager.instance.SetTuto(true);
                StatsManager.instance.SetTutoLevel(3);
            }
        }
        else if (exp >= StatsManager.instance.upLvl1)
        {
            yield return (StatsManager.instance.GetLevel() == 1
                ? AnimLevelUp((exp - StatsManager.instance.upLvl1) /
                              (StatsManager.instance.upLvl2 - StatsManager.instance.upLvl1))
                : AnimXpBar((exp - StatsManager.instance.upLvl1) /
                            (StatsManager.instance.upLvl2 - StatsManager.instance.upLvl1)));
            
            StatsManager.instance.SetLevel(2);
            StatsManager.instance.day = 0;
            if(StatsManager.instance.GetTutoLvl() == 1)
            {
                StatsManager.instance.SetTuto(true);
                StatsManager.instance.SetTutoLevel(2);
            }
        }
        else
        {
            yield return (AnimXpBar(exp / StatsManager.instance.upLvl1));
        }
    }

    private IEnumerator AnimXpBar(float expMax)
    {
        var exp = xpBar.fillAmount;
        while (exp < expMax)
        {
            exp += .01f;
            xpBar.fillAmount = exp;
            
            SfxManager.instance.PlaySfxClip(buttonSfx);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator AnimLevelUp(float expMax)
    {
        var exp = xpBar.fillAmount;
        while (exp < 1)
        {
            exp += .01f;
            xpBar.fillAmount = exp;
            
            SfxManager.instance.PlaySfxClip(buttonSfx);
            yield return new WaitForSeconds(0.05f);
        }

        // Level up frame
        SfxManager.instance.PlaySfxClip(dayEndSfx);
        successFrame.SetActive(true);
        
        bronzeMedal.SetActive(StatsManager.instance.GetLevel() == 1);
        silverMedal.SetActive(StatsManager.instance.GetLevel() == 2);
        goldenMedal.SetActive(StatsManager.instance.GetLevel() == 3);
        successTxt.text = "LVL UP !";

        while (!Input.GetButtonDown("Fire1"))
        {
            yield return null;
        }
        
        bronzeMedal.SetActive(false);
        silverMedal.SetActive(false);
        goldenMedal.SetActive(false);
        successFrame.SetActive(false);
        successTxt.text = "";

        exp = 0;
        while (exp < expMax)
        {
            exp += .01f;
            xpBar.fillAmount = exp;
            
            SfxManager.instance.PlaySfxClip(buttonSfx);
            yield return new WaitForSeconds(0.05f);
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
            
            // Remove the adopted cat is already adopted ?
            foreach (var cat in StatsManager.instance.GetAdoptedCats())
            {
                if (cat == problem.Cat.name)
                {
                    adoptedTemp.Add(cat);
                }
                
            }
        }

        // If the cat has been adopted by a bad family, it is not adopted anymore and come back in the current cats
        foreach (var t in adoptedTemp)
        {
            StatsManager.instance.RemoveAdoptedCat(t);
        }
    }

    private IEnumerator NewAdoptionsSuccess()
    {
        if (StatsManager.instance.newAdoptions <= 0) 
            yield break;
        
        successFrame.SetActive(true);
        successTxt.text = "Nouvelles photos dans l'album !";
        successNewCats.gameObject.SetActive(true);
        successNewCats.text = StatsManager.instance.newAdoptions.ToString();

        while (!Input.GetButtonDown("Fire1"))
        {
            yield return null;
        }

        successFrame.SetActive(false);
        successNewCats.gameObject.SetActive(false);
        successTxt.text = "";
        successNewCats.text = "";
    }
    
    /*
     * Go to the next day when the button is clicked
     */
    private void NextDayButtonClicked()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);

        if (StatsManager.instance.GetDate() > 4 && StatsManager.instance.GetLevel() == 1
            || StatsManager.instance.GetDate() > 8 && StatsManager.instance.GetLevel() == 2)
        {
            StatsManager.instance.gameOver = true;
            GameManager.instance.UpdateGameState(GameManager.GameState.GameOver);
            return;
        }
        
        // End the game if it was the last day, else go on next day 
        if (StatsManager.instance.GetDate() > StatsManager.instance.dayMax)
        {
            StatsManager.instance.gameOver = true;
            GameManager.instance.UpdateGameState(GameManager.GameState.GameOver);
        }
        
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