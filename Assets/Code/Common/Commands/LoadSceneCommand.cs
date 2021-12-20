public class LoadSceneCommand : ICommand
{
    private readonly string _sceneToLoad;

    public LoadSceneCommand(string sceneToLoad)
    {
        _sceneToLoad = sceneToLoad;
    }
        
    public void Execute()
    {
        ServiceLocator.Instance.GetService<SceneController>().LoadScene(_sceneToLoad);    
    }
}
