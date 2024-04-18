using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatManager : MonoBehaviour
{
    public CatsScriptableObject cats;
    
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
    
    
    // Start is called before the first frame update
    void Start()
    {
        
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
}
