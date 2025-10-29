using System.Collections;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyDataBase dataBase;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject bossArrivingEffect;
    [SerializeField]
    private GameObject canvas;
    private Vector2 playerPos;
    private float currentTime = 0f;
    private float span = 40f;
    private int rndUD;
    private int rndLR;
    private Vector2 enemySpwanPos;
    private bool inHalf = false;
    private bool inLast = false;

    [SerializeField]
    private AudioClip bossArrivalSE;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= span && !GMScript.instance.bossArriving)
        {
            GMScript.instance.bossArriving = true;
            audioSource.PlayOneShot(bossArrivalSE);
            var effect = Instantiate(bossArrivingEffect, canvas.transform);
            Destroy(effect, 1f);
            Debug.Log(GMScript.instance.bossArriving);
            StartCoroutine(GenerateEnemy());
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
    
    private IEnumerator GenerateEnemy()
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

        yield return new WaitForSeconds(1f);
        var enemy = Instantiate(rndEnemy, enemySpwanPos, transform.rotation);
        enemy.GetComponent<IEnemy>().Initialize(dataBase.enemies[randomIndex]);
    }
}
