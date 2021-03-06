using System.Collections;
using UnityEngine;

public class LevelSystem : MonoBehaviour, IEventObserver
{
    [SerializeField] private Parallax parallax;
    [SerializeField] private LevelConfiguration[] levelConfigurations;
    [SerializeField] private int countdownTime;

    private UISystem _uiSystem;
    private AudioSystem _audioSystem;
    private EnemySpawner _enemySpawner;
    private PlayerInstaller _playerInstaller;
    private int _currentLevel;


    private void Start()
    {
        _currentLevel = 0;
        _uiSystem = ServiceLocator.Instance.GetService<UISystem>();
        _audioSystem = ServiceLocator.Instance.GetService<AudioSystem>();
        _enemySpawner = ServiceLocator.Instance.GetService<EnemySpawner>();
        _playerInstaller = ServiceLocator.Instance.GetService<PlayerInstaller>();
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.NextLevel, this);
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.Victory, this);
        _uiSystem.SetHiScore();
        StartCoroutine(Countdown());
    }

    public void ResetAndStart()
    {
        _uiSystem.HideAllMenus();
        _uiSystem.SetHiScore();
        StartCoroutine(Countdown());
        ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new RestartLevelCompleteEvent());
    }

    public void NextLevel()
    {
        _uiSystem.HideAllMenus();
        _uiSystem.SetHiScore();
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        parallax.StopParallax();
        _uiSystem.ShowCountdown(_currentLevel+1);
        for (int i = countdownTime; i > 0; i--)
        {
            _uiSystem.SetCountdownText(i.ToString());
            yield return new WaitForSeconds(1f);
        }
        _audioSystem.PlayMusic("gameplay");
        _uiSystem.HideCountdownText();
        _enemySpawner.StartSpawn(levelConfigurations[_currentLevel]);
        parallax.StartParallax();
        _playerInstaller.SpawnPlayer();
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.NextLevel)
        {
            new NextLevelCommand().Execute();
        }

        if (eventData.EventId == EventIds.Victory)
        {
            _currentLevel++;
            if (_currentLevel == levelConfigurations.Length)
            {
                Debug.Log("Falta incluir el codigo para ganar el juego");
            }
            else
            {
                _uiSystem.OnVictory();
            }
        }
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.NextLevel, this);
        ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.Victory, this);
    }
}
