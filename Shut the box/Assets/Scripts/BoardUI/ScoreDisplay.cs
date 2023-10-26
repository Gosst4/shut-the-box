using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerNametext;
    [SerializeField] TextMeshProUGUI _scoreText;

    public void UpdateText(int score)
    {
        _scoreText.text = score.ToString();
    }
}
