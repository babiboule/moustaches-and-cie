using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CatsInfos", menuName = "ScriptableObjects/CatsScriptableObject", order = 1)]
    public class CatsScriptableObject : ScriptableObject
    {
        public enum Nature
        {
            Jovial,
            Peureux,
            Agressif
        }

        public enum Sex
        {
            M,
            F
        }
        [System.Serializable] public struct Cat
        {
            public GameObject picture;
            public string name;
            public float age;
            public string race;
            public Sex sex;
            public bool sick;
            public Nature nature;
            public bool outdoor;
            public bool animals;
            public bool adopted;
            public int level;
        }

        public List<Cat> cats;
    }
}
