using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour, IEnemy
{
    [SerializeField]
    protected EnemyStatus enemyStatus;
    [SerializeField]
    protected GameObject expPrehub;
    protected float enemyATK;
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
        enemyATK = enemyStatus.ATK;
        MaxHP = enemyStatus.MAXHP;
        element = enemyStatus.ELEMENT;
        enemyEXP = enemyStatus.EXP;
        speed = enemyStatus.SPEED;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
    }

    protected abstract void Move();
    public void Damage(float damage)
    {
        MaxHP -= damage;
        if (MaxHP <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Destroy(this.gameObject);
        DropEXP();
    }

    protected void DropEXP()
    {
        for (int i = 0; i < enemyEXP; i++)
        {
            Instantiate(expPrehub, this.transform.position, this.transform.rotation);
        }
    }
    
    protected void Hit(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<IDamageable>().Damage(enemyATK);
        }
    }
    
    protected IEnumerator StopKnockBack(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
    }

    public void KnockBack(float knockBack)
    {
        Vector2 directrion = (transform.position - playerPos).normalized;
        rb.velocity = directrion * knockBack;
        StopKnockBack(0.5f);
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected void OnTrrigerStay2D(Collider2D other)
    {
        playerPos = player.transform.position;
        Hit(other.gameObject);
    }
}
