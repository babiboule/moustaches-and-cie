using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FamilyManager : MonoBehaviour
{
    // Prefabs that compose the unique face
    private GameObject m_Hair;
    private GameObject m_Skin;
    private GameObject m_Eyes;
    private GameObject m_Nose;
    private GameObject m_Mouth;
    private GameObject m_Eyebrows;
    private GameObject m_Cloth;
    private GameObject m_Accessories;
    
    // Family information
    private string m_Name;
    private string m_Forename;
    private int m_Age;
    private string m_Job;
    private int m_Income;
    private int m_FreeTime;
    private bool m_Child;
    private bool m_Outdoor;
    private bool m_Animals;
    private string m_Comment;
    private CatsScriptableObject.Cat m_Cat;
    
    // UI elements
    public Image pictureImage;
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

    public FamilyPictureScriptableObject familyPicture;
    public FamilyInfosScriptableObject familyInfos;
    public CatsScriptableObject cats;

    public Canvas canvas;
    
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            GenerateFamilyInformations();
            GenerateFamilyPicture();
            PrintFamilyInformation();
            PrintFamilyPicture(pictureImage.transform);
        }
        
        
        if (Input.GetKeyDown("f"))
        {
            GenerateFamilyPicture();
            PrintFamilyPicture(gameObject.transform);
        }
    }
    
    // Generate a face by picking random elements from the lists
    public void GenerateFamilyPicture()
    {
        m_Hair = familyPicture.listHairs[Random.Range(0, familyPicture.listHairs.Count)];
        m_Skin = familyPicture.listSkins[Random.Range(0, familyPicture.listSkins.Count)];
        m_Eyes = familyPicture.listEyes[Random.Range(0, familyPicture.listEyes.Count)];
        m_Nose = familyPicture.listNoses[Random.Range(0, familyPicture.listNoses.Count)];
        m_Mouth = familyPicture.listMouths[Random.Range(0, familyPicture.listMouths.Count)];
        m_Eyebrows = familyPicture.listEyebrows[Random.Range(0, familyPicture.listEyebrows.Count)];
        m_Cloth = familyPicture.listClothes[Random.Range(0, familyPicture.listClothes.Count)];
        m_Accessories = familyPicture.listAccessories[Random.Range(0, familyPicture.listAccessories.Count)];
    }

    public void GenerateFamilyInformations()
    {
        m_Name = familyInfos.listNames[Random.Range(0, familyInfos.listNames.Count)];
        m_Forename = familyInfos.listForenames[Random.Range(0, familyInfos.listForenames.Count)];
        m_Age = familyInfos.listAges[Random.Range(0, familyInfos.listAges.Count)];
        m_Comment = familyInfos.listComments[Random.Range(0, familyInfos.listComments.Count)];

        m_Outdoor = false;
        m_Child = false;
        m_Animals = false;
        
        int i = Random.Range(0, cats.cats.Count);
        m_Cat = cats.cats[i];
        
        // If -25 years old
        if (m_Age < 25)
        {
            int p = Random.Range(1, 101);
            
            // 75% chance of being a student
            if (p > 25)
            {
                m_Job = "Etudiant.e";
                m_Income = 400 + Random.Range(0,7)*50; // Between 400 and 600€ per month
                m_FreeTime = 1;
            }
            
            // Else they can have a child or an animal (but not both)
            else
            {
                int index = Random.Range(0, familyInfos.listJobs.Count);
                m_Job = familyInfos.listJobs[index].jobName;
                m_Income = familyInfos.listJobs[index].income + Random.Range(0, 6) * 50;
                m_FreeTime = familyInfos.listJobs[index].freeTime;
                
                int q = Random.Range(1, 101);
                int r = Random.Range(1, 101);
                int s = Random.Range(1, 101);

                if (s > 80) // 20% chance of having a garden
                    m_Outdoor = true;
                
                if (q > 80) // 20% chance of having a child
                    m_Child = true;
                
                else if (r > 40) // 60% chance of having an animal
                    m_Animals = true;
            }
        }
        
        // If +65 years old, retired
        else if (m_Age > 65)
        {
            m_Job = "Retraité.e";
            m_Income = 1200 + Random.Range(0, 15) * 50; // Between 1200 and 1900€ per month
            m_FreeTime = 3;
            
            int p = Random.Range(1, 101);
            int q = Random.Range(1, 101);
            
            if(p>30) // 70% chance of having a garden
                m_Outdoor = true;
            if (q > 80) // 20% chance of having an animal
                m_Animals = true;
        }
        else
        {
            int index = Random.Range(0, familyInfos.listJobs.Count);
            m_Job = familyInfos.listJobs[index].jobName;
            m_Income = familyInfos.listJobs[index].income + Random.Range(0, 6) * 50;
            m_FreeTime = familyInfos.listJobs[index].freeTime;
            
            int q = Random.Range(1, 101);
            int r = Random.Range(1, 101);
            int s = Random.Range(1, 101);

            if (s > 60) // 40% chance of having a garden
                m_Outdoor = true;
                
            if (q > 70) // 30% chance of having a child
                m_Child = true;
                
            else if (r > 50) // 50% chance of having an animal
                m_Animals = true;
        }
    }
    
    // Print the face at the vector location in parameter
    public void PrintFamilyPicture(Transform location)
    {
        GameObject obj = Instantiate(m_Skin, location);
        obj.transform.SetParent(canvas.transform);
        obj = Instantiate(m_Eyes, location);
        obj.transform.SetParent(canvas.transform);
        obj = Instantiate(m_Eyebrows, location);
        obj.transform.SetParent(canvas.transform);
        obj = Instantiate(m_Nose, location);
        obj.transform.SetParent(canvas.transform);
        obj = Instantiate(m_Mouth, location);
        obj.transform.SetParent(canvas.transform);
        obj = Instantiate(m_Cloth, location);
        obj.transform.SetParent(canvas.transform);
        obj = Instantiate(m_Hair, location);
        obj.transform.SetParent(canvas.transform);
        obj = Instantiate(m_Accessories, location);
        obj.transform.SetParent(canvas.transform);
    }

    public void PrintFamilyInformation()
    {
        nameTMP.text = "Nom : " + m_Name;
        forenameTMP.text = "Prénom : " + m_Forename;
        ageTMP.text = "Age : " + m_Age + " ans";
        jobTMP.text = "Profession : " + m_Job;
        incomeTMP.text =  "Revenus : " + m_Income + " €/mois";
        if(m_Child)
            childTMP.text = "Enfants en bas-âge : oui";
        else
            childTMP.text = "Enfants en bas-âge : non";
        if(m_Animals)
            animalsTMP.text = "Autres animaux : oui";
        else
            animalsTMP.text = "Autres animaux : non";
        if(m_Outdoor)
            outdoorTMP.text = "Extérieur : oui";
        else 
            outdoorTMP.text = "Extérieur : non";
        commentTMP.text = "Commentaire : " + m_Comment;
        catTMP.text = "Chat demandé : " + m_Cat.name;
    }
}
