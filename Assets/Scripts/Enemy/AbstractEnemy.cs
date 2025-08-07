using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour, IEnemy
{
    [SerializeField]
    protected EnemyStatus enemyStatus;
    [SerializeField]
    protected GameObject expPrehub;
    protected float EnemyATK;
    protected float MaxHP;
    protected Element element;
    protected int enemyEXP;
    protected float speed;
    protected Rigidbody2D rb;

    protected GameObject player;
    protected Vector3 playerPos;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        EnemyATK = enemyStatus.ATK;
        MaxHP = enemyStatus.MAXHP;
        element = enemyStatus.ELEMENT;
        enemyEXP = enemyStatus.EXP;
        speed = enemyStatus.SPEED;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
    }

    public abstract void Damage(float damage);
    public abstract void KnockBack(float knockBack);
    protected abstract void Move();
    protected abstract void Hit(GameObject target);
    protected abstract void Die();
    protected void DropEXP()
    {
        for (int i = 0; i < enemyEXP; i++)
        {
            Instantiate(expPrehub, this.transform.position, this.transform.rotation);
        }
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected void OnTrrigerEnter2D(Collider2D other)
    {
        Hit(other.gameObject);
    }
}
