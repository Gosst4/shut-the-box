using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceUISelector : MonoBehaviour
{
    Game game;
    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }
    public void SelectNumberOfDices(int number)
    {
        game.UpdateNumberOfDice(number);
    }    
}
