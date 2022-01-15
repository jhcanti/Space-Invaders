public class PowerUpEnemy : Enemy
{
    protected override void DoInit()
    {
        Rb.velocity = -MyTransform.right * Speed;
    }

    protected override void DoTryShoot()
    {
        
    }

    protected override void DoMove()
    {
        
    }

    protected override void DoDestroy()
    {
        
    }
}
