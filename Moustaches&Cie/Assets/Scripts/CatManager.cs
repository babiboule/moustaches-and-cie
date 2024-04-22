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
    public CatsScriptableObject cats;
    private static CatsScriptableObject _cats;

    private static List<CatsScriptableObject.Cat> _currentCats = new List<CatsScriptableObject.Cat>();
    private static int _index;
    private static int _indexMax;
    
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


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        _cats = cats;
        
        _picture = picture;
        _nameTMP = nameTMP;
        _ageTMP = ageTMP;
        _raceTMP = raceTMP;
        _sexTMP = sexTMP;
        _sickTMP = sickTMP;
        _natureTMP = natureTMP;
        _outdoorTMP = outdoorTMP;
        _animalsTMP = animalsTMP;
        
        InitialiseCurrentCats(GameManager.instance.level);
        _index = 0;
        _indexMax = _currentCats.Count;
        if(_indexMax > 0)
            PrintCatInfos(_currentCats[_index%_indexMax]);
        else 
            SceneManager.LoadSceneAsync("Score screen");
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public static void InitialiseCurrentCats(GameManager.GameLevel level)
    {
        switch (level)
        {
            case GameManager.GameLevel.Level1:
                foreach (CatsScriptableObject.Cat cat in _cats.cats)
                {
                    if(!cat.adopted && cat.level == 1)
                        _currentCats.Add(cat);
                }
                break;
            case GameManager.GameLevel.Level2:
                foreach (CatsScriptableObject.Cat cat in _cats.cats)
                {
                    if(!cat.adopted && cat.level <= 2)
                        _currentCats.Add(cat);
                }
                break;
            case GameManager.GameLevel.Level3:
                foreach (CatsScriptableObject.Cat cat in _cats.cats)
                {
                    if(!cat.adopted && cat.level <=3)
                        _currentCats.Add(cat);
                }
                break;
            case GameManager.GameLevel.LevelMax:
                foreach (CatsScriptableObject.Cat cat in _cats.cats)
                {
                    if(!cat.adopted)
                        _currentCats.Add(cat);
                }
                break;
            default:
                Debug.Log("TRYING TO INITALISE CAT BUT NOT ON A LEVEL");
                break;
        }
    }

    public static List<CatsScriptableObject.Cat> GetCurrentCats()
    {
        return _currentCats;
    }

    public static int GetIndex()
    {
        return _index;
    }
    
    public static int GetIndexMax()
    {
        return _indexMax;
    }

    public static void NextIndex()
    {
        _index++;
    }
    
    public static void PreviousIndex()
    {
        _index--;
    }


}
