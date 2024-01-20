using TMPro;
using UnityEngine;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void ShowResult(int firstDice, int secondDice)
    {
        int diceResult = firstDice + secondDice;
        text.text = diceResult.ToString();
    }
}
