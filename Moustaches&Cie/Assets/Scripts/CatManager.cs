using System;
using System.Collections.Generic;
using System.Globalization;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatManager : MonoBehaviour
{
    // UI Elements
    public Image picture;
    public TMP_Text nameTMP;
    public TMP_Text ageTMP;
    public TMP_Text furTMP;
    public TMP_Text sexTMP;
    public TMP_Text sickTMP;
    public TMP_Text natureTMP;
    public TMP_Text outdoorTMP;
    public TMP_Text animalsTMP;
    
    private static Image _picture;
    private static TMP_Text _nameTMP;
    private static TMP_Text _ageTMP;
    private static TMP_Text _furTMP;
    private static TMP_Text _sexTMP;
    private static TMP_Text _sickTMP;
    private static TMP_Text _natureTMP;
    private static TMP_Text _outdoorTMP;
    private static TMP_Text _animalsTMP;
    

    // Start is called before the first frame update
    void Awake()
    {
        _picture = picture;
        _nameTMP = nameTMP;
        _ageTMP = ageTMP;
        _furTMP = furTMP;
        _sexTMP = sexTMP;
        _sickTMP = sickTMP;
        _natureTMP = natureTMP;
        _outdoorTMP = outdoorTMP;
        _animalsTMP = animalsTMP;
    }
    

    public static void PrintCatInfos(CatsScriptableObject.Cat cat)
    {
        _picture.sprite = cat.picture;
        _nameTMP.text = cat.name;
        if (cat.age<1)
        {
            _ageTMP.text = (cat.age*10+2).ToString(CultureInfo.InvariantCulture) + " mois";
        }
        else
        {
            _ageTMP.text = cat.age.ToString(CultureInfo.InvariantCulture) + " ans";
        }
        _furTMP.text = cat.fur;
        _sexTMP.text = cat.sex.ToString();
        _natureTMP.text = cat.nature.ToString();
        if(cat.sick)
            _sickTMP.text = "Oui";
        else 
            _sickTMP.text = "Non";
        if(cat.outdoor)
            _outdoorTMP.text = "Oui";
        else 
            _outdoorTMP.text = "Non";
        if(cat.animals)
            _animalsTMP.text = "Oui";
        else 
            _animalsTMP.text = "Non";
    }

    public static void InitialiseCurrentCats(int level, CatsScriptableObject cats, List<CatsScriptableObject.Cat> currentCats)
    {
        switch (level)
        {
            case 1:
                foreach (CatsScriptableObject.Cat cat in cats.cats)
                {
                    var adopted = false;
                    if(cat.level == 1)
                        if (StatsManager.instance.GetAdoptedCats().Count > 0)
                        {
                            foreach (String adoptedCat in StatsManager.instance.GetAdoptedCats())
                            {
                                if (adoptedCat == cat.name)
                                {
                                    adopted = true;
                                }
                            }

                            if (!adopted)
                            {
                                currentCats.Add(cat);
                            }
                        }
                        else currentCats.Add(cat);
                }
                break;
            case 2:
                foreach (CatsScriptableObject.Cat cat in cats.cats)
                {
                    if(!cat.adopted && cat.level <= 2)
                        currentCats.Add(cat);
                }
                break;
            case 3:
                foreach (CatsScriptableObject.Cat cat in cats.cats)
                {
                    if(!cat.adopted && cat.level <=3)
                        currentCats.Add(cat);
                }
                break;
            case 4:
                foreach (CatsScriptableObject.Cat cat in cats.cats)
                {
                    if(!cat.adopted)
                        currentCats.Add(cat);
                }
                break;
            default:
                Debug.Log("TRYING TO INITALISE CAT BUT NOT ON A LEVEL");
                break;
        }
    }


}
