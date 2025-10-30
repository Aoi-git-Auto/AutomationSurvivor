using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossEnemy : AbstractEnemy
{
    [SerializeField]
    private GameObject hpBar;
    private BossHPBarContoroller bar;
    private GameObject hp;
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
        if (currentHP <= 0)
        {
            BossDying();
        }
    }
    
    private void BossDying()
    {
        Destroy(hp);
        boxCollider.enabled = false;
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
