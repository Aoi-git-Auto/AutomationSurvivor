using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GMScript : MonoBehaviour
{
    public static GMScript instance;
    private TMP_Text timerText;
    private GameObject gameEndPanel;
    private Text gameEndText;
    private Text scoreText;
    private Button endExit;
    private Button endRetry;
    private GameObject scoreManager;
    public float currentTime = 300f;
    public bool inGame;
    public bool bossArriving;
    private bool isDead = true;

    [SerializeField]
    private AudioClip playerDieBGM;
    [SerializeField]
    private AudioClip clearBGM;
    [SerializeField]
    private AudioClip scoreSE;
    private AudioSource audioSource;

    private GameObject gameEndButton;
    private PlayerInput playerInput;

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
        AudioManager.instance.PlayTitleBGM();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (inGame && timerText != null)
        {
            currentTime -= Time.deltaTime;
            timerText.text = $"{(int)currentTime}";
            if(currentTime <= 0)
            {
                isDead = false;
                GameEnd();
            }
        }
    }

    public void LoadUI(GameObject endPanel, TMP_Text timer, Text score, Text endText, Button re, Button esc, GameObject scManager)
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

    public void GameEnd()
    {
        inGame = false;
        Time.timeScale = 0;

        if (isDead)
        {
            AudioManager.instance.PlayBGM(playerDieBGM);
            gameEndText.text = "You Died...";
            gameEndText.color = Color.red;
        }
        else
        {
            AudioManager.instance.PlayBGM(clearBGM);
            gameEndText.text = "You Survived!";
            gameEndText.color = Color.yellow;
        }

        isDead = true;

        Color c = gameEndText.color;
        c.a = 0;
        gameEndText.color = c;

        gameEndPanel.SetActive(true);
        gameEndPanel.transform.SetAsLastSibling();
        playerInput = FindObjectOfType<PlayerInput>();
        Sequence seq = DOTween.Sequence();

        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap("UI");
            gameEndButton = gameEndPanel.GetComponentInChildren<Button>().gameObject;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameEndButton);
        }

        RectTransform buttonRectL = endExit.GetComponent<RectTransform>();
        RectTransform buttonRectR = endRetry.GetComponent<RectTransform>();
        Vector2 endPosL = buttonRectL.anchoredPosition;
        Vector2 endPosR = buttonRectR.anchoredPosition;
        buttonRectL.anchoredPosition = endPosL + new Vector2(0, -1000);
        buttonRectR.anchoredPosition = endPosR + new Vector2(0, -1000);

        int finalscore = scoreManager.GetComponent<ScoreManager>().GetStatus();

        seq.Append(gameEndText.DOFade(1f, 1f))
        .AppendInterval(1f)
        .Append(gameEndText.rectTransform.DOAnchorPosY(gameEndText.rectTransform.anchoredPosition.y + 150f, 0.6f))
        .AppendInterval(0.3f)
        .AppendCallback(() =>
        {
            audioSource.PlayOneShot(scoreSE);
            scoreText.text = finalscore.ToString();
        })
        .AppendInterval(1.5f)
        .Append(buttonRectL.DOAnchorPosY(endPosL.y, 0.6f))
        .Join(buttonRectR.DOAnchorPosY(endPosR.y, 0.6f));

        seq.SetUpdate(true);
    }
}
