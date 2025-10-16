using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : AbstractEnemy
{
    [SerializeField]
    private GameObject hpBar;
    private Vector3 diff;
    private Vector3 direction;
    private BossHPBarContoroller bar;
    private GameObject hp;
    private EnemyGenerator enemyGenerator;

    protected new void Start()
    {
        base.Start();
        GameObject canvas = GameObject.Find("UICanvas");

        hp = Instantiate(hpBar, canvas.transform);
        bar = hp.GetComponent<BossHPBarContoroller>();

        bar.InitializeHPbar(MaxHP, this.name);
    }
    
    public override void Damage(float damage)
    {
        var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        currentHP -= damage;
        bar.UpdateHP(currentHP);
        if(currentHP <= 0)
        {
            Destroy(hp);
            if (enemyGenerator != null)
            {
                enemyGenerator.bossArrival = false;
            }
            Die();
        }
    }

    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        diff.x = playerPos.x - this.transform.position.x;
        if (diff.x > 0)
        {
            direction = new Vector3(0, -180, 0);
            this.transform.eulerAngles = direction;
        }
        if (diff.x < 0)
        {
            direction = new Vector3(0, 180, 0);
            this.transform.eulerAngles = direction;
        }
    }
}
