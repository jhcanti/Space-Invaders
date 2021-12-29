using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    public int CurrentScore => _currentScore;

    [SerializeField] private TextMeshProUGUI hiScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;

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
}
