using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
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

    public void PlaySfxClip(AudioClip audioClip)
    {
        AudioSource audioSource = Instantiate(sfxObject);
        DontDestroyOnLoad(audioSource.gameObject);
        audioSource.clip = audioClip;
        audioSource.volume = StatsManager.instance.GetSfxVolume();
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
    
    
}
