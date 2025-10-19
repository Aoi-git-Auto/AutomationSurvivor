using UnityEngine;

public class PowerItem : AbstractStatusItem
{
    [SerializeField]
    private float powerAmount;
    private GameObject playerAttack;
    private void Start()
    {
        playerAttack = GameObject.Find("Normal_ATK");
    }

    protected override void Enhance(GameObject player)
    {
        if (player.CompareTag("Player"))
        {
            playerAttack.GetComponent<PlayerAttack>().AddPower(powerAmount);
            Destroy(this.gameObject);
        }
    }
}
