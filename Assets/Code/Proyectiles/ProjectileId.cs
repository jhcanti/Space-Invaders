using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New projectile")]
public class ProjectileId : ScriptableObject
{
    [SerializeField] private string projectileId;
    [SerializeField] private Sprite icon;
    [SerializeField] private float fireRate;
    [SerializeField] private float durabilityInSeconds;

    public string Value => projectileId;
    public Sprite Icon => icon;
    public float FireRate => fireRate;
    public float DurabilityInSeconds => durabilityInSeconds;
}