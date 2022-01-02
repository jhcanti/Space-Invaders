using UnityEngine;

public class PlayerMediator : MonoBehaviour, IDamageable
{
    public Teams Team { get; private set; }
    public bool IsActive { get; set; }

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private HealthController healthController;
    [SerializeField] private ShieldController shieldController;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxShield;

    private Collider2D _collider;
    private IInput _input;
    private Vector2 _direction;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _input = ServiceLocator.Instance.GetService<IInput>();
        healthController.Init(maxHealth, Teams.Ally);
        shieldController.Init(maxShield, Teams.Ally);
        Team = Teams.Ally;
        weaponController.Configure(Team);
        IsActive = true;
    }


    private void Update()
    {
        if (!IsActive) return;
        
        _direction = _input.GetDirection();

        if (_input.IsActionFirePressed())
        {
            weaponController.TryShoot();
        }
    }

    private void FixedUpdate()
    {
        if (!IsActive) return;
        
        playerMovement.Move(_direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.ReceiveDamage(100);
    }

    public void ReceiveDamage(int amount)
    {
        var remainingDamage = shieldController.ReceiveDamage(amount);

        if (remainingDamage > 0)
        {
            var isDead = healthController.ReceiveDamage(amount);
            if (isDead)
            {
                ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new PlayerDestroyedEvent());
                Destroy(gameObject);
            }    
        }
    }

    public void AddHealth(int amount)
    {
        healthController.Heal(amount);
    }

    public void AddShield(int amount)
    {
        shieldController.AddShield(amount);
    }

    public void SetProjectile(ProjectileId id)
    {
        weaponController.ChangeProjectile(id);
    }
}
