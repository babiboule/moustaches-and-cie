using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CatsInfos", menuName = "ScriptableObjects/CatsScriptableObject", order = 1)]
    public class CatsScriptableObject : ScriptableObject
    {
        public enum Nature
        {
            Amical,
            Indépendant,
            Peureux,
            Collant
        }

        public enum Sex
        {
            M,
            F
        }

        public enum Sick
        {
            Aucune,
            Coriza,
            FIV,
            PIF
        }

        public enum Animals
        {
            Ok,
            Non,
            Besoin
        }
        
        [System.Serializable] public struct Cat
        {
            public Sprite picture;
            public Sprite reference;
            public string name;
            public float age;
            public string fur;
            public Sex sex;
            public Sick sick;
            public bool disability;
            public Nature nature;
            public bool outdoor;
            public Animals animals;
            public bool adopted;
            public int level;
        }

        public List<Cat> cats;
    }
}
