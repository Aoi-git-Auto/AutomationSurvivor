using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GMScript : MonoBehaviour
{
    public static GMScript instance;
    private Text timerText;
    private GameObject gameEndPanel;
    private Text gameEndText;
    private Text scoreText;
    private Button endExit;
    private Button endRetry;
    private GameObject scoreManager;
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
        Time.timeScale = 0;
        GameEnd();
    }
    public void GameClear()
    {
        inGame = false;
        SceneManager.LoadScene("GameClearScene");
    }

    public void LoadUI(GameObject endPanel, Text timer, Text score, Text endText, Button re, Button esc, GameObject scManager)
    {
        gameEndPanel = endPanel;
        timerText = timer;
        scoreText = score;
        gameEndText = endText;
        endRetry = re;
        endExit = esc;
        scoreManager = scManager;

        gameEndPanel.SetActive(false);
    }

    private void GameEnd()
    {
        Color c = gameEndText.color;
        c.a = 0;
        gameEndText.color = c;

        gameEndPanel.SetActive(true);
        Sequence seq = DOTween.Sequence();

        RectTransform buttonRectL = endExit.GetComponent<RectTransform>();
        RectTransform buttonRectR = endRetry.GetComponent<RectTransform>();
        Vector2 endPosL = buttonRectL.anchoredPosition;
        Vector2 endPosR = buttonRectR.anchoredPosition;
        buttonRectL.anchoredPosition = endPosL + new Vector2(0, -300);
        buttonRectR.anchoredPosition = endPosR + new Vector2(0, -300);

        int finalscore = scoreManager.GetComponent<ScoreManager>().GetStatus();

        seq.Append(gameEndText.DOFade(1f, 1f))
        .AppendInterval(1f)
        .Append(gameEndText.rectTransform.DOAnchorPosY(gameEndText.rectTransform.anchoredPosition.y + 60f, 0.6f))
        .AppendInterval(0.3f)
        .AppendCallback(() =>
        {
            scoreText.text = finalscore.ToString();
        })
        .AppendInterval(1.5f)
        .Append(buttonRectL.DOAnchorPosY(endPosL.y, 0.6f))
        .Join(buttonRectR.DOAnchorPosY(endPosR.y, 0.6f));

        seq.SetUpdate(true);
    }
}
