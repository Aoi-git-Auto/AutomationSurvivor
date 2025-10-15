using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIBinder : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreManager;
    [SerializeField]
    private GameObject gameEndPanel;
    [SerializeField]
    private Text gameEndText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        GMScript.instance.LoadUI(gameEndPanel, timerText, scoreText, gameEndText, retryButton, exitButton, scoreManager);
    }


}
