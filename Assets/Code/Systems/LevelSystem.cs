using System.Collections;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private Parallax parallax;
    [SerializeField] private LevelConfiguration[] levelConfigurations;
    [SerializeField] private int countdownTime;

    private UISystem _uiSystem;
    private EnemySpawner _enemySpawner;
    private PlayerInstaller _playerInstaller;
    private int _currentLevel;


    private void Start()
    {
        _currentLevel = 0;
        _uiSystem = ServiceLocator.Instance.GetService<UISystem>();
        _enemySpawner = ServiceLocator.Instance.GetService<EnemySpawner>();
        _playerInstaller = ServiceLocator.Instance.GetService<PlayerInstaller>();
        StartCoroutine(Countdown());
        parallax.SetParallaxBackground(levelConfigurations[_currentLevel].ParallaxBackground);
    }

    public void ResetAndStart()
    {
        StartCoroutine(Countdown());
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
        _playerInstaller.SpawnPlayer();
    }

}
