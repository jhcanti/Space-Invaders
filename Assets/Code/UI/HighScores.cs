using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    [SerializeField] private ScoreEntryView scoreEntryPrefab;
    [SerializeField] private RectTransform scoresContainer;
    [SerializeField] private Button _backButton;

    private EventQueue _eventQueue;
    private IScoreSystem _scoreSystem;
    private UserData _userData;

    private void Awake()
    {
        _backButton.onClick.AddListener(BackToMenu);
    }

    private void Start()
    {
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        _scoreSystem = ServiceLocator.Instance.GetService<IScoreSystem>();

        _userData = _scoreSystem.GetUserData();

        for (var i = 0; i < _userData.BestScores.Length; i++)
        {
            if (_userData.BestScores[i] > 0)
            {
                var scoreEntry = Instantiate(scoreEntryPrefab, scoresContainer);
                scoreEntry.GetComponent<ScoreEntryView>().Configure(_userData.PlayerNames[i],
                    _userData.BestScores[i].ToString());
            }                
        }
    }
        
    private void BackToMenu()
    {
        _eventQueue.EnqueueEvent(new BackToMenuEvent());
    }
}
