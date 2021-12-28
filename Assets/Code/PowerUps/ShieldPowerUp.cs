public class ShieldPowerUp : PowerUp
{
    protected override void DoInit()
    {
        Rb.velocity = -MyTransform.right * Speed;
    }

    protected override void DoOnTriggerEnter(PlayerMediator player)
    {
        player.ActivateShield();
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
