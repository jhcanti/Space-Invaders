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
    private IInput _input;
    private bool IsGamePaused;


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
        _eventQueue.Subscribe(EventIds.Victory, this);
    }

    private void Update()
    {
        if (!_input.IsPausePressed()) return;

        if (IsGamePaused)
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
        _eventQueue.Unsubscribe(EventIds.Victory, this);
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


    public void ResetScore(int points)
    {
        scoreView.SubtractScore(points);
    }


    public void AddScore(int points)
    {
        scoreView.AddScore(points);
    }

    public void OnPausePressed()
    {
        pauseView.Show();
        IsGamePaused = true;
        new PauseGameCommand().Execute();
    }

    public void OnResumePressed()
    {
        pauseView.Hide();
        IsGamePaused = false;
        new ResumeGameCommand().Execute();
    }

    public void OnRestartPressed()
    {
        gameOverView.Hide();
        _eventQueue.EnqueueEvent(new RestartEvent());
    }

    public void OnMenuPressed()
    {
        _eventQueue.EnqueueEvent(new BackToMenuEvent());
    }

    private void OnGameOver()
    {
        gameOverView.Show(scoreView.CurrentScore);
    }

    private void OnVictory()
    {
        victoryView.Show(scoreView.CurrentScore);
    }
    
    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.GameOver)
        {
            OnGameOver();
        }

        if (eventData.EventId == EventIds.Victory)
        {
            OnVictory();
        }
    }
}
