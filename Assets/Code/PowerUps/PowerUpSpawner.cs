using UnityEngine;

public class PowerUpSpawner : MonoBehaviour, IEventObserver
{
    [SerializeField] private PowerUpsConfiguration powerUpsConfiguration;
    [SerializeField] private Transform powerUpParentTransform;
    
    private PowerUpFactory _powerUpFactory;
    private EventQueue _eventQueue;
    
    
    private void Awake()
    {
        var instance = Instantiate(powerUpsConfiguration);
        _powerUpFactory = new PowerUpFactory(instance);
    }

    private void Start()
    {
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        _eventQueue.Subscribe(EventIds.SpawnPowerUp, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.SpawnPowerUp)
        {
            var spawnPowerUpData = (SpawnPowerUpEvent) eventData;
            var powerUp = _powerUpFactory.Create(spawnPowerUpData.PowerUpId, spawnPowerUpData.SpawnPosition,
                Quaternion.identity, powerUpParentTransform);
        }
    }

    private void OnDestroy()
    {
        _eventQueue.Unsubscribe(EventIds.SpawnPowerUp, this);
    }
}
