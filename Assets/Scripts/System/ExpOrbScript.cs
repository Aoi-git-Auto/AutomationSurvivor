using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpOrbScript : MonoBehaviour
{
    public int EXP;
    int Exp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            Exp++;
            EXPManager.instance.ExpBarDraw();
            Destroy(this.gameObject,0.3f);
        }
    }
}
