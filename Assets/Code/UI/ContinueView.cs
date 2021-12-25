using UnityEngine;
using UnityEngine.UI;

public class ContinueView : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private UISystem _uiSystem;

    private void Awake()
    {
        yesButton.onClick.AddListener(RestartLevel);
        noButton.onClick.AddListener(GameOver);
    }

    public void Configure(UISystem uiSystem)
    {
        _uiSystem = uiSystem;
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void GameOver()
    {
        
    }

    private void RestartLevel()
    {
        _uiSystem.OnRestartPressed();
    }
}
