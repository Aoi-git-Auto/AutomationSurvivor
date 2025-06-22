using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_defATK : MonoBehaviour
{
    public static Player_defATK DEF_ATK;
    [SerializeField] StatusData statusdata;
    public float PlayerATK;
    public static GMScript instance;

    // Start is called before the first frame update
    void Start()
    {
        if (DEF_ATK == null)
        {
            DEF_ATK = this;
        }
        if (GMScript.instance != null)
        {
            PlayerATK = GMScript.instance.PlayerDefaultATK;
        }
        else
        {
            Debug.LogWarning("nullぽ");
        }
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
