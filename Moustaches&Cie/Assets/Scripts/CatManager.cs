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

    public List<CatsScriptableObject.Cat> currentCats;
    public int index;
    public int indexMax;
    
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

    public Button nextCatButton;
    public Button previousCatButton;

    private void Awake()
    {
        nextCatButton.onClick.AddListener(NextCatButtonClicked);
        previousCatButton.onClick.AddListener(PreviousCatButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialiseCurrentCats(GameManager.instance.level);
        index = 0;
        indexMax = currentCats.Count;
        if(indexMax > 0)
            PrintCatInfos(currentCats[index%indexMax]);
        else 
            SceneManager.LoadSceneAsync("Score screen");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            PrintCatInfos(cats.cats[0]);
        }
        
    }

    public void PrintCatInfos(CatsScriptableObject.Cat cat)
    {
        Instantiate(cat.picture, picture.transform);
        nameTMP.text = cat.name;
        ageTMP.text = "Age : " + cat.age.ToString(CultureInfo.InvariantCulture);
        raceTMP.text = "Race : " + cat.race;
        sexTMP.text = "Sexe : " + cat.sex;
        natureTMP.text = "Caractère : " + cat.nature;
        if(cat.sick)
            sickTMP.text = "Malade : oui";
        else 
            sickTMP.text = "Malade : non";
        if(cat.outdoor)
            outdoorTMP.text = "Besoin d'extérieur : oui";
        else 
            outdoorTMP.text = "Besoin d'extérieur : non";
        if(cat.animals)
            animalsTMP.text = "OK animaux : oui";
        else 
            animalsTMP.text = "OK animaux : non";
    }

    private void InitialiseCurrentCats(GameManager.GameLevel level)
    {
        switch (level)
        {
            case GameManager.GameLevel.Level1:
                foreach (CatsScriptableObject.Cat cat in cats.cats)
                {
                    if(!cat.adopted && cat.level == 1)
                        currentCats.Add(cat);
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
    private void NextCatButtonClicked()
    {
        index++;
        if(indexMax > 0)
            PrintCatInfos(currentCats[index%indexMax]);
        else 
            SceneManager.LoadSceneAsync("Score screen");
    }

    private void PreviousCatButtonClicked()
    {
        index--;
        if(indexMax > 0)
            PrintCatInfos(currentCats[index%indexMax]);
        else 
            SceneManager.LoadSceneAsync("Score screen");
    }
}
