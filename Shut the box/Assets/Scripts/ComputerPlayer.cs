using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : Player
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
        List<Chip> chips = Setup.ShowPossibleMoves(diceResult);

        yield return new WaitForSeconds(1f);

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
        yield return new WaitForSeconds(1f);
    }

    public Chip GetMatch(Chip chipToCompare, List<Chip> chips, int total)
    {
        foreach (var chip in chips)
        {
            if (chipToCompare.GetValue() + chip.GetValue() == total)
                return chip;
        }
        return null;
    }
}
