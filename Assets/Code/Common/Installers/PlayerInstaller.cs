using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private PlayerMediator prefab;
    [SerializeField] private Transform playerSpawnPosition;

    private PlayerMediator _player;
    

    public void SpawnPlayer()
    {
        _player = Instantiate(prefab, playerSpawnPosition.position, Quaternion.identity);
    }

    public void SetPlayerInactive()
    {
        _player.IsActive = false;
    }

    public void SetPlayerActive()
    {
        _player.IsActive = true;
    }
    
    public void ResetPlayerPosition()
    {
        _player.transform.position = playerSpawnPosition.position;
    }
}
