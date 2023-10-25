using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _winnerText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnStartAgainClick()
    {
        gameObject.SetActive(false);
    }

    public void UpdateWinnersText(string playerName)
    {
        _winnerText.text = $"The winner is {playerName}!";
    }
}
