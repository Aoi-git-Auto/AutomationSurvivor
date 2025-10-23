using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private float time = 300f;
    [SerializeField]
    private AudioClip startSE;
    private AudioSource audioSource;

    [SerializeField]
    private GameObject loadingCanvas;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnGame()
    {
        audioSource.PlayOneShot(startSE);
        StartCoroutine(GameStart());
    }

    public void OnRetry()
    {
        audioSource.PlayOneShot(startSE);
        StartCoroutine(GameStart());
        GMScript.instance.currentTime = time;
        GMScript.instance.inGame = true;
        Time.timeScale = 1;
    }

    public void OnExit()
    {
        audioSource.PlayOneShot(startSE);
        StartCoroutine(GameExit());
    }

    private IEnumerator GameStart()
    {
        Instantiate(loadingCanvas);
        AsyncOperation async = SceneManager.LoadSceneAsync("GameScene");
        async.allowSceneActivation = false;

        while(async.progress < 0.9f)
        {
            yield return null;
        }

        AudioManager.instance.PlayinGameBGM();
        yield return new WaitForSecondsRealtime(1f);

        GMScript.instance.inGame = true;
        GMScript.instance.currentTime = time;
        Time.timeScale = 1;
        async.allowSceneActivation = true;
    }

    private IEnumerator GameExit()
    {
        Instantiate(loadingCanvas);
        AsyncOperation async = SceneManager.LoadSceneAsync("StartScene");
        async.allowSceneActivation = false;

        while(async.progress < 0.9f)
        {
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1f);
        
        AudioManager.instance.PlayTitleBGM();
        GMScript.instance.inGame = false;
        async.allowSceneActivation = true;
    }
}
