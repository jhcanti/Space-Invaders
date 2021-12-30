using UnityEngine;

public class HealthController : MonoBehaviour
{
    private int _maxHealth;
    private int _currentHealth;
    private UISystem _uiSystem;
    private Teams _team;


    private void Start()
    {
        _uiSystem = ServiceLocator.Instance.GetService<UISystem>();
    }

    public void Init(int maxHealth, Teams team)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
        _team = team;
    }

    public void Heal(int amount)
    {
        var newHealth = _currentHealth + amount;
        _currentHealth = Mathf.Clamp(newHealth, _currentHealth, _maxHealth);
        if (_team == Teams.Ally)
            _uiSystem.SetHealth(_maxHealth, _currentHealth);
    }

    public bool ReciveDamage(int amount)
    {
        var newHealth = _currentHealth - amount;
        _currentHealth = Mathf.Clamp(newHealth, 0, _maxHealth);
        if (_team == Teams.Ally)
            _uiSystem.SetHealth(_maxHealth, _currentHealth);
        
        return _currentHealth == 0;
    }

    public void SetMaxHealth(int amount)
    {
        _maxHealth = amount;
    }
}
