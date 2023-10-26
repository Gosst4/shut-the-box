using UnityEngine;

public class DiceUISelector : MonoBehaviour
{
    DiceManager _diceManager;
    private void Awake()
    {
        _diceManager = FindObjectOfType<DiceManager>();
    }
    public void SelectNumberOfDices(int number)
    {
        _diceManager.UpdateNumberOfDice(number);
    }    
}
