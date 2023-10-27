using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{   
    [SerializeField] Dice dicePrefab;
    [SerializeField] DiceNumberDisplay numberDisplay;
    [SerializeField] GameObject _tintOverDiceSelection;
    [SerializeField] Button _rollButton;

    public int AllDiceResult {  get; private set; }

    int startingNumberOfDice = 2;
    List<Dice> dicePool = new List<Dice>();
    List<Dice> currentDices = new List<Dice>();
   
    public event Action<int> OnAllRollsFinished;

    static DiceManager instance;
    public static DiceManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DiceManager>();
            }
            return instance;
        }
    }

    private void Start()
    {
        PregenerateDicePool(startingNumberOfDice);
        InstantiateDices(startingNumberOfDice);
        CanRollDice(true);
    }

    private void PregenerateDicePool(int v)
    {
        dicePool.Clear();
        for (int i = 0; i < v; i++)
        {
            Dice dice = Instantiate(dicePrefab, new Vector3(i + 1, i, i + 1), Quaternion.identity);
            dice.transform.parent = transform;
            dicePool.Add(dice);
            dice.OnRollFinished += Dice_OnRollFinished;
            dice.gameObject.SetActive(false);
        }
    }

    public void RollTheDice()
    {
        if (!Dice._isRolling)
        {
            CanRollDice(false);
            StartCoroutine(RollAllDice());
        }
    }

    public void CanRollDice(bool isAllowed)
    {
        _rollButton.interactable = isAllowed;
    }

    public void UpdateNumberOfDice(int number)
    {
        int currentNumber = currentDices.Count;
        if (currentNumber == number) return;
        if (currentNumber > number)
        {
            List<Dice> toBeRemoved = new List<Dice>();
            for (int i = 0; i < currentDices.Count; i++)
            {
                if (i < number) continue;
                if (currentDices[i].gameObject.activeInHierarchy == true)
                {
                     currentDices[i].gameObject.SetActive(false);
                     toBeRemoved.Add(currentDices[i]);
                }    
            }           
            foreach (var dice in toBeRemoved)
            {
                currentDices.Remove(dice);
            }
        }
        else if (currentNumber < number)
        {
            for (int i = 0; i < number; i++)
            {
                if (i < currentNumber) continue;
  
                Dice dice = RequestDiceFromPool();
                currentDices.Add(dice);
            }
        }
    }

    public void HideDiceSelection(bool isSelected)
    {
        _tintOverDiceSelection.SetActive(isSelected);
    }

    private void InstantiateDices(int numberOfDice)
    {
        currentDices.Clear();
        for (int i = 0; i < numberOfDice; i++)
        {
            Dice dice = RequestDiceFromPool();
            currentDices.Add(dice);
        }
    }

    private Dice RequestDiceFromPool()
    {
        foreach (var dice in dicePool)
        {
            if (dice.gameObject.activeInHierarchy == false)
            {
                dice.gameObject.SetActive(true);
                return dice;
            }
        }

        Dice newDice = Instantiate(dicePrefab, new Vector3(1, 1, 1), Quaternion.identity);
        newDice.transform.parent = transform;
        newDice.gameObject.SetActive(true);
        newDice.OnRollFinished += Dice_OnRollFinished;
        dicePool.Add(newDice);

        return newDice;
    }

    private IEnumerator RollAllDice()
    {
        AllDiceResult = 0;
        numberDisplay.UpdateText(AllDiceResult);
        Coroutine[] coroutines = new Coroutine[currentDices.Count];
        for(int i = 0; i < currentDices.Count; i++)
        {
            Coroutine c = StartCoroutine(currentDices[i].RollDice());
            coroutines[i] = c;
        }
        foreach (Coroutine c in coroutines)
        {
            yield return c;
        }
        numberDisplay.UpdateText(AllDiceResult);
        OnAllRollsFinished?.Invoke(AllDiceResult);
    }

    private void Dice_OnRollFinished(int sideValue)
    {
        AllDiceResult += sideValue;
    }

    public void RestoreState()
    {
        HideDiceSelection(true);
        UpdateNumberOfDice(2);
    }
}
