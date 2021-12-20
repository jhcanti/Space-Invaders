using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ProjectilesConfiguration projectilesConfiguration;
    [SerializeField] private ProjectileId defaultProyectile;
    [SerializeField] private Transform projectileSpawnpoint;

    private ProjectileFactory _projectileFactory;
    private float fireRate;
    private float _timeBetweenShoots;
    private Teams _team;

    private void Awake()
    {
        var instance = Instantiate(projectilesConfiguration);
        _projectileFactory = new ProjectileFactory(instance);
        _projectileFactory.Init();
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
        var projectile = _projectileFactory.SpawnFromPool(defaultProyectile.Value, projectileSpawnpoint.position, projectileSpawnpoint.rotation, _team);
        _timeBetweenShoots = Time.time + fireRate;
    }
}
