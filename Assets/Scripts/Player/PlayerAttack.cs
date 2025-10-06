using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject attackObj;
    [SerializeField] StatusData statusdata;
    private float attack;
    private float currentTime = 0f;
    private Vector2 attackPos;
    // Start is called before the first frame update
    void Start()
    {
        attack = statusdata.ATK;  
    }

    // Update is called once per frame
    void Update()
    {
        attackPos = transform.parent.gameObject.transform.position;
        currentTime += Time.deltaTime;
        if (currentTime >= 2.0f)
        {
            currentTime = 0f;
            Instantiate(attackObj, transform.position, Quaternion.identity);
            attackObj.GetComponent<BatAttack>().SetAttack(attack);
            attackObj.GetComponent<BatAttack>().SetPos(attackPos);
        }
    }
}
