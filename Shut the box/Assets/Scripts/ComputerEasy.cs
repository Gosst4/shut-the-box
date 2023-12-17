using System.Collections.Generic;

public class ComputerEasy : ComputerPlayer
{
    private Chip[] chips;

    private void Start()
    {
        chips = Setup.Chips;
        PlayerType = PlayerType.ComputerEasy;
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
