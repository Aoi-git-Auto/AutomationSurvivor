using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject attackObj;
    [SerializeField] 
    private StatusData statusdata;
    private float attack;
    private float nockback;
    private float currentTime = 0f;
    private Vector2 attackPos;
    private GameObject player;

    [SerializeField]
    private AudioClip statusUpSE;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        attack = statusdata.ATK;
        nockback = statusdata.NockBack;
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null)
        {
            this.transform.SetParent(player.transform);
        }

        attackPos = transform.parent.position;
        currentTime += Time.deltaTime;
        if (currentTime >= 2.0f)
        {
            currentTime = 0f;
            var bat = Instantiate(attackObj, transform.position, Quaternion.identity);
            bat.transform.SetParent(transform.parent);
            bat.GetComponent<BatAttack>().SetStatus(attack, nockback, attackPos);
        }
    }

    public void AddPower(float power)
    {
        audioSource.PlayOneShot(statusUpSE);
        attack += power;
        Debug.Log("Power UP");
    }
}
