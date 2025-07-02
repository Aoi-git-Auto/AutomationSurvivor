using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Move : MonoBehaviour
{
    [SerializeField] StatusData statusdata;
    [SerializeField] GameObject Hitmark;
    [SerializeField] GameObject EXP_prehub;
    [SerializeField] Chest chest_prehub;
    public bool once;
    GameObject Player;
    bool no_damage;
    private float HP;
    private float currentTime = 0.0f;
    float LifeTimeCount = 0f;
    Vector3 PlayerPos;
    Vector3 diff;
    Vector3 vector;
    private Rigidbody2D rb;
    public bool clearbool;
    [SerializeField] GameObject conffeti;
    [SerializeField] GameObject GameClearUI;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPos = Player.transform.position;
        this.transform.LookAt(PlayerPos);
        HP = statusdata.MAXHP;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = Player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, PlayerPos, statusdata.SPEED * Time.deltaTime);
        diff.x = PlayerPos.x - this.transform.position.x;
        if (diff.x > 0)
        {
            vector = new Vector3(0, -180, 0);
            this.transform.eulerAngles = vector;
        }
        if (diff.x < 0)
        {
            vector = new Vector3(0, 0, 0);
            this.transform.eulerAngles = vector;
        }
        if (no_damage)
        {
            currentTime += Time.deltaTime;
            if (currentTime > statusdata.SPAN)
            {
                currentTime = 0.0f;
                no_damage = false;
                rb.velocity = new Vector2(0, 0);
            }
        }
        if (HP <= 0)
        {
            clearbool = false;
            Player.gameObject.GetComponent<PlayerHP>().Heal(20);
            GameObject effect = Instantiate(Hitmark, transform.position, Quaternion.identity);
            LifeTimeCount += Time.deltaTime;
            DropExp();
            if (chest_prehub != null)
            {
                var Chest = Instantiate(chest_prehub, transform.position, transform.rotation);
            }
            Destroy(effect, 0.1f);
            Destroy(this.gameObject);
        }
        if (statusdata.BOSS == true && clearbool == false)
        {
            clearbool = true;
            GameClearUI = GameObject.Find("GameClearUI");
            GameClearUI.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
            var conffe = Instantiate(conffeti, this.transform.position, transform.rotation);
            StartCoroutine("GameClearFunc");
            for (int i = 0; i < 100; i++)
            {
                Instantiate(conffeti, this.transform.position, transform.rotation);
            }
        }
    }
    IEnumerator GameClearFunc()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
    }
    public void Damage(float damage)
    {
        GameObject effect = Instantiate(Hitmark, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        HP -= damage;
    }
    IEnumerator StopKnockBack(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;        
    }
    public void NockBack(float nockback)
    {
        Vector2 direction = (transform.position - PlayerPos).normalized;
        rb.velocity = direction * nockback;
        StartCoroutine(StopKnockBack(0.5f));
    }
    void DropExp()
    {
        for (int i = 0; statusdata.EXP > i; i++)
        {
            Instantiate(EXP_prehub, this.transform.position, this.transform.rotation);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHP>().Damage(statusdata.ATK);
        }
    }
}
