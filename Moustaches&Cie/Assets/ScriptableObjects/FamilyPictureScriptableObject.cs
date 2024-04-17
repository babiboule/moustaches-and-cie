using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "FamilyPicture", menuName = "ScriptableObjects/FamilyPictureScriptableObject", order = 1)]
    
    public class FamilyPictureScriptableObject : ScriptableObject
    {
        // Lists of Prefabs from different assets composing the face
        public List<GameObject> listHairs;
        public List<GameObject> listSkins;
        public List<GameObject> listEyes;
        public List<GameObject> listNoses;
        public List<GameObject> listMouths;
        public List<GameObject> listEyebrows;
        public List<GameObject> listClothes;
        public List<GameObject> listAccessories;
    }
}
