using System;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FamilyManager : MonoBehaviour
{
    // UI elements
    public TMP_Text nameTMP;
    public TMP_Text forenameTMP;
    public TMP_Text ageTMP;
    public TMP_Text homeTMP;
    public TMP_Text jobTMP;
    public TMP_Text budgetTMP;
    public TMP_Text childTMP;
    public TMP_Text outdoorTMP;
    public TMP_Text animalsTMP;
    public TMP_Text commentTMP;
    public TMP_Text catTMP;
    
    private static TMP_Text _nameTMP;
    private static TMP_Text _forenameTMP;
    private static TMP_Text _ageTMP;
    private static TMP_Text _homeTMP;
    private static TMP_Text _jobTMP;
    private static TMP_Text _budgetTMP;
    private static TMP_Text _childTMP;
    private static TMP_Text _outdoorTMP;
    private static TMP_Text _catsTMP;
    private static TMP_Text _commentTMP;
    private static TMP_Text _catTMP;
    
    // Profile picture
    public Image hair;
    public Image skin;
    public Image eyes;
    public Image nose;
    public Image mouth;
    public Image eyebrows;
    public Image cloth;
    public Image accessories;
    public Image wrinkles;
    
    private static Image _hair;
    private static Image _skin;
    private static Image _eyes;
    private static Image _nose;
    private static Image _mouth;
    private static Image _eyebrows;
    private static Image _cloth;
    private static Image _accessories;
    private static Image _wrinkles;

    // Struct to form a picture with different face elements
    public struct Picture
    {
        public Sprite Hair;
        public Sprite Skin;
        public Sprite Eyes;
        public Sprite Nose;
        public Sprite Mouth;
        public Sprite Eyebrows;
        public Sprite Cloth;
        public Sprite Accessories;
        public Sprite Wrinkles;
    }

    // Struct to create a family with different informations
    public struct Family
    {
        public Picture Picture;
        public string Name;
        public string Forename;
        public int Age;
        public bool Guarantor;
        public FamilyInfosScriptableObject.Home Home;
        public bool Car;
        public string JobName;
        public int Budget;
        public int FreeTime;
        public bool Child;
        public FamilyInfosScriptableObject.Outdoor Outdoor;
        public bool Cats;
        public FamilyInfosScriptableObject.Comment Comment;
        public CatsScriptableObject.Cat Cat;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        // Assign correspondences
        _nameTMP = nameTMP;
        _forenameTMP = forenameTMP;
        _ageTMP = ageTMP;
        _homeTMP = homeTMP;
        _jobTMP = jobTMP;
        _budgetTMP = budgetTMP;
        _childTMP = childTMP;
        _outdoorTMP = outdoorTMP;
        _catsTMP = animalsTMP;
        _commentTMP = commentTMP;
        _catTMP = catTMP;

        _hair = hair;
        _hair.sprite = hair.sprite;
        _skin = skin;
        _skin.sprite = skin.sprite;
        _eyes = eyes;
        _nose = nose;
        _mouth = mouth;
        _eyebrows = eyebrows;
        _cloth = cloth;
        _accessories = accessories;
        _wrinkles = wrinkles;
    }

    /*
     * Generate and return a neutral family with a random picture
     * Param familyPicture : Scriptable object with all the sprites for the picture
     * Param familyInfos : Scriptable object with all the different informations to generate
     * Param cats : list of the cats currently available to the adoption
     */
    public static Family GenerateFamily(FamilyPictureScriptableObject familyPicture, FamilyInfosScriptableObject familyInfos, List<CatsScriptableObject.Cat> cats)
    {
        var family = GenerateFamilyNeutralInformations(familyInfos, cats);
        family.Picture = GenerateFamilyPicture(family, familyPicture);
        
        return family;
    }

    /*
     * Print the Param family picture and infos
     */
    public static void PrintFamily(Family family)
    {
        PrintFamilyPicture(family);
        PrintFamilyInformation(family);   
    }
    
    /*
     * Generate and return a family with neutral informations (can adopt any cat)
     * Param familyInfos : Scriptable object with all the different informations to generate
     * Param cats : list of the cats currently available to the adoption
     */
    private static Family GenerateFamilyNeutralInformations(FamilyInfosScriptableObject familyInfos, List<CatsScriptableObject.Cat> cats)
    {
        // Set random name, forename and age, then set default values
        Family family = new()
        {
            Picture = new Picture(),
            Name = familyInfos.listNames[Random.Range(0, familyInfos.listNames.Count)],
            Forename = familyInfos.listForenames[Random.Range(0, familyInfos.listForenames.Count)],
            Age = familyInfos.listAges[Random.Range(0, familyInfos.listAges.Count)],
            Home = familyInfos.listHomes[0], // Strasbourg 67
            Car = true,
            Comment = familyInfos.listComments[Random.Range(0, familyInfos.listComments.Count)],
            Outdoor = FamilyInfosScriptableObject.Outdoor.Fermé,
            Budget = 300,
            Child = false,
            Cats = false,
            Guarantor = true
        };

        // Regenerate a comment until it is not problematic
        while (family.Comment.problematic)
        {
            family.Comment = familyInfos.listComments[Random.Range(0, familyInfos.listComments.Count)];
        }
        
        // Assign a random available cat
        var i = Random.Range(0, cats.Count);
        family.Cat = cats[i];

        family.Budget = family.Cat.sick switch
        {
            CatsScriptableObject.Sick.Aucune => Random.Range(7, 13) * 10,
            CatsScriptableObject.Sick.Coriza => Random.Range(8, 14) * 10,
            CatsScriptableObject.Sick.FIV => Random.Range(13, 20) * 10,
            CatsScriptableObject.Sick.PIF => Random.Range(22, 30) * 10,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (family.Cat.disability)
            family.Outdoor = FamilyInfosScriptableObject.Outdoor.Aucun;

        // Assign a job depending on the age 
        switch (family.Age)
        {
            // If -25 years old
            case < 25:
            {
                var p = Random.Range(1, 101);
            
                // 75% chance of being a student
                if (p > 25)
                {
                    family.JobName = "Etudiant.e";
                    family.FreeTime = 1;
                }
            
                // Else they have a random job
                else
                {
                    var index = Random.Range(0, familyInfos.listJobs.Count);
                    family.JobName = familyInfos.listJobs[index].jobName;
                    family.FreeTime = familyInfos.listJobs[index].freeTime;
                }

                break;
            }
            
            // If +65 years old, retired
            case > 65:
                family.JobName = "Retraité.e";
                family.FreeTime = 3;
                break;
            
            // Else they have a random job
            default:
            {
                var index = Random.Range(0, familyInfos.listJobs.Count);
                family.JobName = familyInfos.listJobs[index].jobName;
                family.FreeTime = familyInfos.listJobs[index].freeTime;
                break;
            }
        }
        return family;
    }

    /*
     * Create a constraint to a Family and return it
     * Param family : Family to add a constraint on
     * Param familyInfos : Scriptable object with all the different informations to generate
     */
    public static Family AddConstraint(Family family, FamilyInfosScriptableObject familyInfos)
    {
        var constraint = Random.Range(1, StatsManager.instance.GetLevel()<3 ? 7 : 10);
        
        switch (constraint)
        {
            case 1: // Add a child
                if (family.Child)
                    AddConstraint(family, familyInfos);
                else
                    family.Child = true;
                break;
            
            case 2: // Add a cat
                if (family.Cats)
                    AddConstraint(family, familyInfos);
                else
                    family.Cats = true;
                break;
            
            case 3: // Change the comment
                while (!family.Comment.problematic)
                {
                    family.Comment = familyInfos.listComments[Random.Range(0, familyInfos.listComments.Count)];
                }
                break;
            
            case 4: // Change home
                family.Home = familyInfos.listHomes[Random.Range(0, familyInfos.listHomes.Count)];
                break;
            
            case 5 : // Reduce budget
                if (family.Budget <= 50)
                    break;
                family.Budget -= 40;
                break;
            
            case 6 : // Change outdoor conditions
                var i = Random.Range(0, 3);
                family.Outdoor = i switch
                {
                    0 => FamilyInfosScriptableObject.Outdoor.Aucun,
                    1 => FamilyInfosScriptableObject.Outdoor.Fermé,
                    2 => FamilyInfosScriptableObject.Outdoor.Ouvert,
                    _ => FamilyInfosScriptableObject.Outdoor.Fermé
                };
                break;
            
            /*************** Level 3 and more *******************/
            
            case 7 : // Remove Teleworking if possible
                if (family.FreeTime is 1 or 3)
                    AddConstraint(family, familyInfos);
                else
                    family.FreeTime = 1;
                break;
            
            case 8 : // Remove Guarantor
                if (!family.Guarantor)
                    AddConstraint(family, familyInfos);
                else
                    family.Guarantor = false;
                break;
            
            case 9 : //Remove car
                if (!family.Car)
                    AddConstraint(family, familyInfos);
                else
                    family.Car = false;
                break;
        }
        return family;
    }
    
    /*
     * Generate and return a face by picking random elements from the lists
     * Param family : Family to set the picture on (for age)
     * Param familyPicture : Scriptable object to pick random elements from
     */
    public static Picture GenerateFamilyPicture(Family family, FamilyPictureScriptableObject familyPicture)
    {
        Picture picture = new()
        {
            Hair = familyPicture.listHairs[Random.Range(0, familyPicture.listHairs.Count)],
            Skin = familyPicture.listSkins[Random.Range(0, familyPicture.listSkins.Count)],
            Eyes = familyPicture.listEyes[Random.Range(0, familyPicture.listEyes.Count)],
            Nose = familyPicture.listNoses[Random.Range(0, familyPicture.listNoses.Count)],
            Mouth = familyPicture.listMouths[Random.Range(0, familyPicture.listMouths.Count)],
            Eyebrows = familyPicture.listEyebrows[Random.Range(0, familyPicture.listEyebrows.Count)],
            Cloth = familyPicture.listClothes[Random.Range(0, familyPicture.listClothes.Count)],
            Accessories = familyPicture.listAccessories[Random.Range(0, familyPicture.listAccessories.Count)],
            // Add wirnkles if the person is retired
            Wrinkles = family.Age > 65 ? familyPicture.listWrinkles[1] : familyPicture.listWrinkles[0]
        };

        return picture;
    }

    /*
     * Print the Param family informations
     */
    private static void PrintFamilyInformation(Family family)
    {
        _nameTMP.text = family.Name;
        _forenameTMP.text = family.Forename;
        _ageTMP.text = family.Age + " ans";
        _homeTMP.text = family.Home.city + " (" + family.Home.department + ")";
        _jobTMP.text = family.JobName;
        _budgetTMP.text =  family.Budget + " €/mois";
        _childTMP.text = family.Child ? "Oui" : "Non";
        _catsTMP.text = family.Cats ? "Oui" : "Non";
        _outdoorTMP.text = family.Outdoor.ToString();
        _commentTMP.text = family.Comment.commentText;
        _catTMP.text = family.Cat.name;
    }
    
    /*
     * Print the Param family picture
     */
    public static void PrintFamilyPicture(Family family)
    {
        _skin.sprite = family.Picture.Skin;
        _hair.sprite = family.Picture.Hair;
        _eyes.sprite = family.Picture.Eyes;
        _eyebrows.sprite = family.Picture.Eyebrows;
        _nose.sprite = family.Picture.Nose;
        _mouth.sprite = family.Picture.Mouth;
        _cloth.sprite = family.Picture.Cloth;
        _accessories.sprite = family.Picture.Accessories;
        _wrinkles.sprite = family.Picture.Wrinkles;
    }
}