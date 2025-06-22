using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SelfDestroy");
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<Enemy_Move>().Damage(0.1f);
            other.gameObject.GetComponent<Enemy_Move>().NockBack(1f);
        }
    }
    IEnumerator SelfDestroy(){
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
