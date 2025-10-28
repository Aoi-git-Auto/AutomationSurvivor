using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnpouleScript : MonoBehaviour
{
    private Vector2 DestinationPos;
    private Vector2 currentPos;
    [SerializeField] 
    private GameObject DamageZonePrehub;
    private Vector3 localAngle;
    private Transform myTrans;

    // Start is called before the first frame update
    void Start()
    {
        float rndPosX = Random.Range(-2.6f,2.6f);
        float rndPosY = Random.Range(-4.5f,4.5f);
        currentPos.x = rndPosX;
        currentPos.y = 6f;
        transform.position = currentPos;
        DestinationPos = transform.position;
        DestinationPos.y = rndPosY;
        myTrans = this.transform;
        localAngle = myTrans.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        localAngle.z += Time.deltaTime;
        myTrans.localEulerAngles = localAngle;
        transform.position = Vector2.MoveTowards(transform.position, DestinationPos, 4 * Time.deltaTime);
        currentPos = transform.position;
        if (currentPos.y == DestinationPos.y)
        {
            Instantiate(DamageZonePrehub, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(DamageZonePrehub, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
