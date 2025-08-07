using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GMScript : MonoBehaviour
{
    public static GMScript instance;
    [SerializeField] StatusData statusdata;
    [SerializeField] GameObject GameClearUI;
    [SerializeField] GameObject GameOverUI;
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
        GameClearUI.GetComponent<Canvas>().enabled = false;
        GameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void OnFinish()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }
    public void gameOver()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
}
