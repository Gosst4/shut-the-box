using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] int numberOfDice;
    [SerializeField] Dice dicePrefab;
    [SerializeField] DiceNumberDisplay numberDisplay;

    List<Dice> diceList = new List<Dice>();
    int _result;

    private void Start()
    {
        for (int i = 0;  i < numberOfDice; i++)
        {
            Dice dice = Instantiate(dicePrefab, new Vector3(i + 1, i, i + 1), Quaternion.identity);
            diceList.Add(dice);
            dice.OnRollFinished += Dice_OnRollFinished;
        }       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Dice._isRolling)
        {
            StartCoroutine(RollAllDice());
        }
    }

    IEnumerator RollAllDice()
    {
        _result = 0;
        numberDisplay.UpdateText(_result);
        Coroutine[] coroutines = new Coroutine[numberOfDice];
        for(int i = 0; i < numberOfDice; i++)
        {
            Coroutine c = StartCoroutine(diceList[i].RollDice());
            coroutines[i] = c;
        }
        foreach (Coroutine c in coroutines)
        {
            yield return c;
        }
        numberDisplay.UpdateText(_result);
    }

    private void Dice_OnRollFinished(int sideValue)
    {
        _result += sideValue;
    }
}
