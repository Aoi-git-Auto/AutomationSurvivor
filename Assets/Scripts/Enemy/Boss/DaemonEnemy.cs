using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DaemonEnemy : AbstractEnemy
{
    [SerializeField]
    private GameObject hpBar;
    private BossHPBarContoroller bar;
    private GameObject hp;
    private GameObject bossGenerator;

    private float attackTime = 0f;
    private Animator animator;
    [SerializeField]
    private float rushTime = 0.8f;
    [SerializeField]
    private float rushSpeed = 10f;

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
    
    private IEnumerator Rush()
    {
        canMove = false;
        animator.enabled = false;

        Vector2 target = playerPos;
        Vector2 startPos = transform.position;
        Vector2 direction = (target - startPos).normalized;

        yield return new WaitForSeconds(0.5f);

        float elapsed = 0f;
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
}
