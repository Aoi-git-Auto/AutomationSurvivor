using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private float time = 300f;
    public void OnGame()
    {
        GMScript.instance.inGame = true;
        GMScript.instance.currentTime = time;
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void OnRetry()
    {
        GMScript.instance.currentTime = time;
        GMScript.instance.inGame = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void OnExit()
    {
        GMScript.instance.inGame = false;
        SceneManager.LoadScene("StartScene");
    }
}
