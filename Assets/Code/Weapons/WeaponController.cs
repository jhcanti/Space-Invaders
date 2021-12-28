using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ProjectileId defaultProjectile;
    
    private Transform _projectileSpawnPoint;
    private ProjectileId _activeProjectile;
    private float fireRate;
    private float _timeBetweenShoots;
    private Teams _team;

    private void Awake()
    {
        _activeProjectile = defaultProjectile;  // el Player podrá cambiar despues el arma
        var shootPoint = transform.Find("ProjectileSpawnPoint");
        if (shootPoint != null)
            _projectileSpawnPoint = shootPoint;
    }

    public void Configure(float fireRate, Teams team)
    {
        this.fireRate = fireRate;
        _team = team;
    }
    
    
    public void ChangeProjectile(ProjectileId id)
    {
        _activeProjectile = id;
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
        var projectile = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile.gameObject.SetActive(true);
        projectile.Init(_projectileSpawnPoint, _team);
        _timeBetweenShoots = Time.time + fireRate;
    }
}
