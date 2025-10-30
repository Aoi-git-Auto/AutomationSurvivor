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
    private GameObject pausePanel;
    private bool inPause;

    [SerializeField]
    private GameObject loadingCanvas;
    [SerializeField]
    private AudioClip pauseSE;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            inPause = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        if (!inPause)
        {
            OpenPause();
        }
        else
        {
            ClosePause();
        }
    }

    private void OpenPause()
    {
        if (pausePanel != null && GMScript.instance.inGame)
        {
            audioSource.PlayOneShot(pauseSE);
            inPause = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }

    public void ClosePause()
    {
        if (pausePanel != null && GMScript.instance.inGame)
        {
            audioSource.PlayOneShot(startSE);
            inPause = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
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
        GMScript.instance.bossArriving = false;
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
