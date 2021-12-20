using System.Collections;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private int countdownTime;

    private UISystem _uiSystem;
    private EnemySpawner _enemySpawner;

    // al comenzar el nivel debemos mostrar la cuenta atrás de comienzo del nivel
    // instanciar al Player
    // leer el nivel en que estamos y cargar el parallax correspondiente
    // cuando termine la cuenta atrás empieza la batalla

    private void Start()
    {
        _uiSystem = ServiceLocator.Instance.GetService<UISystem>();
        _enemySpawner = ServiceLocator.Instance.GetService<EnemySpawner>();
        StartCoroutine(Countdown());
    }


    private IEnumerator Countdown()
    {
        _uiSystem.ShowCountdown();
        for (int i = countdownTime; i > 0; i--)
        {
            _uiSystem.SetCountdownText(i.ToString());
            yield return new WaitForSeconds(1f);
        }
        _uiSystem.HideCountdownText();
        _enemySpawner.StartSpawn();
    }

}
