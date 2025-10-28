using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEnemyScript : AbstractEnemy
{
    public override void Damage(float damage)
    {
        var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        currentHP -= damage;
        if(currentHP <= 0)
        {
            Die();
        }
    }
}
