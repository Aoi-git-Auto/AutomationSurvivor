using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : AbstractEnemy
{
    private Vector3 diff;
    private Vector3 direction;
    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        diff.x = playerPos.x - this.transform.position.x;
        if (diff.x > 0)
        {
            direction = new Vector3(0, -180, 0);
            this.transform.eulerAngles = direction;
        }
        if(diff.x < 0)
        {
            direction = new Vector3(0, 180, 0);
            this.transform.eulerAngles = direction;
        }
    }
}
