using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour,IDamageable
{
    [SerializeField] StatusData statusdata;
    [SerializeField]
    private SpriteRenderer spriteATK;
    private SpriteRenderer spritePlayer;
    private float currentTime = 0.0f;
    private float damageTime = 0.0f;
    [SerializeField] GameObject normalATK;
    [SerializeField] Sprite imageIdle;
    [SerializeField] Sprite imagenomalATK;
    [SerializeField] Slider hpSlider;
    private Rigidbody2D rb;
    private float currentHP;
    public float Health => currentHP;
    private bool invincibility;
    private bool isFlashing;
    private Vector2 inputAxis;
    void Start()
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = statusdata.MAXHP;
            hpSlider.value = statusdata.MAXHP;
        }
        rb = GetComponent<Rigidbody2D>();
        spriteATK.sprite = imageIdle;
        spritePlayer = GetComponent<SpriteRenderer>();
        normalATK.GetComponent<BoxCollider2D>().enabled = false;
        currentHP = statusdata.MAXHP;
        invincibility = false;
        isFlashing = false;
    }

    // Update is called once per frameb
    void Update()
    {
        hpSlider.maxValue = statusdata.MAXHP;
        inputAxis.x = Input.GetAxisRaw("Horizontal");
        inputAxis.y = Input.GetAxisRaw("Vertical");
        currentTime += Time.deltaTime;
        if (invincibility)
        {
            damageTime += Time.deltaTime;
            if (damageTime > 1.0f)
            {
                damageTime = 0.0f;
                invincibility = false;
            }
        }
        if (currentTime > statusdata.SPAN)
        {
            spriteATK.sprite = imagenomalATK;
            normalATK.GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine(N_ATKswitch());
            currentTime = 0.0f;
        }
    }
    void FixedUpdate()
    {
        rb.velocity = inputAxis.normalized * statusdata.SPEED;
    }

    public void Damage(float damage)
    {
        if (!invincibility)
        {
            currentHP -= damage;
            invincibility = true;
            if (hpSlider != true)
            {
                hpSlider.value = currentHP;
            }
            if (currentHP <= 0)
            {
                Die();
            }
            if (!isFlashing)
            {
                StartCoroutine(flashSprite());
            }
        }
    }

    public void Heal(int heal)
    {
        if (hpSlider != null)
        {
            if (statusdata.MAXHP >= currentHP)
            {
                currentHP += heal;
                hpSlider.value = currentHP;
            }
        }
    }

    public void Die()
    {
        spritePlayer.enabled = false;
        GMScript.instance.GameOver();
    }

    private IEnumerator N_ATKswitch()
    {
        yield return new WaitForSeconds(5);
        spriteATK.sprite = imageIdle;
        normalATK.GetComponent<BoxCollider2D>().enabled = false;
    }
    private IEnumerator flashSprite()
    {
        isFlashing = true;
        float finishTime = 0.0f;
        while (finishTime < 1.0f)
        {
            spritePlayer.enabled = false;
            yield return new WaitForSeconds(0.1f);

            spritePlayer.enabled = true;
            yield return new WaitForSeconds(0.1f);

            finishTime += 0.1f * 2;
        }
        spritePlayer.enabled = true;
        isFlashing = false;
    }
}
