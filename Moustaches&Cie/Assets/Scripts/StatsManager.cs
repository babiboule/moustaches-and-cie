using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Daily stats
    public int GoodAdoptions;
    public int BadAdoptions;
    public List<LogicManager.Problem> ListProblems = new List<LogicManager.Problem>();
    
    // Adopted cats
    public List<string> AdoptedCats = new List<string>();
    
    // General stats
    public int level = 1;
    public int exp = 0;
    
    // Singleton
    private static StatsManager _instance;
    public static StatsManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Stats Manager is NULL !");
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(_instance);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
