using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource bossaMeowa;
    public AudioSource bossaMeowaBg;

    // Singleton
    private static MusicManager _instance;
    public static MusicManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is NULL !");
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

    public void SwitchBossaNova(int index)
    {
        switch (index)
        {
            case 0:
                bossaMeowa.mute = false;
                bossaMeowaBg.mute = true;
                break;
            case 1:
                bossaMeowa.mute = true;
                bossaMeowaBg.mute = false;
                break;
        }
    }
}
