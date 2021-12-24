using UnityEngine;

public class EnemyFactory
{
    private EnemiesConfiguration _enemiesConfiguration;

    public EnemyFactory(EnemiesConfiguration enemiesConfiguration)
    {
        _enemiesConfiguration = enemiesConfiguration;
    }

    public Enemy Create(string id, Vector3 position, Quaternion rotation, Transform enemiesParentTransform)
    {
        var prefab = _enemiesConfiguration.GetEnemyById(id);
        var enemy = Object.Instantiate(prefab, position, rotation, enemiesParentTransform);
        return enemy;
    }
}
