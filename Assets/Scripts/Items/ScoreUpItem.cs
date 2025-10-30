using UnityEngine;

public class ScoreUpItem : AbstractStatusItem
{
    [SerializeField]
    private float scoreUp = 1.2f;
    private GameObject scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager");
    }

    protected override void Enhance(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            scoreManager.GetComponent<ScoreManager>().BoostScoreRate(scoreUp);
            Destroy(this.gameObject);
        }
    }
}
