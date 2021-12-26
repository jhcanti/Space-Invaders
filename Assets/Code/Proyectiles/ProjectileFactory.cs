using UnityEngine;

public class ProjectileFactory
{
    private readonly ProjectilesConfiguration _projectileConfiguration;

    public ProjectileFactory(ProjectilesConfiguration projectileConfiguration)
    {
        _projectileConfiguration = projectileConfiguration;
    }

    public Projectile Create(string id, Transform projectileParentTransform)
    {
        var prefab = _projectileConfiguration.GetProjectileById(id);
        var projectile = Object.Instantiate(prefab, projectileParentTransform); 
        return projectile;
    }

}