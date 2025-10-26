using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGenerator : MonoBehaviour
{
    private float generateTime = 0f;
    private GameObject parent;
    [SerializeField]
    private float span = 7f;
    [SerializeField]
    private GameObject explosionPrehub;
    [SerializeField]
    private float spawnRadius = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.transform.position;
        generateTime += Time.deltaTime;
        if (generateTime > span)
        {
            SpawnRodom();
        }
    }
    
    private void SpawnRodom()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * spawnRadius;

        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        Vector2 spawnPos = (Vector2)transform.position + offset;

        Instantiate(explosionPrehub, spawnPos, Quaternion.identity);
        generateTime = 0;
    }
}
