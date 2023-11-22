using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComputerPlayer : Player
{
    private void Awake()
    {
        Setup = GetComponent<PlayerSetup>();
        PlayerType = PlayerType.Computer;
    }
    public override void UnblockMovement()
    {
        StartCoroutine(RollDice());
    }

    public override bool TryTakeTurn(int diceResult)
    {
        if (Setup.CanMakeMove(diceResult))
        {
            StartCoroutine(MakeMove(diceResult));            
            return true;
        }
        else
        {
            Score += Setup.CalculateRound();
            Setup.UpdateScoreInUi(Score);
            return false;
        }
    }

    protected abstract List<Chip> GetPossibleMoves(int diceResult);

    private IEnumerator RollDice()
    {
        yield return new WaitForSeconds(3f);
        DiceManager.Instance.RollTheDice();
    }

    IEnumerator MakeMove(int diceResult)
    {        
        Coroutine c = StartCoroutine(FinishMove(diceResult));
        yield return c;
        yield return new WaitForSeconds(0.5f);
        DiceManager.Instance.RollTheDice();
    }

    IEnumerator FinishMove(int diceResult)
    {
        Setup.ShowPossibleMoves(diceResult);

        yield return new WaitForSeconds(1f);

        List<Chip> possibleMoves = GetPossibleMoves(diceResult);
        Move(diceResult, possibleMoves);

        yield return new WaitForSeconds(1f);
    }

    protected void Move(int diceResult, List<Chip> chips)
    {
        for (int i = 0; i < chips.Count; i++)
        {
            if (chips[i].GetValue() == diceResult)
            {
                chips[i].Select();
                break;
            }
            else if (chips[i].HasMatch(chips.ToArray(), diceResult))
            {
                chips[i].Select();
                GetMatch(chips[i], chips, diceResult).Select();
                break;
            }
        }
    }

    private Chip GetMatch(Chip chipToCompare, List<Chip> chips, int total)
    {
        foreach (var chip in chips)
        {
            if (chipToCompare.GetValue() + chip.GetValue() == total)
                return chip;
        }
        return null;
    }
}