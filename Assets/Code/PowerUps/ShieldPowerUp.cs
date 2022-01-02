using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    [SerializeField] private int shieldToAdd;
    
    
    protected override void DoInit()
    {
        Rb.velocity = -MyTransform.right * Speed;
    }

    protected override void DoOnTriggerEnter(PlayerMediator player)
    {
        player.AddShield(shieldToAdd);
        DoDestroy();
    }

    protected override void DoMove()
    {
        
    }

    protected override void DoDestroy()
    {
        Destroy(gameObject);
    }
}
