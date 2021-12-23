using UnityEngine;

public class GlobalInstaller : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private EventQueue eventQueue;
    [SerializeField] private ActionBindings actionBindings;


    private void Awake()
    {
        DontDestroyOnLoad(eventQueue.gameObject);
        DontDestroyOnLoad(sceneController.gameObject);

        var input = new UnityInputAdapter();
        input.Configure(actionBindings);
        ServiceLocator.Instance.RegisterService<IInput>(input);
        ServiceLocator.Instance.RegisterService(eventQueue);
        ServiceLocator.Instance.RegisterService(sceneController);
    }
}
