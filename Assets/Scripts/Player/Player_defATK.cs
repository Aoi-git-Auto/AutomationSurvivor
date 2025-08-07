using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_defATK : MonoBehaviour
{
    [SerializeField] StatusData statusdata;
    public float PlayerATK;


    // Start is called before the first frame update
    void Start()
    {
        PlayerATK = statusdata.ATK;
    } 

    // Update is called once per frame
    void Update() {}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<Enemy_Move>().Damage(PlayerATK);
            other.gameObject.GetComponent<Enemy_Move>().NockBack(statusdata.NockBack);
        }
    }
}
