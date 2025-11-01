using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    private PlayerInput playerInput;
    [SerializeField]
    private GameObject pauseFirstButton;

    [SerializeField]
    private GameObject loadingCanvas;
    [SerializeField]
    private AudioClip pauseSE;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            inPause = false;
        }
    }

    public void Pause()
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
            if(playerInput != null)
            {
                playerInput.SwitchCurrentActionMap("UI");
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            }
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
            if(playerInput != null)
            {
                playerInput.SwitchCurrentActionMap("Player");
            }
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
        playerInput.SwitchCurrentActionMap("Player");
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
        Time.timeScale = 1;
        async.allowSceneActivation = true;
    }
}
