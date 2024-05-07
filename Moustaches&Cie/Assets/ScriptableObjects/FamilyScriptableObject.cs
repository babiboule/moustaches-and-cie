using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "FamilyInfos", menuName = "ScriptableObjects/FamilyInfosScriptableObject", order = 1)]
    public class FamilyInfosScriptableObject : ScriptableObject
    {
        // Struct of a job, with the money and the free time corresponding
        [System.Serializable] public struct Job
        {
            public string jobName;
            public int freeTime;
        }
        
        [System.Serializable] public struct Home
        {
            public string city;
            public int department;
        }
        
        [System.Serializable] public enum Outdoor
        {
            Aucun,
            Ouvert,
            Fermé
        }

        [System.Serializable]
        public struct Comment
        {
            public string commentText;
            public bool problematic;
        }
        
        // Lists of characteristics from a family
        public List<string> listNames;
        public List<string> listForenames;
        public List<int> listAges;
        public List<Home> listHomes;
        public List<Job> listJobs;
        public List<Comment> listComments;
    }
}
