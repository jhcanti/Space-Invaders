using UnityEngine;

public abstract class PowerUp : MonoBehaviour, IEventObserver
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
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.GameOver, this);
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.Victory, this);
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
    
    
    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.GameOver, this);
        ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.Victory, this);
    }
    
    
    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.GameOver || eventData.EventId == EventIds.Victory)
        {
            DestroyPowerUp();
        }
    }
}
