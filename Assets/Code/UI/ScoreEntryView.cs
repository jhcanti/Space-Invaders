using TMPro;
using UnityEngine;

public class ScoreEntryView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI score;

    public void Configure(string pName, string pScore)
    {
        playerName.SetText(pName);
        score.SetText(pScore);
    }
}
