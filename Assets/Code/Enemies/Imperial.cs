public class Imperial : Enemy
{
    protected override void DoInit()
    {
        Rb.velocity = MyTransform.right * Speed;
    }

    protected override void DoMove()
    {
        
    }

    protected override void DoDestroy()
    {
        
    }

    protected override void DoTryShoot()
    {
        WeaponController.TryShoot();
    }
}
