using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private float time = 10f;
    public void OnGame()
    {
        GMScript.instance.inGame = true;
        GMScript.instance.currentTime = time;
        SceneManager.LoadScene("GameScene");
    }

    public void OnRetry()
    {
        GMScript.instance.currentTime = time;
        SceneManager.LoadScene("GameScene");
    }

    public void OnExit()
    {
        GMScript.instance.inGame = false;
        SceneManager.LoadScene("StartScene");
    }
}
