using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public string Id => id.Value;
    
    [SerializeField] private PowerUpId id;
}
