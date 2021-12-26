using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Projectile configuration")]
public class ProjectilesConfiguration : ScriptableObject
{
    [SerializeField] private Projectile[] projectilePrefabs;
    private Dictionary<string, Projectile> _idToProjectilePrefab;
    private List<string> _projectileIds;

    private void Awake()
    {
        _idToProjectilePrefab = new Dictionary<string, Projectile>();
        _projectileIds = new List<string>();
        foreach (var projectile in projectilePrefabs)
        {
            _idToProjectilePrefab.Add(projectile.Id, projectile);
            _projectileIds.Add(projectile.Id);
        }
    }

    public Projectile GetProjectileById(string id)
    {
        if (!_idToProjectilePrefab.TryGetValue(id, out var projectile))
        {
            throw new Exception($"Projectile with Id {id} does not exists");
        }

        return projectile;
    }

    public List<string> GetAllProjectileIds()
    {
        return _projectileIds;
    }
}