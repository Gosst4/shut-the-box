using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] Button oneDice;
    [SerializeField] Button twoDice;
    [SerializeField] GameObject _rollButton;

    private void Start()
    {
        _rollButton.GetComponent<Animator>().keepAnimatorStateOnDisable = true;
    }
    public void SetNumberOfDices(int number)
    {
        DiceManager.Instance.UpdateNumberOfDice(number);
    }

    public void AllowDiceSelection(bool canChange)
    {
        oneDice.interactable = canChange;
        twoDice.interactable = canChange;
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
