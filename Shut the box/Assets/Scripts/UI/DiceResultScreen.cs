using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceResultScreen : MonoBehaviour
{
    [SerializeField] ResultDisplay resultDisplayPrefab;

    [Header("DiceSelectionGroup")]
    [SerializeField] GameObject diceGroup;
    [SerializeField] Image firstDie;
    [SerializeField] Image secondDie;
    [SerializeField] Sprite[] dice;
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
        rightArrowBtn.interactable = canChange;
    }

    public void SelectOneDie()
    {
        DiceManager.Instance.UpdateNumberOfDice(1);
        secondDie.gameObject.SetActive(false);
        diceNumberText.text = "One Die";
    }
    public void SelectTwoDices()
    {
        DiceManager.Instance.UpdateNumberOfDice(2);
        secondDie.gameObject.SetActive(true);
        diceNumberText.text = "Two Dice";
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
            firstDie.sprite = dice[allDiceResult[0] - 1];
            //secondDice.gameObject.SetActive(false);               // !
        }
        else if (allDiceResult.Count == 2)
        {
            firstDie.sprite = dice[allDiceResult[0] - 1];
            //secondDice.gameObject.SetActive(true);               // !
            secondDie.sprite = dice[allDiceResult[1] - 1];
        }
    }
}
