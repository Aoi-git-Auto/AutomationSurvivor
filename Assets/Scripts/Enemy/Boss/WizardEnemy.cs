using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemy : AbstractEnemy
{
    [SerializeField]
    private GameObject hpBar;
    private BossHPBarContoroller bar;
    private GameObject hp;
    private GameObject bossGenerator;
    private float attackTime = 0f;
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
    private AudioSource audioSource;

    protected new void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        GameObject canvas = GameObject.Find("UICanvas");

        AudioManager.instance.PlayBossBGM(bgm);
        bossGenerator = GameObject.Find("BossEnemyGenerator");
        hp = Instantiate(hpBar, canvas.transform);
        bar = hp.GetComponent<BossHPBarContoroller>();

        bar.InitializeHPbar(MaxHP, this.name);
    }

    private void Update()
    {
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
            Destroy(hp);
            if (bossGenerator != null)
            {
                bossGenerator.GetComponent<EnemyGenerator>().bossArrival = false;
            }
            audioSource.PlayOneShot(dyingSE);
            AudioManager.instance.PlayinGameBGM();
            Die();
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
        var enemy = Instantiate(summonEnemy, new Vector2(transform.position.x + 0.4f, transform.position.y), Quaternion.identity);
        Destroy(effect);
        enemy.GetComponent<AbstractEnemy>().Initialize(summonStatus);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = original;
        canMove = true;
        animator.enabled = true;
    }
}
