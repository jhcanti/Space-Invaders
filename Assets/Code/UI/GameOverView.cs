using System;
using TMPro;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject nameInput;
    [SerializeField] private TMP_InputField namePlayer;

    private UISystem _uiSystem;

    private void Awake()
    {
        namePlayer.onEndEdit.AddListener(SaveData);
    }


    public void Configure(UISystem uiSystem)
    {
        _uiSystem = uiSystem;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(int highScore, int score, bool showInput)
    {
        gameObject.SetActive(true);
        nameInput.SetActive(false);
        highScoreText.SetText(highScore.ToString());
        scoreText.SetText(score.ToString());
        if (showInput == false) return;
        
        nameInput.SetActive(true);
        namePlayer.Select();
    }

    private void SaveData(string name)
    {
        namePlayer.text = String.Empty;
        _uiSystem.OnNameEnter(name);
    }
}
