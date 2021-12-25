using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New projectile")]
public class ProjectileId : ScriptableObject
{
    [SerializeField] private string projectileId;    

    public string Value => projectileId;
}