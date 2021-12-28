using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [SerializeField] private ProjectilesConfiguration projectilesConfiguration;
    [SerializeField] private Transform projectileParentTransform;
    [SerializeField] private int poolSize = 10;

    private ProjectilesConfiguration _projectilesConfiguration;
    private ProjectileFactory _projectileFactory;
    private Dictionary<string, Queue<Projectile>> _poolDictionary = new Dictionary<string, Queue<Projectile>>();
    private Projectile _projectile;


    private void Awake()
    {
        Instance = this;
        _projectilesConfiguration = Instantiate(projectilesConfiguration);
        _projectileFactory = new ProjectileFactory(_projectilesConfiguration);
    }

    private void Start()
    {
        foreach (var projectileType in _projectilesConfiguration.IdToProjectilePrefabs)
        {
            var projectilePool = new Queue<Projectile>();
            AddProjectile(projectileType.Key, projectilePool, poolSize);
            _poolDictionary.Add(projectileType.Key, projectilePool);
        }
    }

    
    public Projectile Get(string id)
    {
        var pool = _poolDictionary[id];
        if (pool.Count == 0)
            AddProjectile(id, pool, 1);
        
        return pool.Dequeue();
    }
    
    
    private void AddProjectile(string id, Queue<Projectile> pool, int size)
    {
        for (var i = 0; i < size; i++)
        {
            var projectile = _projectileFactory.Create(id, projectileParentTransform);
            projectile.gameObject.SetActive(false);
            pool.Enqueue(projectile);
        }
    }

    
    public void ReturnToPool(Projectile projectile, string id)
    {
        projectile.gameObject.SetActive(false);
        var pool = _poolDictionary[id];
        pool.Enqueue(projectile);
    }
    
}
