using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private PlayerMediator prefab;
    [SerializeField] private Transform playerSpawnPosition;

    private PlayerMediator _player;
    private EventQueue _eventQueue;

    private void Start()
    {
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
    }

    public void SpawnPlayer()
    {
        _player = Instantiate(prefab, playerSpawnPosition.position, Quaternion.identity);
        _eventQueue.EnqueueEvent(new PlayerSpawnEvent());
    }

    public void SetPlayerInactive()
    {
        _player.IsActive = false;
    }

    public void DestroyPlayer()
    {
        Destroy(_player.gameObject);
    }
}
