using System.Collections.Generic;

public class ComputerNormal : ComputerPlayer
{
    private Chip[] chips = new Chip[9];

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

    private void Start()
    {
        chips[0] = Setup.Chips[4];
        chips[1] = Setup.Chips[5];
        chips[2] = Setup.Chips[3];
        chips[3] = Setup.Chips[6];
        chips[4] = Setup.Chips[2];
        chips[5] = Setup.Chips[7];
        chips[6] = Setup.Chips[1];
        chips[7] = Setup.Chips[8];
        chips[8] = Setup.Chips[0];
    }
}
