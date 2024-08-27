using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _championshipBtn;
    [SerializeField] PlayerSelectionScreen _playerSelectionScreen;
    [SerializeField] OptionsPopup _optionsPopup;

    private void Start()
    {
        gameObject.SetActive(true);
        _championshipBtn.interactable = false;
	}
    public void OnPlayClick()
    {
        Instantiate(_playerSelectionScreen,transform.parent);
        gameObject.SetActive(false);
    }

    public void OnOptionsClick()
    {
		Instantiate(_optionsPopup, transform);
	}

    public void OnRulesClick()
    {

    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
