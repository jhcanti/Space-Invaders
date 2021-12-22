using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryView : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private UISystem _uiSystem;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(BackToMainMenu);
    }
    
    public void Configure(UISystem uiSystem)
    {
        _uiSystem = uiSystem;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(int score)
    {
        gameObject.SetActive(true);
        scoreText.SetText(score.ToString());
    }

    private void RestartGame()
    {
        _uiSystem.OnRestartPressed();
    }

    private void BackToMainMenu()
    {
        _uiSystem.OnMenuPressed();
    }
}
