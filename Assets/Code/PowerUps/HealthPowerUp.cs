public class HealthPowerUp : PowerUp
{
    protected override void DoInit()
    {
        Rb.velocity = -MyTransform.right * Speed;
    }

    protected override void DoMove()
    {
        
    }
}
