using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearParticleScript : MonoBehaviour
{
    Vector2 DestinationPos;
    Vector2 currentPos;
    Vector3 localAngle;
    Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        float rndPosX = Random.Range(-2.6f, 2.61f);
        float rndPosY = Random.Range(-4.5f, -4.51f);
        currentPos.x = rndPosX;
        currentPos.y = 6f;
        transform.position = currentPos;
        DestinationPos = transform.position;
        DestinationPos.y = rndPosY;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, DestinationPos, 4 * Time.deltaTime);
        currentPos = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            EXPManager.instance.ExpBarDraw();
            Destroy(this.gameObject);   
        }
    }
}
