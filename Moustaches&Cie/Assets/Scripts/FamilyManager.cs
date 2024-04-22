using ScriptableObjects;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FamilyManager : MonoBehaviour
{
    // Prefabs that compose the unique face
    private static Sprite _hairSprite;
    private static Sprite _skinSprite;
    private static Sprite _eyesSprite;
    private static Sprite _noseSprite;
    private static Sprite _mouthSprite;
    private static Sprite _eyebrowsSprite;
    private static Sprite _clothSprite;
    private static Sprite _accessoriesSprite;
    private static Sprite _wrinklesSprite;
    
    // Family information
    private static string _name;
    private static string _forename;
    private static int _age;
    private static string _job;
    private static int _income;
    private static int _freeTime;
    private static bool _child;
    private static bool _outdoor;
    private static bool _animals;
    private static string _comment;
    private static CatsScriptableObject.Cat _cat;
    
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

    public FamilyPictureScriptableObject familyPicture;
    public FamilyInfosScriptableObject familyInfos;
    public CatsScriptableObject cats;

    private static FamilyPictureScriptableObject _familyPicture;
    private static FamilyInfosScriptableObject _familyInfos;
    private static CatsScriptableObject _cats;
    

    public Canvas canvas;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _familyPicture = familyPicture;
        _familyInfos = familyInfos;
        _cats = cats;

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
        _skin = skin;
        _eyes = eyes;
        _nose = nose;
        _mouth = mouth;
        _eyebrows = eyebrows;
        _cloth = cloth;
        _accessories = accessories;
        _wrinkles = wrinkles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            GenerateFamilyInformations();
            GenerateFamilyPicture();
            PrintFamilyInformation();
            PrintFamilyPicture();
        }
        
        
        if (Input.GetKeyDown("f"))
        {
            GenerateFamilyPicture();
            PrintFamilyPicture();
        }
    }

    public static void GenerateFamily()
    {
        GenerateFamilyInformations();
        GenerateFamilyPicture();
        PrintFamilyInformation();
        PrintFamilyPicture();
    }
    
    
    // Generate a face by picking random elements from the lists
    private static void GenerateFamilyPicture()
    {
        _hairSprite = _familyPicture.listHairs[Random.Range(0, _familyPicture.listHairs.Count)];
        _skinSprite = _familyPicture.listSkins[Random.Range(0, _familyPicture.listSkins.Count)];
        _eyesSprite = _familyPicture.listEyes[Random.Range(0, _familyPicture.listEyes.Count)];
        _noseSprite = _familyPicture.listNoses[Random.Range(0, _familyPicture.listNoses.Count)];
        _mouthSprite = _familyPicture.listMouths[Random.Range(0, _familyPicture.listMouths.Count)];
        _eyebrowsSprite = _familyPicture.listEyebrows[Random.Range(0, _familyPicture.listEyebrows.Count)];
        _clothSprite = _familyPicture.listClothes[Random.Range(0, _familyPicture.listClothes.Count)];
        _accessoriesSprite = _familyPicture.listAccessories[Random.Range(0, _familyPicture.listAccessories.Count)];
        if (_age > 65)
            _wrinklesSprite = _familyPicture.listWrinkles[1];
        else 
            _wrinklesSprite = _familyPicture.listWrinkles[0];
    }

    private static void GenerateFamilyInformations()
    {
        _name = _familyInfos.listNames[Random.Range(0, _familyInfos.listNames.Count)];
        _forename = _familyInfos.listForenames[Random.Range(0, _familyInfos.listForenames.Count)];
        _age = _familyInfos.listAges[Random.Range(0, _familyInfos.listAges.Count)];
        _comment = _familyInfos.listComments[Random.Range(0, _familyInfos.listComments.Count)];

        _outdoor = false;
        _child = false;
        _animals = false;
        
        int i = Random.Range(0, _cats.cats.Count);
        _cat = _cats.cats[i];
        
        // If -25 years old
        if (_age < 25)
        {
            int p = Random.Range(1, 101);
            
            // 75% chance of being a student
            if (p > 25)
            {
                _job = "Etudiant.e";
                _income = 400 + Random.Range(0,7)*50; // Between 400 and 600€ per month
                _freeTime = 1;
            }
            
            // Else they can have a child or an animal (but not both)
            else
            {
                int index = Random.Range(0, _familyInfos.listJobs.Count);
                _job = _familyInfos.listJobs[index].jobName;
                _income = _familyInfos.listJobs[index].income + Random.Range(0, 6) * 50;
                _freeTime = _familyInfos.listJobs[index].freeTime;
                
                int q = Random.Range(1, 101);
                int r = Random.Range(1, 101);
                int s = Random.Range(1, 101);

                if (s > 80) // 20% chance of having a garden
                    _outdoor = true;
                
                if (q > 80) // 20% chance of having a child
                    _child = true;
                
                else if (r > 40) // 60% chance of having an animal
                    _animals = true;
            }
        }
        
        // If +65 years old, retired
        else if (_age > 65)
        {
            _job = "Retraité.e";
            _income = 1200 + Random.Range(0, 15) * 50; // Between 1200 and 1900€ per month
            _freeTime = 3;
            
            int p = Random.Range(1, 101);
            int q = Random.Range(1, 101);
            
            if(p>30) // 70% chance of having a garden
                _outdoor = true;
            if (q > 80) // 20% chance of having an animal
                _animals = true;
        }
        else
        {
            int index = Random.Range(0, _familyInfos.listJobs.Count);
            _job = _familyInfos.listJobs[index].jobName;
            _income = _familyInfos.listJobs[index].income + Random.Range(0, 6) * 50;
            _freeTime = _familyInfos.listJobs[index].freeTime;
            
            int q = Random.Range(1, 101);
            int r = Random.Range(1, 101);
            int s = Random.Range(1, 101);

            if (s > 60) // 40% chance of having a garden
                _outdoor = true;
                
            if (q > 70) // 30% chance of having a child
                _child = true;
                
            else if (r > 50) // 50% chance of having an animal
                _animals = true;
        }
    }
    
    // Print the face at the vector location in parameter
    private static void PrintFamilyPicture()
    {
        _skin.sprite = _skinSprite;
        _hair.sprite = _hairSprite;
        _eyes.sprite = _eyesSprite;
        _eyebrows.sprite = _eyebrowsSprite;
        _nose.sprite = _noseSprite;
        _mouth.sprite = _mouthSprite;
        _cloth.sprite = _clothSprite;
        _accessories.sprite = _accessoriesSprite;
        _wrinkles.sprite = _wrinklesSprite;
    }

    private static void PrintFamilyInformation()
    {
        _nameTMP.text = "Nom : " + _name;
        _forenameTMP.text = "Prénom : " + _forename;
        _ageTMP.text = "Age : " + _age + " ans";
        _jobTMP.text = "Profession : " + _job;
        _incomeTMP.text =  "Revenus : " + _income + " €/mois";
        if(_child)
            _childTMP.text = "Enfants en bas-âge : oui";
        else
            _childTMP.text = "Enfants en bas-âge : non";
        if(_animals)
            _animalsTMP.text = "Autres animaux : oui";
        else
            _animalsTMP.text = "Autres animaux : non";
        if(_outdoor)
            _outdoorTMP.text = "Extérieur : oui";
        else 
            _outdoorTMP.text = "Extérieur : non";
        _commentTMP.text = "Commentaire : " + _comment;
        _catTMP.text = "Chat demandé : " + _cat.name;
    }
}
