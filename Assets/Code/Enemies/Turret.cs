public class Turret : Enemy
{
    protected override void DoInit()
    {
        Rb.velocity = -MyTransform.right * Speed;
    }

    protected override void DoTryShoot()
    {
        WeaponController.TryShoot();
    }

    protected override void DoMove()
    {
        
    }

    protected override void DoDestroy()
    {
        
    }
}
