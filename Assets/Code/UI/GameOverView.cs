using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TMP_InputField namePlayer;


    private UISystem _uiSystem;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(BackToMainMenu);
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

    public void Show(int score)
    {
        gameObject.SetActive(true);
        scoreText.SetText(score.ToString());
        namePlayer.Select();
    }


    private void RestartGame()
    {
        _uiSystem.OnRestartPressed();
    }

    private void BackToMainMenu()
    {
        _uiSystem.OnMenuPressed();
    }

    private void SaveData(string arg0)
    {
        namePlayer.text = String.Empty;
        // llamar al ScoreSystem para grabar el score en caso de estar entre los mejores scores
    }
}
