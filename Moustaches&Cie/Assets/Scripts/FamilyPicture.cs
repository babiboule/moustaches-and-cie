using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyPicture : MonoBehaviour
{
    // Lists of Prefabs from different assets composing the face
    [SerializeField] private List<GameObject> listHairs;
    [SerializeField] private List<GameObject> listSkins;
    [SerializeField] private List<GameObject> listEyes;
    [SerializeField] private List<GameObject> listNoses;
    [SerializeField] private List<GameObject> listMouths;
    [SerializeField] private List<GameObject> listEyebrows;
    [SerializeField] private List<GameObject> listClothes;
    [SerializeField] private List<GameObject> listAccessories;

    // Prefabs that compose the unique face
    private GameObject m_Hair;
    private GameObject m_Skin;
    private GameObject m_Eyes;
    private GameObject m_Nose;
    private GameObject m_Mouth;
    private GameObject m_Eyebrows;
    private GameObject m_Cloth;
    private GameObject m_Accessories;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Generate a face by picking random elements from the lists
    public FamilyPicture GenerateFamilyPicture()
    {
        FamilyPicture newPicture = new FamilyPicture();

        newPicture.m_Hair = listHairs[Random.Range(0, listHairs.Count)];
        newPicture.m_Skin = listSkins[Random.Range(0, listSkins.Count)];
        newPicture.m_Eyes = listEyes[Random.Range(0, listEyes.Count)];
        newPicture.m_Nose = listNoses[Random.Range(0, listNoses.Count)];
        newPicture.m_Mouth = listMouths[Random.Range(0, listMouths.Count)];
        newPicture.m_Eyebrows = listEyebrows[Random.Range(0, listEyebrows.Count)];
        newPicture.m_Cloth = listClothes[Random.Range(0, listClothes.Count)];
        newPicture.m_Accessories = listAccessories[Random.Range(0, listAccessories.Count)];

        return newPicture;
    }
    
    // Print the face at the vector location in parameter
    public void PrintFamilyPicture(FamilyPicture picture, Transform location)
    {
        Instantiate(picture.m_Skin, location);
        Instantiate(picture.m_Eyes, location);
        Instantiate(picture.m_Eyebrows, location);
        Instantiate(picture.m_Nose, location);
        Instantiate(picture.m_Mouth, location);
        Instantiate(picture.m_Cloth, location);
        Instantiate(picture.m_Hair, location);
        Instantiate(picture.m_Accessories, location);
    }
}
