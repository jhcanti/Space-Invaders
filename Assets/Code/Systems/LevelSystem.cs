using System.Collections;
using UnityEngine;

public class LevelSystem : MonoBehaviour, IEventObserver
{
    [SerializeField] private Parallax parallax;
    [SerializeField] private LevelConfiguration[] levelConfigurations;
    [SerializeField] private int countdownTime;

    private UISystem _uiSystem;
    private EnemySpawner _enemySpawner;
    private PlayerInstaller _playerInstaller;
    private bool _isPlayerSpawned;
    private int _currentLevel;


    private void Start()
    {
        _isPlayerSpawned = false;
        _currentLevel = 0;
        _uiSystem = ServiceLocator.Instance.GetService<UISystem>();
        _enemySpawner = ServiceLocator.Instance.GetService<EnemySpawner>();
        _playerInstaller = ServiceLocator.Instance.GetService<PlayerInstaller>();
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.NextLevel, this);
        StartCoroutine(Countdown());
        parallax.SetParallaxBackground(levelConfigurations[_currentLevel].ParallaxBackground);
    }

    public void ResetAndStart()
    {
        _uiSystem.HideAllMenus();
        _isPlayerSpawned = false;
        StartCoroutine(Countdown());
        ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new RestartLevelCompleteEvent());
    }

    public void NextLevel()
    {
        _currentLevel++;
        if (_currentLevel == levelConfigurations.Length)
        {
            Debug.Log("Falta incluir el codigo para ganar el juego");
            return;
        }
   
        _uiSystem.HideAllMenus();
        parallax.SetParallaxBackground(levelConfigurations[_currentLevel].ParallaxBackground);
        StartCoroutine(Countdown());
        _playerInstaller.ResetPlayerPosition();
        _playerInstaller.SetPlayerActive();
    }

    private IEnumerator Countdown()
    {
        _uiSystem.ShowCountdown(_currentLevel+1);
        for (int i = countdownTime; i > 0; i--)
        {
            _uiSystem.SetCountdownText(i.ToString());
            yield return new WaitForSeconds(1f);
        }
        _uiSystem.HideCountdownText();
        _enemySpawner.StartSpawn(levelConfigurations[_currentLevel]);
        parallax.StartParallax();
        if (!_isPlayerSpawned)
        {
            _playerInstaller.SpawnPlayer();
            _isPlayerSpawned = true;
        }
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.NextLevel)
        {
            new NextLevelCommand().Execute();
        }
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.NextLevel, this);
    }
}
