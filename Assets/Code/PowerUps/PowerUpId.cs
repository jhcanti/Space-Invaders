using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/New powerUp")]
public class PowerUpId : ScriptableObject
{
    [SerializeField] private string powerUpId;    

    public string Value => powerUpId;
    
}
