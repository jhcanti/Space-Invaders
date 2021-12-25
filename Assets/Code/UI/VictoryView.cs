using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryView : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private UISystem _uiSystem;

    private void Awake()
    {
        nextLevelButton.onClick.AddListener(NextLevel);
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

    private void NextLevel()
    {
        _uiSystem.OnNextLevelPressed();
    }
}
