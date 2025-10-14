using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrbScript : MonoBehaviour
{
    private int Exp;
    [SerializeField]
    private GameObject ExpManager;

    void Start()
    {
        Destroy(this.gameObject, 10f);
    }

    public void GetExpAmount(int exp)
    {
        Exp = exp;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ExpManager.GetComponent<EXPManager>().AddEXP(Exp);
            Destroy(this.gameObject, 0.3f);
        }
    }
}
