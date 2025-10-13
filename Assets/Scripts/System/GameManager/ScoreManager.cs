using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private int maxDigits = 9;
    private int digits;
    private int count;
    private int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        scoreText.text = "Score 000000000";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int getScore)
    {
        currentScore += getScore;
        digits = currentScore;
        count = 0;
        scoreText.text = "Score ";
        while (digits > 0)
        {
            digits = digits / 10;
            count++;
        }
        for (int i = 0; i < maxDigits - count; i++)
        {
            scoreText.text += "0";
        }
        scoreText.text += $"{currentScore}";
    }
    
    public int GetStatus()
    {
        return currentScore;
    }
}
