using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

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

    public FamilyPictureScriptableObject familyPicture;

    public Canvas canvas;
    
    
    // Start is called before the first frame update
    void Start()
    {
       GenerateFamilyPicture();
       PrintFamilyPicture(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
