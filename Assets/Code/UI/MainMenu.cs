using System;
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
    private GameObject _selectedButton;
        
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
        _selectedButton = startGameButton.gameObject;
        _audioSystem = ServiceLocator.Instance.GetService<AudioSystem>();
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        _audioSystem.PlayMusic("menu");
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != _selectedButton)
        {
            _selectedButton = EventSystem.current.currentSelectedGameObject;
            _audioSystem.Play("select");
        }
    }

    private void GoToHighScores()
    {
        _audioSystem.Play("click");
        _eventQueue.EnqueueEvent(new GoToHighScoresEvent());
    }

    private void StartGame()
    {
        _audioSystem.Play("click");
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