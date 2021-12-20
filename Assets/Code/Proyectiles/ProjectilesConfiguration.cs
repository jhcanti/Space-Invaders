using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Projectile configuration")]
public class ProjectilesConfiguration : ScriptableObject
{
    [SerializeField] private Projectile[] _projectilePrefabs;
    private Dictionary<string, Projectile> _idToProjectilePrefab;

    private void Awake()
    {
        _idToProjectilePrefab = new Dictionary<string, Projectile>();
        foreach (var projectile in _projectilePrefabs)
        {
            _idToProjectilePrefab.Add(projectile.Id, projectile);
        }
    }

    public Dictionary<string, Projectile> GetAllProjectiles()
    {
        return _idToProjectilePrefab;
    }
}