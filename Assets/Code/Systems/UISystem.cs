using DG.Tweening;
using TMPro;
using UnityEngine;

public class UISystem : MonoBehaviour, IEventObserver
{
    [SerializeField] private PauseView pauseView;
    [SerializeField] private GameOverView gameOverView;
    [SerializeField] private VictoryView victoryView;
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private ContinueView continueView;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private CanvasGroup countdownCanvasGroup;

    private EventQueue _eventQueue;
    private IScoreSystem _scoreSystem;
    private IInput _input;
    private bool _isGamePaused;


    private void Awake()
    {
        pauseView.Configure(this);
        gameOverView.Configure(this);
        victoryView.Configure(this);
        continueView.Configure(this);
    }


    private void Start()
    {
        HideAllMenus();
        _input = ServiceLocator.Instance.GetService<IInput>();
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        _eventQueue.Subscribe(EventIds.GameOver, this);
        _scoreSystem = ServiceLocator.Instance.GetService<IScoreSystem>();
    }

    private void Update()
    {
        if (!_input.IsPausePressed()) return;

        if (_isGamePaused)
        {
            OnResumePressed();
        }
        else
        {
            OnPausePressed();
        }
    }

    private void OnDestroy()
    {
        _eventQueue.Unsubscribe(EventIds.GameOver, this);
    }

    public void HideAllMenus()
    {
        pauseView.Hide();
        scoreView.Hide();
        gameOverView.Hide();
        victoryView.Hide();
        continueView.Hide();
    }

    public void ShowCountdown(int level)
    {
        levelText.text = "Level " + level;
        countdownText.gameObject.SetActive(true);
        countdownCanvasGroup.alpha = 1;
    }


    public void SetCountdownText(string text)
    {
        if (text.Equals(countdownText.text)) return;

        countdownText.text = text;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(countdownText.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1), .1f).SetEase(Ease.OutSine));
        sequence.Append(countdownText.rectTransform.DOScale(new Vector3(1, 1, 1), .2f).SetEase(Ease.OutSine));
    }


    public void HideCountdownText()
    {
        countdownText.text = "GO!";
        Sequence sequence = DOTween.Sequence();
        sequence.Append(countdownText.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1), .1f).SetEase(Ease.OutSine));
        sequence.Append(countdownText.rectTransform.DOScale(new Vector3(1, 1, 1), .3f).SetEase(Ease.OutSine));
        sequence.Append(countdownCanvasGroup.DOFade(0, .2f));
        sequence.OnComplete(() =>
        {
            countdownText.gameObject.SetActive(false);
            scoreView.Show();
        });
    }


    public void SubtractLevelScore(int points)
    {
        scoreView.SubtractScore(points);
    }


    public void AddScore(int points)
    {
        scoreView.AddScore(points);
    }

    public void SetHiScore()
    {
        var hiScore = _scoreSystem.GetHighScore();
        scoreView.SetHiScore(hiScore);    
    }

    public void SetHealth(int maxHealth, int currentHealth)
    {
        scoreView.SetHealth(maxHealth, currentHealth);
    }

    private void OnPausePressed()
    {
        pauseView.Show();
        _isGamePaused = true;
        new PauseGameCommand().Execute();
    }

    public void OnResumePressed()
    {
        pauseView.Hide();
        _isGamePaused = false;
        new ResumeGameCommand().Execute();
    }

    public void OnRestartPressed()
    {
        continueView.Hide();
        _eventQueue.EnqueueEvent(new RestartEvent());
    }

    public void OnMenuPressed()
    {
        _eventQueue.EnqueueEvent(new BackToMenuEvent());
    }

    public void OnNextLevelPressed()
    {
        _eventQueue.EnqueueEvent(new NextLevelEvent());
    }
    
    private void OnPlayerDead()
    {
        continueView.Show();
    }

    public void OnGameOver()
    {
        continueView.Hide();
        var showInput = scoreView.CurrentScore > _scoreSystem.GetMinimumScoreTopTen();
        gameOverView.Show(_scoreSystem.GetHighScore(), scoreView.CurrentScore, showInput);
        _eventQueue.EnqueueEvent(new NoContinueEvent(showInput));
    }

    public void OnNameEnter(string name)
    {
        gameOverView.Hide();
        _scoreSystem.UpdateBestScores(name, scoreView.CurrentScore);
        _eventQueue.EnqueueEvent(new ScoreUpdatedEvent());
    }
    
    public void OnVictory()
    {
        victoryView.Show(scoreView.CurrentScore);
    }
    
    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.GameOver)
        {
            OnPlayerDead();
        }
    }
}
