using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour, IEnemy
{
    [SerializeField]
    protected EnemyStatus enemyStatus;
    [SerializeField]
    protected GameObject expPrehub;
    [SerializeField]
    protected GameObject hitEffect;
    protected float enemyATK;
    protected float MaxHP;
    protected float currentHP;
    protected Type type;
    protected int enemyEXP;
    protected float speed;
    protected int score;
    protected Rigidbody2D rb;

    protected GameObject player;
    protected Vector3 playerPos;

    private GameObject scoreManager;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        enemyATK = enemyStatus.ATK;
        MaxHP = enemyStatus.MAXHP;
        currentHP = MaxHP;
        type = enemyStatus.TYPE;
        enemyEXP = enemyStatus.EXP;
        speed = enemyStatus.SPEED;
        score = enemyStatus.SCORE;
        player = GameObject.FindGameObjectWithTag("Player");
        scoreManager = GameObject.Find("ScoreManager");
        playerPos = player.transform.position;
    }

    protected abstract void Move();
    public abstract void Damage(float damage);

    protected void Die()
    {
        Destroy(this.gameObject);
        scoreManager.GetComponent<ScoreManager>().AddScore(score);
        DropEXP();
    }

    protected void DropEXP()
    {
        var exp = Instantiate(expPrehub, this.transform.position, this.transform.rotation);
        exp.GetComponent<ExpOrbScript>().GetExpAmount(enemyEXP);
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
        StartCoroutine(StopKnockBack(0.5f));
    }

    protected virtual void FixedUpdate()
    {
        playerPos = player.transform.position;
        Move();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other.gameObject);
    }
}
