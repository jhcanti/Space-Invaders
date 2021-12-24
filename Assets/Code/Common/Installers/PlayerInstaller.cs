using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private PlayerMediator prefab;
    [SerializeField] private Transform playerSpawnPosition;

    private PlayerMediator _player;

    public void SpawnPlayer()
    {
        _player = Instantiate(prefab, playerSpawnPosition.position, Quaternion.identity);
        ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new RestartLevelCompleteEvent());
    }

    public void SetPlayerInactive()
    {
        _player.IsActive = false;
    }
}
