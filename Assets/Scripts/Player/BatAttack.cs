using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    private float damage;
    private float knockback;

    void Start()
    {
        Destroy(this.gameObject,1f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IEnemy>().Damage(damage);
            other.gameObject.GetComponent<IEnemy>().KnockBack(knockback);
        }
    }

    public void SetStatus(float atk, float force, Vector2 currentPos)
    {
        damage = atk;
        knockback = force;
        transform.position = currentPos;
    }
}
