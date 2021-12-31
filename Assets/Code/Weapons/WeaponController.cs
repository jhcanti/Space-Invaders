using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ProjectileId defaultProjectile;
    
    private Transform _projectileSpawnPoint;
    private ProjectileId _activeProjectile;
    private UISystem _uiSystem;
    private float _fireRate;
    private float _durabilityInSeconds;
    private float _timeBetweenShoots;
    private float _timeRemaining;
    private Teams _team;
    private bool _hasWeapon;
    private float _durability;
    private bool _hasShoot;

    private void Awake()
    {
        _durability = 100;
        _hasShoot = false;
        var shootPoint = transform.Find("ProjectileSpawnPoint");
        if (shootPoint != null)
            _projectileSpawnPoint = shootPoint;
    }

    public void Configure(Teams team)
    {
        _team = team;
        _uiSystem = ServiceLocator.Instance.GetService<UISystem>();
        if (defaultProjectile == null)
        {
            _hasWeapon = false;
        }
        else
        {
            _hasWeapon = true;
            ChangeProjectile(defaultProjectile);
        }
    }


    private void Update()
    {
        if (_team == Teams.Enemy) return;
        
        if (_durabilityInSeconds > 0)
        {
            CheckDurability();
        }
    }

    
    public void ChangeProjectile(ProjectileId id)
    {
        _activeProjectile = id;
        _fireRate = _activeProjectile.FireRate;
        _durabilityInSeconds = _activeProjectile.DurabilityInSeconds;
        _timeRemaining = _durabilityInSeconds;
        _durability = 100;
        
        if (_team == Teams.Enemy) return;
        
        _uiSystem.SetWeaponIcon(id.Icon);
        _uiSystem.SetWeaponDurability(_durability);
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
            
            if (_team == Teams.Enemy) return;
        
            if (_durabilityInSeconds > 0)
            {
                _timeRemaining -= Time.deltaTime;
                _durability = (_timeRemaining / _durabilityInSeconds) * 100f;
                _uiSystem.SetWeaponDurability(_durability);
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
            ChangeProjectile(defaultProjectile);
        }

        if (_fireRate == 0 && _hasShoot)
        {
            _timeRemaining -= Time.deltaTime;
            _durability = (_timeRemaining / _durabilityInSeconds) * 100f;
            _uiSystem.SetWeaponDurability(_durability);
        }
    }
}
