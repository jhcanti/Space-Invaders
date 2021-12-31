using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ProjectileId defaultProjectile;
    
    private Transform _projectileSpawnPoint;
    private ProjectileId _activeProjectile;
    private float _fireRate;
    private float _costPerSecond;
    private float _timeBetweenShoots;
    private float _oneSecond;
    private Teams _team;
    private bool _hasWeapon;
    private float _durability;
    private bool _hasShoot;

    private void Awake()
    {
        _durability = 100;
        _oneSecond = 1f;
        if (defaultProjectile == null)
        {
            _hasWeapon = false;
        }
        else
        {
            _hasWeapon = true;
            _activeProjectile = defaultProjectile;
            _fireRate = _activeProjectile.FireRate;
            _costPerSecond = _activeProjectile.CostPerSecond;
        }

        _hasShoot = false;
        var shootPoint = transform.Find("ProjectileSpawnPoint");
        if (shootPoint != null)
            _projectileSpawnPoint = shootPoint;
    }

    public void Configure(Teams team)
    {
        _team = team;
    }


    private void Update()
    {
        if (_costPerSecond > 0)
        {
            CheckDurability();
        }
    }

    
    public void ChangeProjectile(ProjectileId id)
    {
        _activeProjectile = id;
        _fireRate = _activeProjectile.FireRate;
        _costPerSecond = _activeProjectile.CostPerSecond;
        if (_costPerSecond > 0)
        {
            _durability = 100;
            _oneSecond = 1f;
        }
    }

    
    public void TryShoot()
    {
        if (_hasWeapon == false) return;

        if (_fireRate == 0)
        {
            if (_hasShoot == false)
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
            
            if (_costPerSecond > 0)
            {
                _oneSecond -= Time.deltaTime;
                Debug.Log("Time: " + _oneSecond);
                if (_oneSecond <= 0)
                {
                    _durability -= _costPerSecond;
                    Debug.Log("Durability: " + _durability);
                    _oneSecond = 1f;
                }    
            }
        }
    }

    
    private void Shoot()
    {
        var projectile = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile.gameObject.SetActive(true);
        projectile.Init(_projectileSpawnPoint, _team);
        
        if (_fireRate == 0)
        {
            _hasShoot = true;
        }
        else
        {
            _timeBetweenShoots = Time.time + _fireRate;
        }
        
    }

    
    private void CheckDurability()
    {
        if (_durability <= 0)
        {
            Debug.Log("Cambiando a proyectil por defecto");
            ChangeProjectile(defaultProjectile);
        }

        if (_fireRate == 0 && _hasShoot)
        {
            _oneSecond -= Time.deltaTime;
            if (_oneSecond <= 0)
            {
                _durability -= _costPerSecond;
                _oneSecond = 1f;
            }    
        }
    }
}
