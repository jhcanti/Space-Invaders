using UnityEngine;

public class HealthPowerUp : PowerUp
{
    [SerializeField] private int health;
    
    protected override void DoInit()
    {
        Rb.velocity = -MyTransform.right * Speed;
    }

    protected override void DoMove()
    {
        
    }
    
    protected override void DoOnTriggerEnter(PlayerMediator player)
    {
        player.AddHealth(health);
        DoDestroy();
    }

    protected override void DoDestroy()
    {
        Destroy(gameObject);
    }
}
