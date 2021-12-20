using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public Teams Team { get; private set; }

    [SerializeField] private EnemyId id;
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

    public string Id => id.Value;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        EnemyCollider = GetComponent<Collider2D>();
        Renderer = GetComponent<SpriteRenderer>();
        HealthController = GetComponent<HealthController>();
        WeaponController = GetComponent<WeaponController>();
        Team = Teams.Enemy;
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
    }

    protected abstract void DoMove();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.RecieveDamage(damageForImpact);
    }


    public void RecieveDamage(int amount)
    {
        var isDead = HealthController.ReciveDamage(amount);
        if (isDead)
        {
            Destroy(gameObject);
            var enemyDestroyedEvent = new EnemyDestroyedEvent(PointsToAdd, GetInstanceID());
            ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(enemyDestroyedEvent);
        }
    }
}
