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
        GMScript.instance.inGame = true;
        GMScript.instance.currentTime = time;
        Time.timeScale = 1;
        StartCoroutine(GameStart());
    }

    public void OnRetry()
    {
        GMScript.instance.currentTime = time;
        GMScript.instance.inGame = true;
        Time.timeScale = 1;
        AudioManager.instance.PlayTitleBGM();
        SceneManager.LoadScene("GameScene");
    }

    public void OnExit()
    {
        GMScript.instance.inGame = false;
        AudioManager.instance.PlayTitleBGM();
        SceneManager.LoadScene("StartScene");
    }

    private IEnumerator GameStart()
    {
        audioSource.PlayOneShot(startSE);
        AudioManager.instance.PlayinGameBGM();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }
}
