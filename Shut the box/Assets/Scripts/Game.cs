using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] int numberOfDice;
    [SerializeField] Dice dicePrefab;

    List<Dice> diceList = new List<Dice>();

    private void Start()
    {
        for (int i = 0;  i < numberOfDice; i++)
        {
            Dice dice = Instantiate(dicePrefab, new Vector3(i + 1, i, i + 1), Quaternion.identity);
            diceList.Add(dice);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Dice._isRolling)
        {
            foreach (Dice dice in diceList)
            {
                dice.RollDice();
            }
        }
    }
}
