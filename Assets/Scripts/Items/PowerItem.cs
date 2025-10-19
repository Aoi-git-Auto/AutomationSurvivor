using UnityEngine;

public class PowerItem : AbstractStatusItem
{
    [SerializeField]
    private float powerAmount;
    private PlayerAttack playerAttack;
    private void Start()
    {
        playerAttack = GetComponentInChildren<PlayerAttack>();
    }

    protected override void Enhance(GameObject player)
    {
        if (player.CompareTag("Player"))
        {
            playerAttack.AddPower(powerAmount);
            Destroy(this.gameObject);
        }
    }
}
