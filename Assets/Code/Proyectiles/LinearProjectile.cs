using UnityEngine;

public class LinearProjectile : Projectile
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