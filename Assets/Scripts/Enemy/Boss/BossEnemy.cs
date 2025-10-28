using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : AbstractEnemy
{
    [SerializeField]
    private GameObject hpBar;
    private BossHPBarContoroller bar;
    private GameObject hp;
    private GameObject bossGenerator;

    [SerializeField]
    private AudioClip dyingSE;
    private AudioSource audioSource;

    protected new void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        GameObject canvas = GameObject.Find("UICanvas");

        AudioManager.instance.PlayBossBGM(bgm);
        bossGenerator = GameObject.Find("BossEnemyGenerator");
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
        if(currentHP <= 0)
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
}
