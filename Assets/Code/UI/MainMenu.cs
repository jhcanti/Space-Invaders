using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button highScoresButton;
    [SerializeField] private Button quitButton;

    private EventQueue _eventQueue;
        
    private void Awake()
    {
        startGameButton.onClick.AddListener(StartGame);
        highScoresButton.onClick.AddListener(GoToHighScores);
        quitButton.onClick.AddListener(Quit);
    }

    private void Start()
    {
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
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