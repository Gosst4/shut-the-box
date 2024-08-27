using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] GameObject _rollButton;
    [SerializeField] Button _optionsButton;
    [SerializeField] Button _rulesButton;
    [SerializeField] PlayerNameDisplay _playerNamePrefab;

    static Hud instance;
    public static Hud Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Hud>();
            }
            return instance;
        }
    }

    private void Start()
    {
        _rollButton.GetComponent<Animator>().keepAnimatorStateOnDisable = true;
		gameObject.SetActive(false);
	}

    public void CanRollDice(bool isAllowed)
    {
        _rollButton.GetComponent<Button>().interactable = isAllowed;
    }

    public void ShowRollButton(bool isShown)
    {
        _rollButton.SetActive(isShown);
    }

    public void ShowPlayerName(string name)
    {
        PlayerNameDisplay display = Instantiate(_playerNamePrefab, transform);
        display.SetPlayerName(name);
        Destroy(display.gameObject, 3f);
    }
}
