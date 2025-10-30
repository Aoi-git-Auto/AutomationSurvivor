using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyDataBase dataBase;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float spawnBuffer = 2.0f;
    private float currentTime = 0f;
    private float span;
    private Vector2 enemySpawnPos;
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
    }
    
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= span)
        {
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
        Camera camera = Camera.main;
        float cameraHeight = camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        Vector2 screenCenter = player.transform.position;

        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                enemySpawnPos = new Vector2(
                    Random.Range(screenCenter.x - cameraWidth, screenCenter.x + cameraWidth),
                    screenCenter.y + cameraHeight + spawnBuffer
                );
                break;
            case 1:
                enemySpawnPos = new Vector2(
                    Random.Range(screenCenter.x - cameraWidth, screenCenter.x + cameraWidth),
                    screenCenter.y - cameraHeight - spawnBuffer
                );
                break;
            case 2:
                enemySpawnPos = new Vector2(
                    screenCenter.x + cameraWidth + spawnBuffer,
                    Random.Range(screenCenter.y - cameraHeight, screenCenter.y + cameraHeight)
                );
                break;
            case 3:
                enemySpawnPos = new Vector2(
                    screenCenter.x - cameraWidth - spawnBuffer,
                    Random.Range(screenCenter.y - cameraHeight, screenCenter.y + cameraHeight)
                );
                break;
        }

        int randomIndex = Random.Range(0, dataBase.enemies.Count);
        GameObject rndEnemy = dataBase.enemies[randomIndex].PREHUB;

        yield return new WaitForSeconds(1f);
        var enemy = Instantiate(rndEnemy, enemySpawnPos, transform.rotation);
        enemy.GetComponent<IEnemy>().Initialize(dataBase.enemies[randomIndex]);
    }
}
