using UnityEngine;

public class CurveProjectile : Projectile
{
    private Vector3 _currentPosition;
    private float _currentTime;
    
    protected override void DoInit()
    {
        _currentTime = 0;
        _currentPosition = MyTransform.position;
    }

    protected override void DoMove()
    {
        _currentPosition += MyTransform.right * (Speed * Time.deltaTime);
        var verticalPosition = MyTransform.up * VerticalMovement.Evaluate(_currentTime);
        Rb.MovePosition(_currentPosition + verticalPosition);
        _currentTime += Time.deltaTime;
    }

    protected override void DoDeactivate()
    {
        
    }
}
