using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : AbstractStatusItem
{
    [SerializeField]
    private float healAmount;

    void Start()
    {
        Destroy(this.gameObject, 10f);
    }
    
    protected override void Enhance(GameObject player)
    {
        player.GetComponent<PlayerController>().Heal(healAmount);
        Destroy(this.gameObject);
    }
}
