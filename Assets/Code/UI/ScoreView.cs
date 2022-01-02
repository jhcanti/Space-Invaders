using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    public int CurrentScore => _currentScore;

    [SerializeField] private TextMeshProUGUI hiScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private BarView healthBar;
    [SerializeField] private BarView weaponBar;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private BarView[] shieldBars;
    private int _currentScore;



    private void UpdateScore(int newScore)
    {
        scoreText.SetText(_currentScore.ToString());
    }

    public void AddScore(int points)
    {
        _currentScore += points;
        UpdateScore(_currentScore);
    }

    public void SubtractScore(int points)
    {
        _currentScore -= points;
        UpdateScore(_currentScore);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetHiScore(int score)
    {
        hiScoreText.SetText(score.ToString());
    }

    public void SetHealth(int maxHealth, int currentHealth)
    {
        var percentage = currentHealth / (float) maxHealth;
        healthBar.SetBarAmount(percentage);
    }

    public void SetShield(int maxShield, int currentShield)
    {
        var shieldPiece = (float) maxShield / shieldBars.Length;
        for (var i = 0; i < shieldBars.Length; i++)
        {
            var maximum = shieldPiece * (i + 1);
            
            if (currentShield >= maximum)
            {
                shieldBars[i].SetBarAmount(100f);
            }
            else
            {
                var minimum = shieldPiece * i;
                var amount = (currentShield - minimum) / shieldPiece;
                shieldBars[i].SetBarAmount(amount);
            }
        }
    }
    
    public void SetWeaponIcon(Sprite sprite)
    {
        weaponIcon.sprite = sprite;
    }

    public void SetWeaponDurability(float currentDurability)
    {
        var percentage = currentDurability / 100f;
        weaponBar.SetBarAmount(percentage);
    }
    
}
