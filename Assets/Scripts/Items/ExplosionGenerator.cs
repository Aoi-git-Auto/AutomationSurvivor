using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGenerator : MonoBehaviour
{
    private float generateTime = 0f;
    private GameObject player;
    [SerializeField]
    private float span = 6f;
    [SerializeField]
    private GameObject explosionPrehub;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        generateTime += Time.deltaTime;
        if (generateTime > span)
        {
            SpawnBomb();
        }
    }
    
    private void SpawnBomb()
    {
        Instantiate(explosionPrehub, transform.position, Quaternion.identity);
        generateTime = 0;
    }
}
