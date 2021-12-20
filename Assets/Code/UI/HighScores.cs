using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    [SerializeField] private ScoreEntryView scoreEntryPrefab;
    [SerializeField] private RectTransform scoresContainer;
    [SerializeField] private Button _backButton;

    private EventQueue _eventQueue;
    private UserData data;

    private void Awake()
    {
        _backButton.onClick.AddListener(BackToMenu);
    }

    private void Start()
    {
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();

        //data = DataStore.Instance.GetData<UserData>("data");

        if (data == null) return;
            
        for (var i = 0; i < data.BestScores.Length; i++)
        {
                
        }
    }
        
    private void BackToMenu()
    {
        _eventQueue.EnqueueEvent(new BackToMenuEvent());
    }
}
