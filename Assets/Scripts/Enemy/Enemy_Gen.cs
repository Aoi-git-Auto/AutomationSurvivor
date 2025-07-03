using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Gen : MonoBehaviour
{
    [SerializeField] StatusData statusdata1;
    [SerializeField] StatusData statusstrong;
    [SerializeField]
    private GameObject StrongEnemyPrehub;
    [SerializeField]
    private GameObject BossEnemyPrehub;
    [SerializeField]
    private GameObject EnemyPrehub;
    GameObject Player;
    Vector2 PlayerPos;
    private float currentTime = 0.0f;
    private float overTime = 0.0f;
    private float bossTime = 0.0f;
    int rndUD;
    int rndLR;
    private bool Boss;
    Vector2 enemyspwnPos;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Boss = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        overTime += Time.deltaTime;
        bossTime += Time.deltaTime;
        if (currentTime > statusdata1.SPAN)
        {
            EnemyGenerate(EnemyPrehub);
            currentTime = 0.0f;
        }
        if (Time.time > 10f && overTime > statusstrong.SPAN)
        {
            EnemyGenerate(StrongEnemyPrehub);
            overTime = 0.0f;
        }
        if (bossTime > 10f && Boss == false)
        {
            bossTime = 0.0f;
            Boss = true;
            EnemyGenerate(BossEnemyPrehub);
        }
    }
    public void EnemyGenerate(GameObject Enemy)
    {
        PlayerPos = Player.transform.position;
        rndUD = Random.Range(0,2);
        rndLR = Random.Range(0,2);
        float rndPositiveX = Random.Range(1.0f,3.0f);
        float rndPositiveY = Random.Range(1.0f,3.0f);
        float rndNegativeX = Random.Range(-3.0f,-1.0f);
        float rndNegativeY = Random.Range(-3.0f,-1.0f);
        switch(rndUD)
        {
            case 0:
            enemyspwnPos.y = rndPositiveY;
            break;
            case 1:
            enemyspwnPos.y = rndNegativeY;
            break;
        }
        switch(rndLR)
        {
            case 0:
            enemyspwnPos.x = rndPositiveX;
            break;
            case 1:
            enemyspwnPos.x = rndNegativeX;
            break;
        }
        enemyspwnPos = enemyspwnPos + PlayerPos;
        var enemy = Instantiate(Enemy,enemyspwnPos,transform.rotation);
    }
}
