using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New projectile")]
public class ProjectileId : ScriptableObject
{
    [SerializeField] private string _projectileId;    

    public string Value => _projectileId;
}