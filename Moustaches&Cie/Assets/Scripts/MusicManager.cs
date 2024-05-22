using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Musics
    public AudioSource bossaMeowa;
    public AudioSource bossaMeowaBg;

    // Singleton
    private static MusicManager _instance;
    public static MusicManager instance
    {
        get
        {
            if (_instance is null)
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

    /*
     * Switch the music to the version with fx if Param a = true
     */
    public void BgBossaNova(bool a)
    {
        bossaMeowa.mute = a;
        bossaMeowaBg.mute = !a;
    }
}