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

    List<int> _allDiceResult = new List<int>();
    bool _animationOn = false;

    private void Start()
    {
        ClearDiceResult();
    }
    public void ShowResultScreen(List<int> allDiceResult)
    {
        _animationOn = true;
        _allDiceResult = allDiceResult;
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
        GetComponent<Animator>().SetTrigger("FadeOutDiceResult");
        //StartCoroutine(HideCor(allDiceResult));        
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

    public void OnScreenFaded()
    {
        UpdateDiceSelectionGroup();
        resultDisplay.SetActive(false);
    }

    private void UpdateDiceSelectionGroup()
    {
        if (Dice._isRolling) return;

        diceGroup.SetActive(true);

        if (_allDiceResult.Count == 1)
        {
            firstDice.sprite = diceSprites[_allDiceResult[0] - 1];
        }
        else if (_allDiceResult.Count == 2)
        {
            firstDice.sprite = diceSprites[_allDiceResult[0] - 1];
            secondDice.sprite = diceSprites[_allDiceResult[1] - 1];
        }
    }
}
