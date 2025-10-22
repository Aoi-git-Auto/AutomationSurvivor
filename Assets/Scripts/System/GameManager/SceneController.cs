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
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnGame()
    {
        StartCoroutine(GameStart());
        GMScript.instance.inGame = true;
        GMScript.instance.currentTime = time;
        Time.timeScale = 1;
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
        GMScript.instance.inGame = false;
        audioSource.PlayOneShot(startSE);
        StartCoroutine(GameExit());
    }

    private IEnumerator GameStart()
    {
        audioSource.PlayOneShot(startSE);
        AudioManager.instance.PlayinGameBGM();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }

    private IEnumerator GameExit()
    {
        audioSource.PlayOneShot(startSE);
        yield return new WaitForSecondsRealtime(1f);
        AudioManager.instance.PlayTitleBGM();
        AsyncOperation async = SceneManager.LoadSceneAsync("StartScene");
        while (!async.isDone)
        {
            Debug.Log("loading");
            yield return null;

        }    
    }
}
