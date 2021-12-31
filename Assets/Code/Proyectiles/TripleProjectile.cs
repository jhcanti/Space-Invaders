public class TripleProjectile : Projectile
{
    protected override void DoInit()
    {
        Rb.velocity = MyTransform.right * Speed;
    }

    protected override void DoMove()
    {
        
    }

    protected override void DoDeactivate()
    {
        
    }
}
