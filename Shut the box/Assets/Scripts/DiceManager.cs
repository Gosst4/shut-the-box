using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{   
    [SerializeField] Dice dicePrefab;
    [SerializeField] DiceResultScreen diceResultScreen;
    [SerializeField] Hud hud;

    public List<int> AllDiceResult {  get; private set; } = new List<int>();
    public List<Dice> CurrentDice { get; private set; } = new List<Dice>();

    int startingNumberOfDice = 2;
    List<Dice> dicePool = new List<Dice>();
       
    public event Action<List<int>> OnAllRollsFinished;

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
        InstantiateDice(startingNumberOfDice);
        CanRollDice(true);
        AllowDiceSelection(false);
    }

    public int GetAllDiceResult()
    {
        int total = 0;
        foreach (int i in AllDiceResult)
        {
            total += i;
        }
        return total;
    }

    private void PregenerateDicePool(int v)
    {
        dicePool.Clear();
        int pos = 0;
        for (int i = 0; i < v; i++)
        {
            Dice dice = Instantiate(dicePrefab, new Vector3(pos, 2, pos), Quaternion.identity);
            pos += 3;
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
            AllowDiceSelection(false);
            StartCoroutine(RollAllDice());
        }
    }

    public void CanRollDice(bool isAllowed)
    {
        hud.CanRollDice(isAllowed);
    }

    public void ShowRollButton(bool isShown)
    {
        hud.ShowRollButton(isShown);
        CanRollDice(true);
    }
   
    public void OnSelectNumberOfDiceClick()
    {
        if (CurrentDice.Count == 2)        
            UpdateNumberOfDice(1);        
        else        
            UpdateNumberOfDice(2);
    }

    public void AllowDiceSelection(bool canChange)
    {
        diceResultScreen.AllowDiceSelection(canChange);
    }

    public void ResetDice()
    {
        AllowDiceSelection(false);
        //ShowRollButton(true);
        UpdateNumberOfDice(2);
    }
    private void UpdateNumberOfDice(int number)
    {
        int currentNumber = CurrentDice.Count;
        if (currentNumber == number) return;
        if (currentNumber > number)
        {
            List<Dice> toBeRemoved = new List<Dice>();
            for (int i = CurrentDice.Count - 1; i >= 0; i--)
            {
                if (i < number) continue;
                if (CurrentDice[i].gameObject.activeInHierarchy == true)
                {
                    CurrentDice[i].gameObject.SetActive(false);
                    toBeRemoved.Add(CurrentDice[i]);
                }
            }
            foreach (var dice in toBeRemoved)
            {
                CurrentDice.Remove(dice);
            }
        }
        else if (currentNumber < number)
        {
            for (int i = 0; i < number; i++)
            {
                if (i < currentNumber) continue;

                Dice dice = RequestDiceFromPool();
                CurrentDice.Add(dice);
            }
        }
        diceResultScreen.UpdateDiceInfo(number);
    }

    private void InstantiateDice(int numberOfDice)
    {
        CurrentDice.Clear();
        for (int i = 0; i < numberOfDice; i++)
        {
            Dice dice = RequestDiceFromPool();
            CurrentDice.Add(dice);
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
        AllDiceResult.Clear();
        diceResultScreen.ClearDiceResult();

        Coroutine[] coroutines = new Coroutine[CurrentDice.Count];

        int pos = 5;
        for (int i = 0; i < CurrentDice.Count; i++)
        {
            Coroutine c = StartCoroutine(CurrentDice[i].RollDice(pos));
            coroutines[i] = c;
            // pos += 6;
        }
        foreach (Coroutine c in coroutines)
        {
            yield return c;
        }

        diceResultScreen.ShowResultScreen(AllDiceResult);
        OnAllRollsFinished?.Invoke(AllDiceResult);
    }

    private void Dice_OnRollFinished(int sideValue)
    {
        AllDiceResult.Add(sideValue);
    }
}
