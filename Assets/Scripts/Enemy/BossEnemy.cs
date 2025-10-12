using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : AbstractEnemy
{
    [SerializeField]
    private GameObject hpBar;
    private Vector3 diff;
    private Vector3 direction;
    private BossHPBarContoroller bar;

    protected new void Start()
    {
        base.Start();
        GameObject canvas = GameObject.Find("Canvas");

        var hp = Instantiate(hpBar, canvas.transform);
        bar = hp.GetComponent<BossHPBarContoroller>();

        bar.InitializeHPbar(MaxHP);
    }

    private void Update()
    {
        bar.UpdateHP(MaxHP);
    }

    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        diff.x = playerPos.x - this.transform.position.x;
        if (diff.x > 0)
        {
            direction = new Vector3(0, -180, 0);
            this.transform.eulerAngles = direction;
        }
        if (diff.x < 0)
        {
            direction = new Vector3(0, 180, 0);
            this.transform.eulerAngles = direction;
        }
    }
}
