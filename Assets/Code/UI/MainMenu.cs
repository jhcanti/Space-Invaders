using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button highScoresButton;
    [SerializeField] private Button quitButton;

    private AudioSystem _audioSystem;
    private EventQueue _eventQueue;
        
    private void Awake()
    {
        startGameButton.onClick.AddListener(StartGame);
        highScoresButton.onClick.AddListener(GoToHighScores);
        quitButton.onClick.AddListener(Quit);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startGameButton.gameObject);
        _audioSystem = ServiceLocator.Instance.GetService<AudioSystem>();
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        _audioSystem.PlayMusic("menu");
    }

    private void GoToHighScores()
    {
        _eventQueue.EnqueueEvent(new GoToHighScoresEvent());
    }

    private void StartGame()
    {
        _eventQueue.EnqueueEvent(new StartGameEvent());
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();       
#endif
    }

}