using UnityEngine;

public class LinearProjectile : Projectile
{    
    protected override void DoInit(Vector3 position, Quaternion rotation)
    {
        MyTransform.position = position;
        MyTransform.rotation = rotation;
        Rb.velocity = MyTransform.right * Speed;
    }

    protected override void DoMove()
    {
        
    }

    protected override void DoDeactivate()
    {

    }
    
}