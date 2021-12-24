using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour, IEventObserver
{
    public Teams Team { get; private set; }
    public string Id => id.Value;

    [SerializeField] private ProjectileId id;
    [SerializeField] protected float Speed;
    [SerializeField] private int damage;

    protected Rigidbody2D Rb;
    protected Collider2D Collider2D;
    protected Transform MyTransform;
    protected bool Active;
    
    private Camera _camera;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
        _camera = Camera.main;
    }

    public void Init(Vector3 position, Quaternion rotation, Teams team)
    {
        MyTransform = transform;
        Active = true;
        Team = team;
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.GameOver, this);
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.Victory, this);
        DoInit(position, rotation);
    }

    protected abstract void DoInit(Vector3 position, Quaternion rotation);

    private void FixedUpdate()
    {
        DoMove();
        CheckLimits();
    }
    
    protected abstract void DoMove();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (damageable == null) return;

        if (damageable.Team != Team)
        {
            damageable.ReceiveDamage(damage);
            DeactivateProjectile();
        }
            
    }
    
    private void CheckLimits()
    {
        var viewportPosition = _camera.WorldToViewportPoint(Rb.position);
        if (viewportPosition.x < -0.02f || viewportPosition.x > 1.02f)
            DeactivateProjectile();
    }

    private void DeactivateProjectile()
    {
        DoDeactivate();
        Active = false;
        gameObject.SetActive(false);
    }

    protected abstract void DoDeactivate();
    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.GameOver || eventData.EventId == EventIds.Victory)
        {
            DeactivateProjectile();
        }
    }
}