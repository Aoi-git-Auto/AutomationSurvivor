using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GMScript : MonoBehaviour
{
    public static GMScript instance;
    [SerializeField] StatusData statusdata;
    public float PlayerDefaultATK;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        Time.timeScale = 1;
        PlayerDefaultATK = statusdata.ATK;
    }
    public void OnRetry()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }
    public void OnFinish()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
    public void GameClear()
    {
        SceneManager.LoadScene("GameClearScene");
    }
}
