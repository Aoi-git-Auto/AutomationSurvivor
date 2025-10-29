using UnityEngine;

public class HealEnemyScript : AbstractEnemy
{
    [SerializeField]
    private GameObject healItemPrehub;

    public override void Damage(float damage)
    {
        var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            DropHealItem();
            Die();
        }
    }

    private void DropHealItem()
    {
        if (healItemPrehub != null)
        {
            Instantiate(healItemPrehub, new Vector2(transform.position.x - 0.1f, transform.position.y), transform.rotation);
        }
    }
}
