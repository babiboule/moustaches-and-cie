using UnityEngine;

public class SfxManager : MonoBehaviour
{
    // AudioSource
    [SerializeField] private AudioSource sfxObject;
    
    // Singleton
    private static SfxManager _instance;
    public static SfxManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Sfx Manager is NULL !");
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
     * Play the Param audioClip
     */
    public void PlaySfxClip(AudioClip audioClip)
    {
        // Instantiate an sfx audioSource
        var audioSource = Instantiate(sfxObject);
        DontDestroyOnLoad(audioSource.gameObject);
        
        // Set the clip on the audioSource
        audioSource.clip = audioClip;
        audioSource.volume = StatsManager.instance.GetSfxVolume();
        
        // Play the clip and destroy the source at the end
        audioSource.Play();
        var clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}