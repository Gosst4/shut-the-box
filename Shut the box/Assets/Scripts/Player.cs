using System;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public string Name { get; private set; }
    public int Score { get; protected set; }
    public PlayerSetup Setup { get; protected set; }
    public PlayerType PlayerType { get; protected set; }

    public abstract bool TryTakeTurn(int diceResult);
    public abstract void UnblockMovement();

    public void SetPlayerName(string name)
    {
        Name = name;
        Setup.scoreDisplay.SetPlayerName(Name);
    }

    public void ResetScore()
    {
        Score = 0; 
        Setup.UpdateScoreInUi(Score);
    }
}

public enum PlayerType { Human, ComputerEasy, ComputerNormal, ComputerHard}
