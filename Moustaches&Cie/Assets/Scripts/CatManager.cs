using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatManager : MonoBehaviour
{
    // UI Elements
    public Image picture;
    public TMP_Text nameTMP;
    public TMP_Text ageTMP;
    public TMP_Text raceTMP;
    public TMP_Text sexTMP;
    public TMP_Text sickTMP;
    public TMP_Text natureTMP;
    public TMP_Text outdoorTMP;
    public TMP_Text animalsTMP;
    
    private static Image _picture;
    private static TMP_Text _nameTMP;
    private static TMP_Text _ageTMP;
    private static TMP_Text _raceTMP;
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
        _raceTMP = raceTMP;
        _sexTMP = sexTMP;
        _sickTMP = sickTMP;
        _natureTMP = natureTMP;
        _outdoorTMP = outdoorTMP;
        _animalsTMP = animalsTMP;
    }
    

    public static void PrintCatInfos(CatsScriptableObject.Cat cat)
    {
        Instantiate(cat.picture, _picture.transform);
        _nameTMP.text = cat.name;
        _ageTMP.text = "Age : " + cat.age.ToString(CultureInfo.InvariantCulture);
        _raceTMP.text = "Race : " + cat.race;
        _sexTMP.text = "Sexe : " + cat.sex;
        _natureTMP.text = "Caractère : " + cat.nature;
        if(cat.sick)
            _sickTMP.text = "Malade : oui";
        else 
            _sickTMP.text = "Malade : non";
        if(cat.outdoor)
            _outdoorTMP.text = "Besoin d'extérieur : oui";
        else 
            _outdoorTMP.text = "Besoin d'extérieur : non";
        if(cat.animals)
            _animalsTMP.text = "OK animaux : oui";
        else 
            _animalsTMP.text = "OK animaux : non";
    }

    public static void InitialiseCurrentCats(GameManager.GameLevel level, CatsScriptableObject cats, List<CatsScriptableObject.Cat> currentCats)
    {
        bool adopted = false;
        switch (level)
        {
            case GameManager.GameLevel.Level1:
                foreach (CatsScriptableObject.Cat cat in cats.cats)
                {
                    adopted = false;
                    if(cat.level == 1)
                        if (StatsManager.AdoptedCats.Count > 0)
                        {
                            foreach (CatsScriptableObject.Cat adoptedCat in StatsManager.AdoptedCats)
                            {
                                if (adoptedCat.name == cat.name)
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
            case GameManager.GameLevel.Level2:
                foreach (CatsScriptableObject.Cat cat in cats.cats)
                {
                    if(!cat.adopted && cat.level <= 2)
                        currentCats.Add(cat);
                }
                break;
            case GameManager.GameLevel.Level3:
                foreach (CatsScriptableObject.Cat cat in cats.cats)
                {
                    if(!cat.adopted && cat.level <=3)
                        currentCats.Add(cat);
                }
                break;
            case GameManager.GameLevel.LevelMax:
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
