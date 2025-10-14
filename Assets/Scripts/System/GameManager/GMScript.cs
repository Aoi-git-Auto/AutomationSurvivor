using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GMScript : MonoBehaviour
{
    public static GMScript instance;
    [SerializeField] StatusData statusdata;
    private Text timerText;
    public float PlayerDefaultATK;
    public float currentTime = 300f;
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
    void Update()
    {
        if (inGame && timerText != null)
        {
            timerText.text = $"{(int)currentTime}";
            currentTime -= Time.deltaTime;
            timerText.text = $"{(int)currentTime}";
            if(currentTime <= 0)
            {
                GameClear();
            }
        }
    }
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

    public void GetTimerText(Text t)
    {
        timerText = t;
    }
}
