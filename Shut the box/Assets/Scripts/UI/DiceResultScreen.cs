using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceResultScreen : MonoBehaviour
{
    [Header("Popup")]
    [SerializeField] GameObject resultDisplay;    
    [SerializeField] TextMeshProUGUI textFullScreen;
    [SerializeField] Image firstDiceBig;
    [SerializeField] Image secondDiceBig;
    [SerializeField] Sprite[] diceSprites;

    [Header("DiceSelectionGroup")]
    [SerializeField] GameObject diceGroup;
    [SerializeField] Image firstDice;
    [SerializeField] Image secondDice;
    [SerializeField] TextMeshProUGUI diceNumberText;
    [SerializeField] Button leftArrowBtn;
    [SerializeField] Button rightArrowBtn;

    private void Start()
    {
        ClearDiceResult();
    }
    public void ShowResultScreen(List<int> allDiceResult)
    {
        diceGroup.SetActive(false);
        resultDisplay.SetActive(true);
        if (allDiceResult.Count == 1)
        {
            textFullScreen.text = allDiceResult[0].ToString();            
            firstDiceBig.sprite = diceSprites[allDiceResult[0] - 1];
            secondDiceBig.gameObject.SetActive(false);
        }
        else if (allDiceResult.Count == 2)
        {  
            int diceResult = allDiceResult[0] + allDiceResult[1];
            textFullScreen.text = diceResult.ToString();

            firstDiceBig.sprite = diceSprites[allDiceResult[0] - 1];
            secondDiceBig.sprite = diceSprites[allDiceResult[1] - 1];
        }
        resultDisplay.GetComponent<Animator>().SetTrigger("FadeOutDiceResult");
        StartCoroutine(HideCor(allDiceResult));        
    }

    public void ClearDiceResult()
    {
        diceGroup.SetActive(false);
        resultDisplay.SetActive(false);
    }

    public void AllowDiceSelection(bool canChange)
    {
        leftArrowBtn.interactable = canChange;
        rightArrowBtn.interactable = canChange;
    }

    public void UpdateDiceInfo(int diceNumber)
    {
        if (diceNumber == 2)
        {
            secondDice.gameObject.SetActive(false);
            diceNumberText.text = "One Dice";
        }
        else
        {
            secondDice.gameObject.SetActive(true);
            diceNumberText.text = "Two Dice";
        }
    }   

    private IEnumerator HideCor(List<int> allDiceResult)
    {
        yield return new WaitForSeconds(2);
        UpdateDiceSelectionGroup(allDiceResult);
        resultDisplay.SetActive(false);
    }

    private void UpdateDiceSelectionGroup(List<int> allDiceResult)
    {
        diceGroup.SetActive(true);

        if (allDiceResult.Count == 1)
        {
            firstDice.sprite = diceSprites[allDiceResult[0] - 1];
        }
        else if (allDiceResult.Count == 2)
        {
            firstDice.sprite = diceSprites[allDiceResult[0] - 1];
            secondDice.sprite = diceSprites[allDiceResult[1] - 1];
        }
    }
}
