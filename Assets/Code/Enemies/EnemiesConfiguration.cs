using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Enemy configuration")]
public class EnemiesConfiguration : ScriptableObject
{
    [SerializeField] private Enemy[] enemyPrefabs;
    private Dictionary<string, Enemy> _idToEnemyPrefab;

    private void Awake()
    {
        _idToEnemyPrefab = new Dictionary<string, Enemy>();
        foreach (var enemy in enemyPrefabs)
        {
            _idToEnemyPrefab.Add(enemy.Id, enemy);
        }
    }

    public Enemy GetEnemyById(string id)
    {
        if (!_idToEnemyPrefab.TryGetValue(id, out var enemy))
        {
            throw new Exception($"Enemy with Id {id} does not exists");
        }

        return enemy;
    }
}
