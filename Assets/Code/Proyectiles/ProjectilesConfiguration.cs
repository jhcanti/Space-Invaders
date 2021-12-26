using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Projectile configuration")]
public class ProjectilesConfiguration : ScriptableObject
{
    public Dictionary<string, Projectile> IdToProjectilePrefabs => _idToProjectilePrefab;
    
    [SerializeField] private Projectile[] projectilePrefabs;
    private Dictionary<string, Projectile> _idToProjectilePrefab;

    private void Awake()
    {
        _idToProjectilePrefab = new Dictionary<string, Projectile>();
        foreach (var projectile in projectilePrefabs)
        {
            _idToProjectilePrefab.Add(projectile.Id, projectile);
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
}