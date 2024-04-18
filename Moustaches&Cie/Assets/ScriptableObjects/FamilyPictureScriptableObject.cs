using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "FamilyPicture", menuName = "ScriptableObjects/FamilyPictureScriptableObject", order = 1)]
    
    public class FamilyPictureScriptableObject : ScriptableObject
    {
        // Lists of Prefabs from different assets composing the face
        public List<Image> listHairs;
        public List<Image> listSkins;
        public List<Image> listEyes;
        public List<Image> listNoses;
        public List<Image> listMouths;
        public List<Image> listEyebrows;
        public List<Image> listClothes;
        public List<Image> listAccessories;
    }
}
