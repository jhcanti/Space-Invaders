using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ProjectileId defaultProjectile;
    [SerializeField] private ProjectileId tripleProjectile;
    [SerializeField] private ProjectileId fragmentationProjectile;
    
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
        if (_activeProjectile == tripleProjectile)
        {
            TripleShoot();
            return;
        }

        if (_activeProjectile == fragmentationProjectile)
        {
            FragmentationShoot();
            return;
        }
        
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

    private void TripleShoot()
    {
        var projectile1 = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile1.gameObject.SetActive(true);
        projectile1.Init(_projectileSpawnPoint, _team);
        projectile1.GetComponent<TripleProjectile>().Configure(new Vector2(1f,.15f));
        
        var projectile2 = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile2.gameObject.SetActive(true);
        projectile2.Init(_projectileSpawnPoint, _team);
        projectile2.GetComponent<TripleProjectile>().Configure(new Vector2(1f,0f));
        
        var projectile3 = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile3.gameObject.SetActive(true);
        projectile3.Init(_projectileSpawnPoint, _team);
        projectile3.GetComponent<TripleProjectile>().Configure(new Vector2(1f,-0.15f));
        
        _timeBetweenShoots = Time.time + _fireRate;
    }

    private void FragmentationShoot()
    {
        var projectile1 = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile1.gameObject.SetActive(true);
        projectile1.Init(_projectileSpawnPoint, _team);
        projectile1.GetComponent<FragmentationProjectile>().Configure(new Vector2(1f,.2f), .3f);
        
        var projectile2 = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile2.gameObject.SetActive(true);
        projectile2.Init(_projectileSpawnPoint, _team);
        projectile2.GetComponent<FragmentationProjectile>().Configure(new Vector2(1f,.1f), .3f);
        
        var projectile3 = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile3.gameObject.SetActive(true);
        projectile3.Init(_projectileSpawnPoint, _team);
        projectile3.GetComponent<FragmentationProjectile>().Configure(new Vector2(1f,0f), .3f);
        
        var projectile4 = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile4.gameObject.SetActive(true);
        projectile4.Init(_projectileSpawnPoint, _team);
        projectile4.GetComponent<FragmentationProjectile>().Configure(new Vector2(1f,-0.1f), .3f);
        
        var projectile5 = ProjectilePool.Instance.Get(_activeProjectile.Value);
        projectile5.gameObject.SetActive(true);
        projectile5.Init(_projectileSpawnPoint, _team);
        projectile5.GetComponent<FragmentationProjectile>().Configure(new Vector2(1f,-0.2f), .3f);
        
        _timeBetweenShoots = Time.time + _fireRate;
    }
    
    private void CheckDurability()
    {
        if (_durability <= 0)
        {
            _hasShoot = false;
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
