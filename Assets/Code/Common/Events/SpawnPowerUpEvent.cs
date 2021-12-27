using UnityEngine;

public class SpawnPowerUpEvent : EventData
{
    public readonly string PowerUpId;
    public readonly Vector3 SpawnPosition;
    
    public SpawnPowerUpEvent(string id, Vector3 position) : base(EventIds.SpawnPowerUp)
    {
        PowerUpId = id;
        SpawnPosition = position;
    }
}
