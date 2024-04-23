using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class StatsManager : MonoBehaviour
{

    public static List<CatsScriptableObject.Cat> AdoptedCats = new List<CatsScriptableObject.Cat>();
    
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
