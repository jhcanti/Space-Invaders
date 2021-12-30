using System;
using UnityEngine;

public class WeaponController : MonoBehaviour, IEventObserver
{
    [SerializeField] private ProjectileId defaultProjectile;
    
    private Transform _projectileSpawnPoint;
    private ProjectileId _activeProjectile;
    private float _fireRate;
    private float _timeBetweenShoots;
    private Teams _team;
    private bool _hasWeapon;
    private bool _canShoot;

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

        _canShoot = true;
        var shootPoint = transform.Find("ProjectileSpawnPoint");
        if (shootPoint != null)
            _projectileSpawnPoint = shootPoint;
    }

    public void Configure(Teams team)
    {
        _team = team;
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.CanShoot, this);
    }
    
    
    public void ChangeProjectile(ProjectileId id)
    {
        _activeProjectile = id;
        _fireRate = _activeProjectile.FireRate;
    }
    
    
    public void TryShoot()
    {
        if (_hasWeapon == false) return;

        if (_fireRate == 0)
        {
            if (_canShoot)
            {
                Shoot();    
            }            
        }
        else
        {
            if (Time.time > _timeBetweenShoots)
            {
                Shoot();
            }    
        }
    }

    private void Shoot()
    {
        var projectile = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile.gameObject.SetActive(true);
        projectile.Init(_projectileSpawnPoint, _team);
        _timeBetweenShoots = Time.time + _fireRate;
        _canShoot = false;
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.CanShoot)
        {
            _canShoot = true;
        }
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.CanShoot, this);
    }
}
