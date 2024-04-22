using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FamilyManager : MonoBehaviour
{
    // UI elements
    public TMP_Text nameTMP;
    public TMP_Text forenameTMP;
    public TMP_Text ageTMP;
    public TMP_Text jobTMP;
    public TMP_Text incomeTMP;
    public TMP_Text childTMP;
    public TMP_Text outdoorTMP;
    public TMP_Text animalsTMP;
    public TMP_Text commentTMP;
    public TMP_Text catTMP;
    
    private static TMP_Text _nameTMP;
    private static TMP_Text _forenameTMP;
    private static TMP_Text _ageTMP;
    private static TMP_Text _jobTMP;
    private static TMP_Text _incomeTMP;
    private static TMP_Text _childTMP;
    private static TMP_Text _outdoorTMP;
    private static TMP_Text _animalsTMP;
    private static TMP_Text _commentTMP;
    private static TMP_Text _catTMP;
    
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
    

    public Canvas canvas;

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

    public struct Family
    {
        public Picture Picture;
        public string Name;
        public string Forename;
        public int Age;
        public string JobName;
        public int Income;
        public int FreeTime;
        public bool Child;
        public bool Outdoor;
        public bool Animals;
        public string Comment;
        public CatsScriptableObject.Cat Cat;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        _nameTMP = nameTMP;
        _forenameTMP = forenameTMP;
        _ageTMP = ageTMP;
        _jobTMP = jobTMP;
        _incomeTMP = incomeTMP;
        _childTMP = childTMP;
        _outdoorTMP = outdoorTMP;
        _animalsTMP = animalsTMP;
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

    public static Family GenerateFamily(FamilyPictureScriptableObject familyPicture, FamilyInfosScriptableObject familyInfos, CatsScriptableObject cats)
    {
        Family family = GenerateFamilyInformations(familyInfos, cats);
        family.Picture = GenerateFamilyPicture(family, familyPicture);
        PrintFamilyPicture(family);
        PrintFamilyInformation(family);
        
        return family;
    }
    

    private static Family GenerateFamilyInformations(FamilyInfosScriptableObject familyInfos, CatsScriptableObject cats)
    {
        Family family = new Family();
        family.Picture = new Picture();
        family.Name = familyInfos.listNames[Random.Range(0, familyInfos.listNames.Count)];
        family.Forename = familyInfos.listForenames[Random.Range(0, familyInfos.listForenames.Count)];
        family.Age = familyInfos.listAges[Random.Range(0, familyInfos.listAges.Count)];
        family.Comment = familyInfos.listComments[Random.Range(0, familyInfos.listComments.Count)];

        family.Outdoor = false;
        family.Child = false;
        family.Animals = false;
        
        int i = Random.Range(0, cats.cats.Count);
        family.Cat = cats.cats[i];
        
        // If -25 years old
        if (family.Age < 25)
        {
            int p = Random.Range(1, 101);
            
            // 75% chance of being a student
            if (p > 25)
            {
                family.JobName = "Etudiant.e";
                family.Income = 400 + Random.Range(0,7)*50; // Between 400 and 600€ per month
                family.FreeTime = 1;
            }
            
            // Else they can have a child or an animal (but not both)
            else
            {
                int index = Random.Range(0, familyInfos.listJobs.Count);
                family.JobName = familyInfos.listJobs[index].jobName;
                family.Income = familyInfos.listJobs[index].income + Random.Range(0, 6) * 50;
                family.FreeTime = familyInfos.listJobs[index].freeTime;
                
                int q = Random.Range(1, 101);
                int r = Random.Range(1, 101);
                int s = Random.Range(1, 101);

                if (s > 80) // 20% chance of having a garden
                    family.Outdoor = true;
                
                if (q > 80) // 20% chance of having a child
                    family.Child = true;
                
                else if (r > 40) // 60% chance of having an animal
                    family.Animals = true;
            }
        }
        
        // If +65 years old, retired
        else if (family.Age > 65)
        {
            family.JobName = "Retraité.e";
            family.Income = 1200 + Random.Range(0, 15) * 50; // Between 1200 and 1900€ per month
            family.FreeTime = 3;
            
            int p = Random.Range(1, 101);
            int q = Random.Range(1, 101);
            
            if(p>30) // 70% chance of having a garden
                family.Outdoor = true;
            if (q > 80) // 20% chance of having an animal
                family.Animals = true;
        }
        else
        {
            int index = Random.Range(0, familyInfos.listJobs.Count);
            family.JobName = familyInfos.listJobs[index].jobName;
            family.Income = familyInfos.listJobs[index].income + Random.Range(0, 6) * 50;
            family.FreeTime = familyInfos.listJobs[index].freeTime;
            
            int q = Random.Range(1, 101);
            int r = Random.Range(1, 101);
            int s = Random.Range(1, 101);

            if (s > 60) // 40% chance of having a garden
                family.Outdoor = true;
                
            if (q > 70) // 30% chance of having a child
                family.Child = true;
                
            else if (r > 50) // 50% chance of having an animal
                family.Animals = true;
        }

        return family;
    }
    
    // Generate a face by picking random elements from the lists
    private static Picture GenerateFamilyPicture(Family family, FamilyPictureScriptableObject familyPicture)
    {
        Picture picture = new Picture();

        picture.Hair = familyPicture.listHairs[Random.Range(0, familyPicture.listHairs.Count)];
        picture.Skin = familyPicture.listSkins[Random.Range(0, familyPicture.listSkins.Count)];
        picture.Eyes = familyPicture.listEyes[Random.Range(0, familyPicture.listEyes.Count)];
        picture.Nose = familyPicture.listNoses[Random.Range(0, familyPicture.listNoses.Count)];
        picture.Mouth = familyPicture.listMouths[Random.Range(0, familyPicture.listMouths.Count)];
        picture.Eyebrows = familyPicture.listEyebrows[Random.Range(0, familyPicture.listEyebrows.Count)];
        picture.Cloth = familyPicture.listClothes[Random.Range(0, familyPicture.listClothes.Count)];
        picture.Accessories = familyPicture.listAccessories[Random.Range(0, familyPicture.listAccessories.Count)];
        if (family.Age > 65)
            picture.Wrinkles = familyPicture.listWrinkles[1];
        else 
            picture.Wrinkles = familyPicture.listWrinkles[0];
        return picture;
    }

    private static void PrintFamilyInformation(Family family)
    {
        _nameTMP.text = "Nom : " + family.Name;
        _forenameTMP.text = "Prénom : " + family.Forename;
        _ageTMP.text = "Age : " + family.Age + " ans";
        _jobTMP.text = "Profession : " + family.JobName;
        _incomeTMP.text =  "Revenus : " + family.Income + " €/mois";
        if(family.Child)
            _childTMP.text = "Enfants en bas-âge : oui";
        else
            _childTMP.text = "Enfants en bas-âge : non";
        if(family.Animals)
            _animalsTMP.text = "Autres animaux : oui";
        else
            _animalsTMP.text = "Autres animaux : non";
        if(family.Outdoor)
            _outdoorTMP.text = "Extérieur : oui";
        else 
            _outdoorTMP.text = "Extérieur : non";
        _commentTMP.text = "Commentaire : " + family.Comment;
        _catTMP.text = "Chat demandé : " + family.Cat.name;
    }
    
    // Print the face at the vector location in parameter
    private static void PrintFamilyPicture(Family family)
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
