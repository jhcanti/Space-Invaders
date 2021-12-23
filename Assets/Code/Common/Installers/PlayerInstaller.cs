using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private PlayerMediator prefab;
    [SerializeField] private Transform playerSpawnPosition;


    public void SpawnPlayer()
    {
        var player = Instantiate(prefab, playerSpawnPosition.position, Quaternion.identity);
        ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new RestartLevelCompleteEvent());
    }
}
