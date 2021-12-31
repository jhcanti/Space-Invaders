using UnityEngine;

public class SetTriplePowerUp : PowerUp
{
    [SerializeField] private ProjectileId projectileId;
    
    protected override void DoInit()
    {
        Rb.velocity = -MyTransform.right * Speed;
    }

    protected override void DoOnTriggerEnter(PlayerMediator player)
    {
        player.SetProjectile(projectileId);
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
