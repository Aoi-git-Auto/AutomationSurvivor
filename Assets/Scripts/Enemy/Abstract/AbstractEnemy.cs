using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour, IEnemy
{
    [SerializeField]
    protected GameObject expPrehub;
    [SerializeField]
    protected GameObject hitEffect;
    [SerializeField]
    private GameObject chestPrehub;
    protected float enemyATK;
    protected float MaxHP;
    protected float currentHP;
    protected int enemyEXP;
    protected float speed;
    protected int score;
    protected String enemyName;
    protected Type enemyType;
    protected bool canMove = true;
    protected AudioClip bgm;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D boxCollider;

    protected GameObject player;
    protected Vector3 playerPos;
    protected Vector3 diff;
    protected Vector3 direction;

    protected GameObject scoreManager;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreManager = GameObject.Find("ScoreManager");
        playerPos = player.transform.position;
    }

    public abstract void Damage(float damage);

    protected void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        diff.x = playerPos.x - this.transform.position.x;
        if (diff.x > 0)
        {
            spriteRenderer.flipX = false;
            direction = new Vector3(0, -180, 0);
            this.transform.eulerAngles = direction;
        }
        if(diff.x < 0)
        {
            spriteRenderer.flipX = true;
            direction = new Vector3(0, 180, 0);
            this.transform.eulerAngles = direction;
        }
    }

    protected void Die()
    {
        Destroy(this.gameObject);
        scoreManager.GetComponent<ScoreManager>().AddScore(score);
        DropEXP();
        DropChest();
    }

    protected void DropEXP()
    {
        var exp = Instantiate(expPrehub, new Vector2(transform.position.x + 0.1f, transform.position.y), transform.rotation);
        exp.GetComponent<ExpOrbScript>().GetExpAmount(enemyEXP);
    }

    protected void DropChest()
    {
        if (enemyType == Type.BOSS)
        {
            Instantiate(chestPrehub, transform.position, Quaternion.identity);
        }
        else
        {
            System.Random random = new System.Random();
            int randomIndex = random.Next(1, 100);
            if(randomIndex >= 98)
            {
                Instantiate(chestPrehub, transform.position, Quaternion.identity);
            }
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
        if (!canMove) return;
        Vector2 directrion = (transform.position - playerPos).normalized;
        rb.velocity = directrion * knockBack;
        StartCoroutine(StopKnockBack(0.5f));
    }

    public void Initialize(EnemyStatus initialStatus)
    {
        enemyATK = initialStatus.ATK;
        MaxHP = initialStatus.MAXHP;
        currentHP = MaxHP;
        enemyEXP = initialStatus.EXP;
        speed = initialStatus.SPEED;
        score = initialStatus.SCORE;
        enemyType = initialStatus.TYPE;
        enemyName = initialStatus.NAME;
        if(initialStatus.BGM != null)
        {
            bgm = initialStatus.BGM;
        }
    }

    protected virtual void FixedUpdate()
    {
        if(!canMove) return ;
        playerPos = player.transform.position;
        Move();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other.gameObject);
    }
}
