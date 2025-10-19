using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : AbstractStatusItem
{
    [SerializeField]
    private int healAmount;
    protected override void Enhance(GameObject player)
    {
        player.GetComponent<PlayerController>().Heal(healAmount);
    }
}
