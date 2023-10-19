using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceUISelector : MonoBehaviour
{
    DiceManager game;
    private void Awake()
    {
        game = FindObjectOfType<DiceManager>();
    }
    public void SelectNumberOfDices(int number)
    {
        game.UpdateNumberOfDice(number);
    }    
}
