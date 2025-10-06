using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    private float damage;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy_Move>().Damage(damage);
        }
    }

    public void SetAttack(float atk)
    {
        damage = atk;
    }

    public void SetPos(Vector2 currentPos)
    {
        transform.position = currentPos;
    }
}
