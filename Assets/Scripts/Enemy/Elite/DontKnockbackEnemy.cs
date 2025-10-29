using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontKnockbackEnemy : AbstractEnemy
{
    public override void Damage(float damage)
    {
        var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        StartCoroutine(StopKnockBack(0));
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }
}
