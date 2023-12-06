using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] Button oneDice;
    [SerializeField] Button twoDice;
    [SerializeField] Button _rollButton;

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
}
