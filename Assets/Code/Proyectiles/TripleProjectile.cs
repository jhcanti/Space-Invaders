using UnityEngine;

public class TripleProjectile : Projectile
{
    private Vector2 _direction;
    private Vector3 _currentPosition;


    public void Configure(Vector2 direction)
    {
        _direction = direction;
        Rb.velocity = direction * Speed;
    }
    
    protected override void DoInit()
    {
        
    }

    protected override void DoMove()
    {
        
    }

    protected override void DoDeactivate()
    {
        
    }
}
