using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [SerializeField] private ProjectilesConfiguration projectilesConfiguration;
    [SerializeField] private Transform projectileParentTransform;
    
    private ProjectileFactory _projectileFactory;
    private Queue<Projectile> _projectiles = new Queue<Projectile>();
    private List<string> _projectileIds;
    private Projectile _projectile;


    private void Awake()
    {
        Instance = this;
        var instance = Instantiate(projectilesConfiguration);
        _projectileFactory = new ProjectileFactory(instance);
    }

    private void Start()
    {
        _projectileIds = projectilesConfiguration.GetAllProjectileIds();
        foreach (var projectileId in _projectileIds)
        {
            AddProjectile(projectileId, 40);
        }
    }

    public Projectile Get(string id)
    {
        var found = false;
        while (!found)
        {
            _projectile = _projectiles.Dequeue();
            if (_projectile.Id != id)
            {
                _projectiles.Enqueue(_projectile);
            }
            else
            {
                if (_projectile.gameObject.activeInHierarchy == false)
                {
                    found = true;
                }
                else
                {
                    _projectiles.Enqueue(_projectile);
                }
            }
        }

        return _projectile;
    }

    private void AddProjectile(string id, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var projectile = _projectileFactory.Create(id, projectileParentTransform);
            projectile.gameObject.SetActive(false);
            _projectiles.Enqueue(projectile);
        }
    }

    public void ReturnToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        _projectiles.Enqueue(projectile);
    }
}
