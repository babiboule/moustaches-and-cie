using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "FamilyPicture", menuName = "ScriptableObjects/FamilyPictureScriptableObject", order = 1)]
    
    public class FamilyPictureScriptableObject : ScriptableObject
    {
        // Lists of Prefabs from different assets composing the face
        public List<Sprite> listHairs;
        public List<Sprite> listSkins;
        public List<Sprite> listEyes;
        public List<Sprite> listNoses;
        public List<Sprite> listMouths;
        public List<Sprite> listEyebrows;
        public List<Sprite> listClothes;
        public List<Sprite> listAccessories;
        public List<Sprite> listWrinkles;
    }
}
