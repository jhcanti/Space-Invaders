using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IEventObserver
{
    public Teams Team { get; private set; }
    public string Id => id.Value;

    [SerializeField] private EnemyId id;
    [SerializeField] private int damageForImpact;

    protected Rigidbody2D Rb;
    protected Collider2D EnemyCollider;
    protected SpriteRenderer Renderer;
    protected HealthController HealthController;
    protected WeaponController WeaponController;
    protected int Health;
    protected float Speed;
    protected int PointsToAdd;
    protected Transform MyTransform;
    private Camera _camera;
    private PowerUpProbability[] _powerUpProbabilities;
    private int _previousInstance;
    private int _previousFrame;


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

    public void Configure(int health, float speed, int pointsToAdd, PowerUpProbability[] powerUpProbabilities)
    {
        MyTransform = transform;
        Health = health;
        Speed = speed;
        PointsToAdd = pointsToAdd;
        _powerUpProbabilities = powerUpProbabilities;
        HealthController.Init(Health, Teams.Enemy);
        WeaponController.Configure(Team);
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
        
        var isDead = HealthController.ReceiveDamage(amount);
        if (isDead)
        {
            EnemyCollider.enabled = false;
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

    protected void DestroyEnemy(int points)
    {
        var instance = gameObject.GetInstanceID();
        if (instance == _previousInstance && Time.frameCount == _previousFrame)
            return;

        _previousInstance = instance;
        _previousFrame = Time.frameCount;
        DoDestroy();
        var enemyDestroyedEvent = new EnemyDestroyedEvent(points);
        ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(enemyDestroyedEvent);
        
        if (points > 0)
        {
            // aqui instanciaremos la explosión

            CalculatePowerUpSpawn();
        }
        
        Destroy(gameObject);
    }

    protected abstract void DoDestroy();
    
    
    private void CalculatePowerUpSpawn()
    {
        var number = Random.Range(0f, 100f);

        foreach (var powerUpProbability in _powerUpProbabilities)
        {
            if (number >= powerUpProbability.MinimumRange && number <= powerUpProbability.MaximumRange)
            {
                var spawnPowerUpEvent = new SpawnPowerUpEvent(powerUpProbability.PowerUpId.Value, transform.position);
                ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(spawnPowerUpEvent);
                return;
            }
        }
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
