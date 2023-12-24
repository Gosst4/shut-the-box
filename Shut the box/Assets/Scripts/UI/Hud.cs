using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] Button oneDice;
    [SerializeField] Button twoDice;
    [SerializeField] Button _rollButton;

    private void Start()
    {
        oneDice.GetComponent<Animator>().keepAnimatorStateOnDisable = true;
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
        _rollButton.interactable = isAllowed;
    }

    public void ShowRollButton(bool isShown)
    {
        _rollButton.gameObject.SetActive(isShown);
    }
}
