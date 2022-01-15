using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 speed;

    private Rigidbody2D _rb;
    private Camera _camera;
    private Vector2 _currentPosition;
    private float _turnAmount;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentPosition = _rb.position;
        _camera = Camera.main;
    }

    public void Move(Vector2 direction)
    {
        _currentPosition += direction * (speed * Time.deltaTime);
        _currentPosition = ClampFinalPosition(_currentPosition);
        _rb.MovePosition(_currentPosition);
        _turnAmount = direction.y;
        animator.SetFloat("Turn", _turnAmount);
    }

    private Vector2 ClampFinalPosition(Vector2 position)
    {
        var viewportPoint = _camera.WorldToViewportPoint(position);
        viewportPoint.x = Mathf.Clamp(viewportPoint.x, 0.06f, 0.95f);
        viewportPoint.y = Mathf.Clamp(viewportPoint.y, 0.15f, 0.94f);
        return _camera.ViewportToWorldPoint(viewportPoint);
    }
}
