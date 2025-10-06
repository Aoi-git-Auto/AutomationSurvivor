using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    private float damage;
    private float nockback;

    void Start()
    {
        Destroy(this.gameObject,1f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy_Move>().Damage(damage);
            other.gameObject.GetComponent<Enemy_Move>().NockBack(nockback);
        }
    }

    public void SetStatus(float atk, float force, Vector2 currentPos)
    {
        damage = atk;
        nockback = force;
        transform.position = currentPos;
    }
}
