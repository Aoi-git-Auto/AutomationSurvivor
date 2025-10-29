using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ExplodeMushroom : AbstractEnemy
{
    [SerializeField]
    private GameObject explodePrehub;
    private float duaration = 0.1f;
    private float explodeTime = 1f;
    
    public override void Damage(float damage)
    {
        var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            StartCoroutine(Explode());
        }
    }
    
    private IEnumerator Explode()
    {
        Color original = spriteRenderer.color;
        float count = 0f;
        canMove = false;
        while (count < explodeTime)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(duaration);
            spriteRenderer.color = original;
            yield return new WaitForSeconds(duaration);

            count += 0.1f * 2;
        }
        Instantiate(explodePrehub, transform.position, Quaternion.identity);
        Die();
    }
}
