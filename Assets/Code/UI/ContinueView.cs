using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContinueView : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private UISystem _uiSystem;

    private void Awake()
    {
        yesButton.onClick.AddListener(RestartLevel);
        noButton.onClick.AddListener(NoContinue);
    }

    public void Configure(UISystem uiSystem)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(yesButton.gameObject);
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
    
    private void NoContinue()
    {
        _uiSystem.OnGameOver();
    }

    private void RestartLevel()
    {
        _uiSystem.OnRestartPressed();
    }
}
