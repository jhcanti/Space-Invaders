using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _speed;

    private Rigidbody2D _rb;
    private Camera _camera;
    private Vector2 _currentPosition;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentPosition = _rb.position;
        _camera = Camera.main;
    }

    public void Move(Vector2 direction)
    {
        _currentPosition += direction * (_speed * Time.deltaTime);
        _currentPosition = ClampFinalPosition(_currentPosition);
        _rb.MovePosition(_currentPosition);
    }

    private Vector2 ClampFinalPosition(Vector2 position)
    {
        var viewportPoint = _camera.WorldToViewportPoint(position);
        viewportPoint.x = Mathf.Clamp(viewportPoint.x, 0.05f, 0.95f);
        viewportPoint.y = Mathf.Clamp(viewportPoint.y, 0.05f, 0.95f);
        return _camera.ViewportToWorldPoint(viewportPoint);
    }
}
