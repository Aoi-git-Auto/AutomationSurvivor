using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KingEnemy : AbstractEnemy
{
    private enum AttackType
    {
        Warp,Beam,Hall
    };
    private List<AttackType> randomAttack = new List<AttackType>();
    private float attackTime = 0f;

    [SerializeField]
    private GameObject warpGatePrehub;
    [SerializeField]
    private GameObject beamPrehub;
    [SerializeField]
    private GameObject itemEffectPrehub;
    [SerializeField]
    private GameObject hallPrehub;
    private bool canAttack = true;

    [SerializeField]
    private GameObject hpBar;
    private BossHPBarContoroller bar;
    private GameObject hp;
    private Animator animator;

    [SerializeField]
    private AudioClip dyingSE;
    [SerializeField]
    private AudioClip warpSE;
    [SerializeField]
    private AudioClip beamEffectSE;
    private AudioSource audioSource;

    protected new void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        randomAttack.Add(AttackType.Warp);
        randomAttack.Add(AttackType.Beam);
        randomAttack.Add(AttackType.Hall);
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
        if(attackTime > 5f)
        {
            AttackType type = randomAttack[Random.Range(0, randomAttack.Count)];
            SelectAttack(type);
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

    private void SelectAttack(AttackType type)
    {
        switch (type)
        {
            case AttackType.Warp:
                StartCoroutine(WarpToPlayer());
                break;
            case AttackType.Beam:
                StartCoroutine(LaunchBeam());
                break;
            case AttackType.Hall:
                StartCoroutine(SuctionHall());
                break;
        }
    }

    private IEnumerator WarpToPlayer()
    {
        canMove = false;
        animator.enabled = false;
        audioSource.PlayOneShot(warpSE);
        var effect = Instantiate(warpGatePrehub, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = false;
        Destroy(effect);
        yield return new WaitForSeconds(0.5f);
        transform.position = playerPos;
        audioSource.PlayOneShot(warpSE);
        effect = Instantiate(warpGatePrehub, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        spriteRenderer.enabled = true;
        canMove = true;
        animator.enabled = true;
    }

    private IEnumerator LaunchBeam()
    {
        canMove = false;
        animator.enabled = false;
        audioSource.PlayOneShot(beamEffectSE);
        var effect = Instantiate(itemEffectPrehub, playerPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(effect);
        Instantiate(beamPrehub, playerPos, Quaternion.identity);
        canMove = true;
        animator.enabled = true;
    }

    private IEnumerator SuctionHall()
    {
        canMove = false;
        animator.enabled = false;
        audioSource.PlayOneShot(beamEffectSE);
        var effect = Instantiate(itemEffectPrehub, playerPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(effect);
        Instantiate(hallPrehub, playerPos, Quaternion.identity);
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
