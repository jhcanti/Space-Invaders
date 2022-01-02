using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private int _maxShield;
    private int _currentShield;
    private UISystem _uiSystem;
    private Teams _team;
    
    
    private void Start()
    {
        _uiSystem = ServiceLocator.Instance.GetService<UISystem>();
        _uiSystem.SetShield(_maxShield, _currentShield);
    }

    public void Init(int maxShield, Teams team)
    {
        _maxShield = maxShield;
        _currentShield = _maxShield;
        _team = team;
    }

    public void AddShield(int amount)
    {
        var newShield = _currentShield + amount;
        _currentShield = Mathf.Clamp(newShield, _currentShield, _maxShield);
        if (_team == Teams.Ally)
            _uiSystem.SetShield(_maxShield, _currentShield);
    }

    public int ReceiveDamage(int amount)
    {
        var remainingDamage = 0;

        if (amount >= _currentShield)
        {
            Debug.Log("Demasiado daño: " + amount + ":" + _currentShield);
            remainingDamage = amount - _currentShield;
            _currentShield = 0;
        }
        else
        {
            _currentShield -= amount;
            remainingDamage = 0;
        }
        
        if (_team == Teams.Ally)
            _uiSystem.SetShield(_maxShield, _currentShield);
        
        return remainingDamage;
    }
    
    public void SetMaxShield(int amount)
    {
        _maxShield = amount;
    }
}
