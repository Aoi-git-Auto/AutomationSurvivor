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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
                audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void PlayBGM(AudioClip bgm, float fadeTime)
    {
        if (audioSource.clip == bgm) return;
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeToBGM(bgm, fadeTime));
    }
    
    private IEnumerator FadeToBGM(AudioClip newClip, float fadeTime)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeTime);
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeTime);
            yield return null;
        }

        audioSource.volume = startVolume;
    }

    public void PlayTitleBGM() => PlayBGM(titleBGM, 1f);
    public void PlayinGameBGM() => PlayBGM(inGameBGM, 1f);
    public void PlayBossBGM(AudioClip bossBGM) => PlayBGM(bossBGM, 1f);
}
