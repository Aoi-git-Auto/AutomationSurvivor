using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GMScript : MonoBehaviour
{
    public static GMScript instance;
    [SerializeField] StatusData statusdata;
    [SerializeField]
    private Text timerText;
    public float PlayerDefaultATK;
    public float currentTime = 10f;
    public bool inGame;
    // Start is called before the first frame update
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
    }
    void Start()
    {
        Time.timeScale = 1;
        PlayerDefaultATK = statusdata.ATK;
    }

    void Update()
    {
        Debug.Log(inGame);
        if (inGame)
        {
            currentTime -= Time.deltaTime;
            Debug.Log("残り時間は"+$"{currentTime}");
            if(currentTime <= 0)
            {
                GameClear();
            }
        }
    }

    /*public void OnGame()
    {
        inGame = true;
        currentTime = 10f;
        SceneManager.LoadScene("GameScene");
    }
    public void OnRetry()
    {

        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }
    public void OnFinish()
    {
        inGame = false;
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }*/
    public void GameOver()
    {
        inGame = false;
        SceneManager.LoadScene("GameOverScene");
    }
    public void GameClear()
    {
        inGame = false;
        SceneManager.LoadScene("GameClearScene");
    }
}
