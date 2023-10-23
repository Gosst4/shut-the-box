using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    public string Name { get; private set; }
    public int Score { get; private set; }
    public PlayerSetup Setup { get; private set; }

    public int Id { get; }

    public Player(int id, string name, PlayerSetup setup)
    {
        Name = name;
        Score = 0;
        Setup = setup;
        Id = id;
    }

    public void UnblockMovement(int diceResult)
    {
        if (Setup.CanMakeMove(diceResult))
        {
            Setup.ShowPossibleMoves(diceResult);
        }
        else
        {
            Score += Setup.CalculateRound();
            Setup.UpdateScoreInUi(Score);
        }
    }
}
