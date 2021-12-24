using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ProjectilesConfiguration projectilesConfiguration;
    [SerializeField] private ProjectileId defaultProjectile;
    
    private Transform _projectileSpawnPoint;
    private Transform _projectileParentTransform;
    private ProjectileFactory _projectileFactory;
    private float fireRate;
    private float _timeBetweenShoots;
    private Teams _team;

    private void Awake()
    {
        var instance = Instantiate(projectilesConfiguration);
        _projectileFactory = new ProjectileFactory(instance);
        _projectileFactory.Init();
        _projectileParentTransform = GameObject.FindWithTag("ProjectilesParent").transform;
        var shootPoint = transform.Find("ProjectileSpawnPoint");
        if (shootPoint != null)
            _projectileSpawnPoint = shootPoint;
    }

    public void Configure(float fireRate, Teams team)
    {
        this.fireRate = fireRate;
        _team = team;
    }
    
    public void TryShoot()
    {
        if (Time.time > _timeBetweenShoots)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var projectile = _projectileFactory.SpawnFromPool(defaultProjectile.Value, _projectileSpawnPoint.position,
                        _projectileSpawnPoint.rotation, _projectileParentTransform, _team);
        _timeBetweenShoots = Time.time + fireRate;
    }
}
