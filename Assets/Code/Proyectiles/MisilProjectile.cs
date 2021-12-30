using UnityEngine;

public class MisilProjectile : Projectile
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float radius;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layerMask;

    private Transform _target;
    
    
    protected override void DoInit()
    {
        Rb.velocity = MyTransform.right * Speed;
    }

    protected override void DoMove()
    {
        if (_target == null)
        {
            var results = Physics2D.CircleCastAll(MyTransform.position, radius,
                MyTransform.right, distance, layerMask);

            if (results.Length == 0) return;
            
            foreach (var raycastHit2D in results)
            {
                if (raycastHit2D.point.x > MyTransform.position.x)
                {
                    _target = raycastHit2D.transform;
                    break;
                }
            }    
        }

        if (_target == null) return;
        
        var direction = (Vector2) _target.position - Rb.position;
        direction.Normalize();
        var rotateAmount = Vector3.Cross(direction, MyTransform.right).z;
        Rb.angularVelocity = -rotateAmount * rotateSpeed;
        Rb.velocity = MyTransform.right * Speed;
    }

    protected override void DoDeactivate()
    {
        
    }
}
