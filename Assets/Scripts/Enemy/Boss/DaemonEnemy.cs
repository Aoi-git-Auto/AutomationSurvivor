using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DaemonEnemy : AbstractEnemy
{
    [SerializeField]
    private GameObject hpBar;
    private BossHPBarContoroller bar;
    private GameObject hp;
    private bool canAttack = true;

    private float attackTime = 0f;
    private Animator animator;
    [SerializeField]
    private float rushTime ;
    [SerializeField]
    private float rushSpeed;

    [SerializeField]
    private AudioClip dyingSE;
    [SerializeField]
    private AudioClip rushSE;
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

        bar.InitializeHPbar(MaxHP, enemyName);
    }

    private void Update()
    {
        if (!canAttack) return;
        attackTime += Time.deltaTime;
        if(attackTime > 6f)
        {
            StartCoroutine(Rush());
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

    private IEnumerator Rush()
    {
        canMove = false;
        animator.enabled = false;

        Vector2 target = playerPos;
        Vector2 startPos = transform.position;
        Vector2 direction = (target - startPos).normalized;

        yield return new WaitForSeconds(0.5f);

        float elapsed = 0f;
        audioSource.PlayOneShot(rushSE);
        while (elapsed < rushTime)
        {
            transform.position += (Vector3)(direction * rushSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
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
        })
        .SetLink(gameObject);
    }
}
