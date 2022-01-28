using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseView : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private UISystem _uiSystem;

    private void Awake()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(BackToMainmenu);
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
    }

    private void ResumeGame()
    {
        _uiSystem.OnResumePressed();
    }

    private void RestartGame()
    {
        _uiSystem.OnRestartPressed();
    }

    private void BackToMainmenu()
    {
        _uiSystem.OnMenuPressed();
    }
}
