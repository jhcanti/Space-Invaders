using UnityEngine;

public class Giratorio : Enemy
{
    [SerializeField] private float xPositionToTurn;
    [SerializeField] private float yExtremeValue;
    [SerializeField] private float verticalSpeed;

    private Vector3 _originalPosition;
    private Vector3 _currentPosition;
    
    protected override void DoInit()
    {
        _currentPosition = MyTransform.position;
        _originalPosition = _currentPosition;
    }

    protected override void DoTryShoot()
    {
        
    }

    protected override void DoMove()
    {
        _currentPosition += MyTransform.right * (Speed * Time.deltaTime);

        if (MyTransform.position.x < xPositionToTurn)
        {
            _currentPosition.y += (verticalSpeed * Time.deltaTime);
            if (Mathf.Abs(_currentPosition.y - _originalPosition.y) > yExtremeValue)
            {
                verticalSpeed *= -1f;
            }
        }

        Rb.MovePosition(_currentPosition);
    }

    protected override void DoDestroy()
    {
        
    }
}
