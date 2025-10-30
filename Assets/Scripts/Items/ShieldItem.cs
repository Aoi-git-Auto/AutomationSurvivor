using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : AbstractStatusItem
{    
    protected override void Enhance(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            target.GetComponent<PlayerController>().SetShiled();
            Destroy(this.gameObject);
        }
    }
}
