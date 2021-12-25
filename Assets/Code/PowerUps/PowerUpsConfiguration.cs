using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/PowerUp configuration")]
public class PowerUpsConfiguration : ScriptableObject
{
    [SerializeField] private PowerUp[] powerUpPrefabs;
    private Dictionary<string, PowerUp> _idToPowerUpPrefab;

    private void Awake()
    {
        _idToPowerUpPrefab = new Dictionary<string, PowerUp>();
        foreach (var powerUp in powerUpPrefabs)
        {
            _idToPowerUpPrefab.Add(powerUp.Id, powerUp);
        }
    }
    
    public PowerUp GetPowerUpById(string id)
    {
        if (!_idToPowerUpPrefab.TryGetValue(id, out var powerUp))
        {
            throw new Exception($"PowerUp with Id {id} does not exists");
        }

        return powerUp;
    }
}
