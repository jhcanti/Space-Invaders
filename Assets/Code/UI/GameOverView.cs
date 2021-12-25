using System;
using TMPro;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
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

    public void Show(int score)
    {
        gameObject.SetActive(true);
        scoreText.SetText(score.ToString());
        namePlayer.Select();
    }

    private void SaveData(string arg0)
    {
        namePlayer.text = String.Empty;
        // llamar al ScoreSystem para grabar el score en caso de estar entre los mejores scores
    }
}
