using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private string _tag;
    private Projectile _projectilePrefab;
    private const int SIZE = 10;
    private Queue<Projectile> _objectPool;


    public ObjectPool(string tag, Projectile prefab)
    {
        _tag = tag;
        _projectilePrefab = prefab;
    }

    public void Init()
    {
        _objectPool = new Queue<Projectile>();
        for (int i = 0; i < SIZE; i++)
        {
            var obj = Object.Instantiate(_projectilePrefab);
            obj.gameObject.SetActive(false);
            _objectPool.Enqueue(obj);
        }
    }
    
    public Projectile SpawnFromPool(Vector3 position, Quaternion rotation, Transform projectileParentTransform,
        Teams team)
    {
        var obj = _objectPool.Dequeue();

        if (obj.gameObject.activeInHierarchy)
        {
            _objectPool.Enqueue(obj);
            var newObj = Object.Instantiate(_projectilePrefab, projectileParentTransform);
            _objectPool.Enqueue(newObj);
            newObj.Init(position, rotation, team);
            return newObj;
        }

        obj.gameObject.SetActive(true);
        obj.Init(position, rotation, team);
        _objectPool.Enqueue(obj);
        return obj;
    }
}
