using UnityEngine;

public class Giratorio2 : Enemy
{
    [SerializeField] private float xPositionToChase;
    [SerializeField] private float verticalSpeed;
    
    private Vector3 _currentPosition;
    private Transform _target;
    private Vector2 _targetPosition;
    
    
    protected override void DoInit()
    {
        _currentPosition = MyTransform.position;
        _target = GameObject.FindWithTag("Player").transform;
    }

    protected override void DoTryShoot()
    {
        
    }

    protected override void DoMove()
    {
        _currentPosition += MyTransform.right * (Speed * Time.deltaTime);
        _targetPosition = _target.position;

        if (MyTransform.position.x < xPositionToChase)
        {
            var direction = _targetPosition - (Vector2)_currentPosition;
            direction.Normalize();
            _currentPosition.y += (direction.y * verticalSpeed * Time.deltaTime);
        }
        
        Rb.MovePosition(_currentPosition);
    }

    protected override void DoDestroy()
    {
        
    }
}
