using UnityEngine;
using UnityEngine.UI;

public class DiceUISelector : MonoBehaviour
{
    [SerializeField] Button oneDice;
    [SerializeField] Button twoDice;

    public void SelectNumberOfDices(int number)
    {
        DiceManager.Instance.UpdateNumberOfDice(number);
    }

    public void ShowDiceSelection(bool isSelected)
    {
        oneDice.interactable = isSelected;
        twoDice.interactable = isSelected;       
    }
}
