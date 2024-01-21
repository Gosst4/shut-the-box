using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceResultScreen : MonoBehaviour
{
    [SerializeField] ResultDisplay resultDisplayPrefab;

    [Header("DiceSelectionGroup")]
    [SerializeField] GameObject diceGroup;
    [SerializeField] Image firstDice;
    [SerializeField] Image secondDice;
    [SerializeField] Sprite[] dices;
    [SerializeField] Button leftArrowBtn;
    [SerializeField] Button rightArrowBtn;

    private void Start()
    {
        ClearDiceResult();
    }
    public void ShowResultScreen(List<int> allDiceResult)
    {
        diceGroup.SetActive(false);
        ResultDisplay display = Instantiate(resultDisplayPrefab, transform);
        if (allDiceResult.Count == 1)
        {
            display.ShowResult(allDiceResult[0]);
        }
        else if (allDiceResult.Count == 2)
        {
            display.ShowResult(allDiceResult[0], allDiceResult[1]);
        }
        StartCoroutine(HideCor(display, allDiceResult));        
    }

    public void ClearDiceResult()
    {
        diceGroup.SetActive(false);
    }

    public void AllowDiceSelection(bool canChange)
    {
        leftArrowBtn.interactable = canChange;
        leftArrowBtn.interactable = canChange;
    }

    public void SetNumberOfDices(int number)
    {
        DiceManager.Instance.UpdateNumberOfDice(number);
    }

    private IEnumerator HideCor(ResultDisplay display, List<int> allDiceResult)
    {
        yield return new WaitForSeconds(2);
        UpdateDiceSelectionGroup(allDiceResult);
        Destroy(display.gameObject);
    }

    private void UpdateDiceSelectionGroup(List<int> allDiceResult)
    {
        diceGroup.SetActive(true);

        if (allDiceResult.Count == 1)
        {
            firstDice.sprite = dices[allDiceResult[0] - 1];
            secondDice.gameObject.SetActive(false);
        }
        else if (allDiceResult.Count == 2)
        {
            firstDice.sprite = dices[allDiceResult[0] - 1];
            secondDice.gameObject.SetActive(true);
            secondDice.sprite = dices[allDiceResult[1] - 1];
        }
    }
}
