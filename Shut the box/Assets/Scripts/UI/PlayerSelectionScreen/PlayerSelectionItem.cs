using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionItem : MonoBehaviour
{    
    [SerializeField] TextMeshProUGUI _playerName;
    [SerializeField] GameObject playerItemContainer;
    [SerializeField] int index;

    [Header("Human Player")]
    [SerializeField] GameObject playerDiscription;
    [SerializeField] bool isDefault;

    [Header("Computer Player")]
    [SerializeField] ToggleGroup toggleGroup;
	[SerializeField] bool isDefaultAi;

	[Header("Buttons")]
    [SerializeField] GameObject buttonContainer;
    [SerializeField] Button removeButton;

    string _name;
    PlayerType _playerType;
    bool isSelected = false;
	PlayerSelectionScreen _playerSelectionScreen;

	const int DefaultDiffIndex = 2;

    private void Start()
    {
        removeButton.gameObject.SetActive(!isDefault);
        playerItemContainer.SetActive(false);
        _playerSelectionScreen = FindObjectOfType<PlayerSelectionScreen>();

		if (isDefault) AddHumanPlayer();
        if (isDefaultAi) AddComputerPlayer();
    }

    public void AddHumanPlayer()
    {
        playerItemContainer.SetActive(true);
        playerDiscription.SetActive(true);
        toggleGroup.gameObject.SetActive(false);
        _name = "Player " + index;
        _playerName.text = _name;
        buttonContainer.SetActive(false);
        _playerType = PlayerType.Human;
        isSelected = true;

		_playerSelectionScreen.CheckStartButton();
	}

    public void AddComputerPlayer()
    {
        playerItemContainer.SetActive(true);
        playerDiscription.SetActive(false);
        toggleGroup.gameObject.SetActive(true);
        _name = "A.I. " + index;
        _playerName.text = _name;
        var selectedToggle = toggleGroup.transform.GetChild(DefaultDiffIndex).GetComponent<Toggle>();
        selectedToggle.isOn = true;
        selectedToggle.Select();

        buttonContainer.SetActive(false);
        isSelected = true;

		_playerSelectionScreen.CheckStartButton();
    }

    public void OnRandomClick(bool difficulty)
    {
        if (difficulty)
            _playerType = (PlayerType)UnityEngine.Random.Range(1, 4);                  
    }

    public void OnEasyClick(bool difficulty)
    {
        if (difficulty)
            _playerType = PlayerType.ComputerEasy;
    }
    public void OnNormalClick(bool difficulty)
    {
        if (difficulty)
            _playerType = PlayerType.ComputerNormal;
    }
    public void OnHardClick(bool difficulty)
    {
        if (difficulty)
            _playerType = PlayerType.ComputerHard;
    }
    public void OnRemoveClick()
    {
        buttonContainer.SetActive(true);
        playerItemContainer.SetActive(false);
        isSelected = false;
		_playerSelectionScreen.CheckStartButton();
	}

    public PlayerData GetPlayerData()
    {
        if (isSelected) return new PlayerData(_name, _playerType);
        else return null;
    }
}

