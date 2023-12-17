using System.Collections.Generic;

public class ComputerHard : ComputerPlayer
{
    private Chip[] chips = new Chip[9];
    private void Start()
    {
        PlayerType = PlayerType.ComputerHard;

        int index = 8;
        for (int i = 0; i < chips.Length; i++)
        {
            chips[i] = Setup.Chips[index];
            index--;
        }
    }

    protected override List<Chip> GetPossibleMoves(int diceResult)
    {
        List<Chip> possibleMoves = new List<Chip>();

        for (int i = 0; i < chips.Length; i++)
        {
            if (!chips[i].IsActive) continue;
            if (chips[i].GetValue() == diceResult)
            {
                possibleMoves.Add(chips[i]);
            }
            else if (chips[i].HasMatch(chips, diceResult))
            {
                possibleMoves.Add(chips[i]);
            }
        }
        return possibleMoves;
    }
}
