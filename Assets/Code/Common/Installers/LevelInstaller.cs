using UnityEngine;

public class LevelInstaller : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private UISystem uiSystem;
    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private PlayerInstaller playerInstaller;
    [SerializeField] private PowerUpSpawner powerUpSpawner;


    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(enemySpawner);
        ServiceLocator.Instance.RegisterService(uiSystem);
        ServiceLocator.Instance.RegisterService(levelSystem);
        ServiceLocator.Instance.RegisterService(playerInstaller);
        ServiceLocator.Instance.RegisterService(powerUpSpawner);
    }

    private void Start()
    {
        ServiceLocator.Instance.GetService<IScoreSystem>().Init();
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<EnemySpawner>();
        ServiceLocator.Instance.UnregisterService<UISystem>();
        ServiceLocator.Instance.UnregisterService<LevelSystem>();
        ServiceLocator.Instance.UnregisterService<PlayerInstaller>();
        ServiceLocator.Instance.UnregisterService<PowerUpSpawner>();
    }
}
