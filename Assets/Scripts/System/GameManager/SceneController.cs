using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private float time = 300f;
    [SerializeField]
    private AudioClip startSE;
    private AudioSource audioSource;
    private Slider progressBar;
    private float displayProgress = 0f;

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
    }

    public void OnExit()
    {
        audioSource.PlayOneShot(startSE);
        StartCoroutine(GameExit());
    }

    private IEnumerator GameStart()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("GameScene");
        var loading = Instantiate(loadingCanvas);
        progressBar = loading.GetComponentInChildren<Slider>();
        async.allowSceneActivation = false;

        displayProgress = 0f;

        while (displayProgress < 1.1f)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            progressBar.value = displayProgress;
            displayProgress += 0.1f;
        }
        
        yield return new WaitForSecondsRealtime(1f);

        AudioManager.instance.PlayinGameBGM();
        GMScript.instance.inGame = true;
        GMScript.instance.currentTime = time;
        Time.timeScale = 1;
        async.allowSceneActivation = true;
    }

    private IEnumerator GameExit()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("StartScene");
        var loading = Instantiate(loadingCanvas);
        progressBar = loading.GetComponentInChildren<Slider>();
        async.allowSceneActivation = false;

        displayProgress = 0f;

        while (displayProgress < 1.1f)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            progressBar.value = displayProgress;
            displayProgress += 0.1f;
        }

        yield return new WaitForSecondsRealtime(1f);

        AudioManager.instance.PlayTitleBGM();
        GMScript.instance.inGame = false;
        async.allowSceneActivation = true;
    }
}
