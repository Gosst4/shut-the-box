using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerName;
    [SerializeField] GameObject playerDiscription;
    [SerializeField] TextMeshProUGUI playerDiscriptionText;
    [SerializeField] Button randomButton;
    [SerializeField] Button easyButton;
    [SerializeField] Button normalButton;
    [SerializeField] Button hardButton;

    public void SetPlayerName(string playerName)
    {
        _playerName.text = playerName;
    }

    public void SelectDifficulty(Difficulty difficulty)
    {
        
    }  
}
