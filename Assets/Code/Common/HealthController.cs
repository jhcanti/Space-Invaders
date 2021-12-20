using UnityEngine;

public class HealthController : MonoBehaviour
{
    private int _maxHealth;
    private int _currentHealth;

    public void Init(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void Heal(int amount)
    {
        var newHealth = _currentHealth + amount;
        _currentHealth = Mathf.Clamp(newHealth, _currentHealth, _maxHealth);
    }

    public bool ReciveDamage(int amount)
    {
        var newHealth = _currentHealth - amount;
        _currentHealth = Mathf.Clamp(newHealth, 0, _maxHealth);
        return _currentHealth == 0;
    }

    public void SetMaxHealth(int amount)
    {
        _maxHealth = amount;
    }
}
