using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerNametext;
    [SerializeField] TextMeshProUGUI _scoreText;

    public void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void SetPlayerName(string name)
    {
        _playerNametext.text = name;
    }
}
