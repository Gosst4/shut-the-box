using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerNametext;
    [SerializeField] TextMeshProUGUI _scoreText;

	LocalizeStringEvent _playerNameEvent;

	private void Start()
	{
		_playerNameEvent = _playerNametext.GetComponent<LocalizeStringEvent>();
	}

	public void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void SetPlayerName(string name)
    {
		_playerNameEvent.StringReference.SetReference("Localization", name);
    }
}
