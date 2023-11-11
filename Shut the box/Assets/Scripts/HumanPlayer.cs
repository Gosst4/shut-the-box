using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    private void Awake()
    {
        Setup = GetComponent<PlayerSetup>();
        PlayerType = PlayerType.Human;
    }

    public override bool TryTakeTurn(int diceResult)
    {
        if (Setup.CanMakeMove(diceResult))
        {
            Setup.ShowPossibleMoves(diceResult);
            return true;
        }
        else
        {
            Score += Setup.CalculateRound();
            Setup.UpdateScoreInUi(Score);
            return false;
        }
    }

    public override void UnblockMovement()
    {
        DiceManager.Instance.Reset();
    }
}
