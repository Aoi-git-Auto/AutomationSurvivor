using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyDataBase dataBase;
    [SerializeField]
    private GameObject player;
    private Vector2 playerPos;
    private float currentTime = 0f;
    private float span;
    private int rndUD;
    private int rndLR;
    private Vector2 enemySpwanPos;
    public bool bossArrival = false;
    private bool inHalf = false;
    private bool inLast = false;

    void Start()
    {
        if (dataBase.enemies[0].TYPE == Type.NORMAL)
        {
            span = 2f;
        }
        else if (dataBase.enemies[0].TYPE == Type.ELITE)
        {
            span = 10f;
        }

        else if (dataBase.enemies[0].TYPE == Type.BOSS)
        {
            span = 40f;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= span && !bossArrival)
        {
            if (dataBase.enemies[0].TYPE == Type.BOSS)
            {
                bossArrival = true;
            }
            GenerateEnemy();
            currentTime = 0f;
        }
        if (GMScript.instance.currentTime <= 150 && !inHalf)
        {
            span = span / 2;
            inHalf = true;
        }
        if(GMScript.instance.currentTime <= 60 && !inLast)
        {
            span = span / 4;
            inLast = true;
        }
    }
    
    private void GenerateEnemy()
    {
        playerPos = player.transform.position;
        rndUD = Random.Range(0,2);
        rndLR = Random.Range(0,2);
        float rndPositiveX = Random.Range(1.0f,3.0f);
        float rndPositiveY = Random.Range(1.0f,3.0f);
        float rndNegativeX = Random.Range(-3.0f,-1.0f);
        float rndNegativeY = Random.Range(-3.0f, -1.0f);
        
        switch(rndUD)
        {
            case 0:
            enemySpwanPos.y = rndPositiveY;
            break;
            case 1:
            enemySpwanPos.y = rndNegativeY;
            break;
        }
        switch(rndLR)
        {
            case 0:
            enemySpwanPos.x = rndPositiveX;
            break;
            case 1:
            enemySpwanPos.x = rndNegativeX;
            break;
        }
        enemySpwanPos = enemySpwanPos + playerPos;

        int randomIndex = Random.Range(0, dataBase.enemies.Count);
        GameObject rndEnemy = dataBase.enemies[randomIndex].PREHUB;

        var enemy = Instantiate(rndEnemy, enemySpwanPos, transform.rotation);
        enemy.GetComponent<IEnemy>().Initialize(dataBase.enemies[randomIndex]);
    }
}
