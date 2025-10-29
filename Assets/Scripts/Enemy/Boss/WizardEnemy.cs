using System.Collections;
using UnityEngine;
using DG.Tweening;

public class WizardEnemy : AbstractEnemy
{
    [SerializeField]
    private GameObject hpBar;
    private BossHPBarContoroller bar;
    private GameObject hp;
    private float attackTime = 0f;
    private bool canAttack = true;
    [SerializeField]
    private GameObject summonEnemy;
    [SerializeField]
    private GameObject summonEffect;
    [SerializeField]
    private Sprite summonSprite;
    [SerializeField]
    private EnemyStatus summonStatus;
    private Animator animator;

    [SerializeField]
    private AudioClip dyingSE;
    [SerializeField]
    private AudioClip summonSE;
    private AudioSource audioSource;

    protected new void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        GameObject canvas = GameObject.Find("UICanvas");

        AudioManager.instance.PlayBossBGM(bgm);
        hp = Instantiate(hpBar, canvas.transform);
        bar = hp.GetComponent<BossHPBarContoroller>();

        bar.InitializeHPbar(MaxHP, this.name);
    }

    private void Update()
    {
        if (!canAttack) return;
        attackTime += Time.deltaTime;
        if(attackTime > 3f)
        {
            StartCoroutine(Summon());
            attackTime = 0f;
        }
    }

    public override void Damage(float damage)
    {
        var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        currentHP -= damage;
        bar.UpdateHP(currentHP);
        if (currentHP <= 0)
        {
            BossDying();
        }
    }

    private IEnumerator Summon()
    {
        canMove = false;
        animator.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Sprite original = spriteRenderer.sprite;
        spriteRenderer.sprite = summonSprite;
        yield return new WaitForSeconds(0.3f);
        var effect = Instantiate(summonEffect, new Vector2(transform.position.x + 0.4f, transform.position.y - 0.3f), Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        audioSource.PlayOneShot(summonSE);
        var enemy = Instantiate(summonEnemy, new Vector2(transform.position.x + 0.4f, transform.position.y), Quaternion.identity);
        Destroy(effect);
        enemy.GetComponent<AbstractEnemy>().Initialize(summonStatus);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = original;
        canMove = true;
        animator.enabled = true;
    }
    
    private void BossDying()
    {
        Destroy(hp);
        boxCollider.enabled = false;
        canAttack = false;
        audioSource.PlayOneShot(dyingSE);
        spriteRenderer.color = Color.red;
        canMove = false;
        animator.enabled = false;
        spriteRenderer.DOFade(0f, 1f).OnComplete(() =>
        {
            AudioManager.instance.PlayinGameBGM();
            GMScript.instance.bossArriving = false;
            Die();
        });
    }
}
