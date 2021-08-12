using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] soundEffects;
    [SerializeField] private AudioSource bgm, victoryMusic;

    private void Awake()
    {
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        //����Ҧ����b���񪺭��ġA����P�ɳB�z�Ӧh����
        soundEffects[soundToPlay].Stop();
        
        soundEffects[soundToPlay].Play();
    }
}
