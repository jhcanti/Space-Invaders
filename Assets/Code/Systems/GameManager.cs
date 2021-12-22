using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameStates CurrentGameState { get; set; }

    private StateMachine _stateMachine;
    private SceneController _sceneController;
    private DataStore _dataStore;
    private IScoreSystem _scoreSystem;
    private UserData _userData;
    private InMenuState _inMenu;
    private InHighScoresState _inHighScores;
    private PlayingState _playing;
    private GameOverState _gameOver;
    private VictoryState _victory;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _stateMachine = new StateMachine();
        _dataStore = new DataStore();

        // instanciamos los diferentes estados
        _inMenu = new InMenuState(this);
        _inHighScores = new InHighScoresState(this);
        _playing = new PlayingState(this);
        _gameOver = new GameOverState(this);
        _victory = new VictoryState(this);

        // definimos las transiciones
        AddTransition(_inMenu, _playing, OnStatePlaying());
        AddTransition(_inMenu, _inHighScores, OnStateHighScores());
        AddTransition(_inHighScores, _inMenu, OnStateInMenu());
        AddTransition(_playing, _gameOver, OnStateGameOver());
        AddTransition(_playing, _victory, OnStateVictory());
        AddTransition(_playing, _inMenu, OnStateInMenu());
        AddTransition(_gameOver, _inMenu, OnStateInMenu());
        AddTransition(_victory, _inMenu, OnStateInMenu());
            
        void AddTransition(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            
        // definimos las condiciones
        Func<bool> OnStatePlaying() => () => CurrentGameState == GameStates.Playing;
        Func<bool> OnStateHighScores() => () => CurrentGameState == GameStates.InHighScores;
        Func<bool> OnStateGameOver() => () => CurrentGameState == GameStates.GameOver;
        Func<bool> OnStateVictory() => () => CurrentGameState == GameStates.Victory;
        Func<bool> OnStateInMenu() => () => CurrentGameState == GameStates.InMenu;
    }

    private void Start()
    {
        _scoreSystem = new ScoreSystem(_dataStore);
        ServiceLocator.Instance.RegisterService<IScoreSystem>(_scoreSystem);
        _sceneController = ServiceLocator.Instance.GetService<SceneController>();
            
        CurrentGameState = GameStates.InMenu;
        _stateMachine.SetState(_inMenu);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }


    public void GameOver()
    {
        
    }

        
    public void SaveData(string name, int score)
    {
        if (name == String.Empty)
            name = "User name";
            

        //_sceneController.LoadScene("HighScores");
    }
}
