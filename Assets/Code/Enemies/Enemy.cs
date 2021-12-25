using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IEventObserver
{
    public Teams Team { get; private set; }
    public string Id => id.Value;

    [SerializeField] private EnemyId id;
    [SerializeField] protected AnimationCurve VerticalMovement;
    [SerializeField] private int damageForImpact;

    protected Rigidbody2D Rb;
    protected Collider2D EnemyCollider;
    protected SpriteRenderer Renderer;
    protected HealthController HealthController;
    protected WeaponController WeaponController;
    protected int Health;
    protected float Speed;
    protected float FireRate;
    protected int PointsToAdd;
    protected Transform MyTransform;
    private Camera _camera;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        EnemyCollider = GetComponent<Collider2D>();
        Renderer = GetComponent<SpriteRenderer>();
        HealthController = GetComponent<HealthController>();
        WeaponController = GetComponent<WeaponController>();
        Team = Teams.Enemy;
        _camera = Camera.main;
    }

    public void Configure(int health, float speed, float fireRate, int pointsToAdd)
    {
        MyTransform = transform;
        Health = health;
        Speed = speed;
        FireRate = fireRate;
        PointsToAdd = pointsToAdd;
        HealthController.Init(Health);
        WeaponController.Configure(FireRate, Team);
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.GameOver, this);
        DoInit();
    }

    protected abstract void DoInit();

    private void Update()
    {
        DoTryShoot();    
    }

    protected abstract void DoTryShoot();

    private void FixedUpdate()
    {
        DoMove();
        CheckLimits();
    }
    

    protected abstract void DoMove();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.ReceiveDamage(damageForImpact);
    }


    public void ReceiveDamage(int amount)
    {
        var viewportPosition = _camera.WorldToViewportPoint(Rb.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1) return;
        
        var isDead = HealthController.ReciveDamage(amount);
        if (isDead)
        {
            DestroyEnemy(PointsToAdd);
        }
    }
    
    private void CheckLimits()
    {
        var viewportPosition = _camera.WorldToViewportPoint(Rb.position);
        if (viewportPosition.x < -0.05f)
        {
            DestroyEnemy(0);
        }
    }

    private void DestroyEnemy(int points)
    {
        Destroy(gameObject);
        var enemyDestroyedEvent = new EnemyDestroyedEvent(points);
        ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(enemyDestroyedEvent);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.GameOver, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.GameOver)
        {
            DestroyEnemy(0);
        }
    }
}
