using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioClip titleBGM;
    [SerializeField]
    private AudioClip inGameBGM;
    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayBGM(AudioClip bgm)
    {
        audioSource.Stop();
        audioSource.clip = bgm;
        audioSource.Play();
    }

    public void PlayTitleBGM() => PlayBGM(titleBGM);
    public void PlayinGameBGM() => PlayBGM(inGameBGM);
    public void PlayBossBGM(AudioClip bossBGM) => PlayBGM(bossBGM);
}
