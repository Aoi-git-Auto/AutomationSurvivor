using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnpouleScript : MonoBehaviour
{
    Vector2 DestinationPos;
    Vector2 currentPos;
    [SerializeField] GameObject DamageZonePrehub;
    Vector3 localAngle;
    Transform myTrans;

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
        transform.position = Vector2.MoveTowards(transform.position,DestinationPos,4*Time.deltaTime);
        currentPos = transform.position;
        if(currentPos.y == DestinationPos.y){
            var DameageZone = Instantiate(DamageZonePrehub,transform.position,transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
