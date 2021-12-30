using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ProjectileId defaultProjectile;
    
    private Transform _projectileSpawnPoint;
    private ProjectileId _activeProjectile;
    private float _fireRate;
    private float _timeBetweenShoots;
    private Teams _team;
    private bool _hasWeapon;

    private void Awake()
    {
        if (defaultProjectile == null)
        {
            _hasWeapon = false;
        }
        else
        {
            _hasWeapon = true;
            _activeProjectile = defaultProjectile;
            _fireRate = _activeProjectile.FireRate;
        }
        
        var shootPoint = transform.Find("ProjectileSpawnPoint");
        if (shootPoint != null)
            _projectileSpawnPoint = shootPoint;
    }

    public void Configure(Teams team)
    {
        _team = team;
    }
    
    
    public void ChangeProjectile(ProjectileId id)
    {
        _activeProjectile = id;
        _fireRate = _activeProjectile.FireRate;
    }
    
    
    public void TryShoot()
    {
        if (_hasWeapon == false) return;
        
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
        _timeBetweenShoots = Time.time + _fireRate;
    }
}
