using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] StatusData statusdata;
    public Slider hpslider;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject Player;
    float currentTime = 0f;
    private bool no_damage;
    float HP;
    // Start is called before the first frame update
    void Awake()
    {
        GameOverUI.SetActive(false);
    }
    void Start()
    {
        if (hpslider != null)
        {
            hpslider.maxValue = statusdata.MAXHP;
            hpslider.value = statusdata.MAXHP;
        }
        HP = statusdata.MAXHP;
        no_damage = false;
    }
    void Update()
    {
        hpslider.maxValue = statusdata.MAXHP;
        if (no_damage)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 0.2f)
            {
                currentTime = 0f;
                no_damage = false;
            }
        }
    }
    public void Damage(float damage)
    {
        if (!no_damage)
        {
            HP -= damage;
            no_damage = true;
            if (hpslider != null)
            {
                hpslider.value = HP;
            }
            if (HP <= 0)
            {
                Player.GetComponent<SpriteRenderer>().enabled = false;
                GameOverUI.SetActive(true);
                Time.timeScale = 0;
            }
        }

    }
    public void Heal(int heal)
    {
        if (hpslider != null)
        {
            if (statusdata.MAXHP >= HP)
            {
                HP += heal;
            }
            hpslider.value = HP;
        }
    }
}
