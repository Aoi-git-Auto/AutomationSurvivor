using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject attackObj;
    [SerializeField] StatusData statusdata;
    private float attack;
    private float nockback;
    private float currentTime = 0f;
    private Vector2 attackPos;
    // Start is called before the first frame update
    void Start()
    {
        attack = statusdata.ATK;
        nockback = statusdata.NockBack;
    }

    // Update is called once per frame
    void Update()
    {
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
        attack += power;
    }
}
