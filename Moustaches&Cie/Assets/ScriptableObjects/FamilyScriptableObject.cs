using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "FamilyInfos", menuName = "ScriptableObjects/FamilyInfosScriptableObject", order = 1)]
    public class FamilyInfosScriptableObject : ScriptableObject
    {
        // Struct of a job, with the money and the free time corresponding
        [System.Serializable] public struct Job
        {
            public string jobName;
            public int income;
            public int freeTime;
        }
        
        // Lists of characteristics from a family
        public List<string> listNames;
        public List<string> listForenames;
        public List<int> listAges;
        public List<Job> listJobs;
        public List<string> listComments;
    }
}
