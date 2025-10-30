using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : AbstractStatusItem
{
    [SerializeField]
    private float speedAmouunt;

    protected override void Enhance(GameObject player)
    {
        player.GetComponent<PlayerController>().AddSpeed(speedAmouunt);
        Destroy(this.gameObject);
    }
}
