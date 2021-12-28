using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public string Id => id.Value;
    
    [SerializeField] private PowerUpId id;
    [SerializeField] protected float Speed;
    
    protected Rigidbody2D Rb;
    protected Transform MyTransform;

    private Camera _camera;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void Start()
    {
        MyTransform = transform;
        DoInit();
    }

    protected abstract void DoInit();
    
    private void FixedUpdate()
    {
        DoMove();
        CheckLimits();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DoOnTriggerEnter(other.GetComponent<PlayerMediator>());
    }

    protected abstract void DoOnTriggerEnter(PlayerMediator player);
    
    protected abstract void DoMove();
    
    private void CheckLimits()
    {
        var viewportPosition = _camera.WorldToViewportPoint(Rb.position);
        if (viewportPosition.x < -0.05f)
        {
            DestroyPowerUp();
        }
    }

    private void DestroyPowerUp()
    {
        DoDestroy();
    }

    protected abstract void DoDestroy();
}
