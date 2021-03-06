using UnityEngine;

public class Phantom : Enemy
{
    [SerializeField] protected AnimationCurve verticalMovement;

    private Vector3 _currentPosition;
    private float _currentTime;
    
    protected override void DoInit()
    {
        _currentTime = 0;
        _currentPosition = MyTransform.position;
    }

    protected override void DoTryShoot()
    {
        WeaponController.TryShoot();
    }

    protected override void DoMove()
    {
        _currentPosition += MyTransform.right * (Speed * Time.deltaTime);
        var verticalPosition = MyTransform.up * verticalMovement.Evaluate(_currentTime);
        Rb.MovePosition(_currentPosition + verticalPosition);
        _currentTime += Time.deltaTime;
    }

    protected override void DoDestroy()
    {
        
    }
}
