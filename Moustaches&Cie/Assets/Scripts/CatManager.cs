using System.Collections.Generic;
using System.Globalization;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatManager : MonoBehaviour
{
    // UI Elements
    [SerializeField] private Image picture;
    [SerializeField] private TMP_Text nameTMP;
    [SerializeField] private TMP_Text ageTMP;
    [SerializeField] private TMP_Text furTMP;
    [SerializeField] private TMP_Text sexTMP;
    [SerializeField] private TMP_Text sickTMP;
    [SerializeField] private TMP_Text disabilityTMP;
    [SerializeField] private TMP_Text natureTMP;
    [SerializeField] private TMP_Text outdoorTMP;
    [SerializeField] private TMP_Text animalsTMP;
    
    // Private static correspondences 
    private static Image _picture;
    private static TMP_Text _nameTMP;
    private static TMP_Text _ageTMP;
    private static TMP_Text _furTMP;
    private static TMP_Text _sexTMP;
    private static TMP_Text _sickTMP;
    private static TMP_Text _disabilityTMP;
    private static TMP_Text _natureTMP;
    private static TMP_Text _outdoorTMP;
    private static TMP_Text _animalsTMP;
    

    // Start is called before the first frame update
    void Awake()
    {
        // Match the correspondences
        _picture = picture;
        _nameTMP = nameTMP;
        _ageTMP = ageTMP;
        _furTMP = furTMP;
        _sexTMP = sexTMP;
        _sickTMP = sickTMP;
        _disabilityTMP = disabilityTMP;
        _natureTMP = natureTMP;
        _outdoorTMP = outdoorTMP;
        _animalsTMP = animalsTMP;
    }
    

    // Print the current cat informations on the screen
    public static void PrintCatInfos(CatsScriptableObject.Cat cat)
    {
        _picture.sprite = cat.picture;
        _nameTMP.text = cat.name;
        
        /* print cats age in month if it's under 1 year (+2 to make it between 2 and 11 month)
                                                            ex : 0.8y = 10 month */
        if (cat.age<1)
            _ageTMP.text = (cat.age*10+2).ToString(CultureInfo.InvariantCulture) + " mois";
        else
            _ageTMP.text = cat.age.ToString(CultureInfo.InvariantCulture) + " ans";
        
        _furTMP.text = cat.fur;
        _sexTMP.text = cat.sex.ToString();
        _natureTMP.text = cat.nature.ToString();
        _sickTMP.text = cat.sick.ToString();
        _disabilityTMP.text = cat.disability ? "Oui" : "Non";
        _outdoorTMP.text = cat.outdoor ? "Oui" : "Non";
        _animalsTMP.text = cat.animals ? "Oui" : "Non";
    }

    /*
     * Set the current list of cats available, depending on the level and the cats already adopted
     * param level : level of the stage
     * param cats : scriptable objects where all the cats are stocked
     * param currentCats : the list which will contains the current cats
     */
    public static void InitialiseCurrentCats(int level, CatsScriptableObject cats, List<CatsScriptableObject.Cat> currentCats)
    {
        foreach (var cat in cats.cats)
        {
            var adopted = false;
            if(cat.level == level)
                if (StatsManager.instance.GetAdoptedCats().Count > 0)
                {
                    foreach (var adoptedCat in StatsManager.instance.GetAdoptedCats())
                    {
                        if (adoptedCat == cat.name)
                            adopted = true;
                    }

                    if (!adopted)
                        currentCats.Add(cat);
                }
                else currentCats.Add(cat);
        }
    }
}