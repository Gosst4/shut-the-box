using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] GameObject _rollButton;

    private void Start()
    {
        _rollButton.GetComponent<Animator>().keepAnimatorStateOnDisable = true;
    }

    public void CanRollDice(bool isAllowed)
    {
        _rollButton.GetComponent<Button>().interactable = isAllowed;
    }

    public void ShowRollButton(bool isShown)
    {
        _rollButton.SetActive(isShown);
    }
}
