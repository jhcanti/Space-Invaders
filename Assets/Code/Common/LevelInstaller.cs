using UnityEngine;

public class LevelInstaller : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private UISystem uiSystem;


    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(enemySpawner);
        ServiceLocator.Instance.RegisterService(uiSystem);
    }

    private void Start()
    {
        ServiceLocator.Instance.GetService<IScoreSystem>().Init();
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<EnemySpawner>();
        ServiceLocator.Instance.UnregisterService<UISystem>();
    }
}
