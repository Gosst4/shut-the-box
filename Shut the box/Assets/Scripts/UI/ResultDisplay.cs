using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image firstDice;
    [SerializeField] Image secondDice;
    [SerializeField] Sprite[] dices;

    public void ShowResult(int firstDiceValue, int secondDiceValue)
    {
        int diceResult = firstDiceValue + secondDiceValue;
        text.text = diceResult.ToString();
        SetSprite(firstDiceValue, firstDice);
        SetSprite(secondDiceValue, secondDice);
    }

    public void ShowResult(int diceResult)
    {
        text.text = diceResult.ToString();
        secondDice.gameObject.SetActive(false);
        SetSprite(diceResult, firstDice);
    }

    private void SetSprite(int diceResult, Image diceImage)
    {
        diceImage.sprite = dices[diceResult - 1];
    }
}
