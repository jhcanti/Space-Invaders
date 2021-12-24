using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory
{
    private readonly ProjectilesConfiguration _projectileConfiguration;
    private Dictionary<string, ObjectPool> _tagToObjectPool;


    public ProjectileFactory(ProjectilesConfiguration projectileConfiguration)
    {
        _projectileConfiguration = projectileConfiguration;
    }

    public void Init()
    {
        _tagToObjectPool = new Dictionary<string, ObjectPool>();
        foreach (var projectile in _projectileConfiguration.GetAllProjectiles())
        {
            var objectPool = new ObjectPool(projectile.Key, projectile.Value);
            objectPool.Init();
            _tagToObjectPool.Add(projectile.Key, objectPool);
        }
    }

    public Projectile SpawnFromPool(string id, Vector3 position, Quaternion rotation, Transform projectileParentTransform, Teams team)
    {
        if (!_tagToObjectPool.TryGetValue(id, out var pool))
        {
            throw new Exception($"Pool with Id {id} does not exists");
        }

        var projectile = pool.SpawnFromPool(position, rotation, projectileParentTransform, team);
        return projectile;
    }

}