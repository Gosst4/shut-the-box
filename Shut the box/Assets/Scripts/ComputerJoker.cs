using System.Collections.Generic;
using UnityEngine;

public class ComputerJoker : ComputerPlayer
{
    private Chip[] chips;

    private void Start()
    {
        chips = Setup.Chips;
    }
    protected override List<Chip> GetPossibleMoves(int diceResult)
    {
        Debug.Log("GetPossibleMoves");
        List<Chip> possibleMoves = new List<Chip>();
        Chip[] shuffledChips = ShuffleChips(chips);

        for (int i = 0; i < shuffledChips.Length; i++)
        {
            if (!shuffledChips[i].IsActive) continue;
            if (shuffledChips[i].GetValue() == diceResult)
            {
                possibleMoves.Add(shuffledChips[i]);
            }
            else if (shuffledChips[i].HasMatch(chips, diceResult))
            {
                possibleMoves.Add(shuffledChips[i]);
            }
        }
        return possibleMoves;
    }

    private Chip[] ShuffleChips(Chip[] chips)
    {
        Debug.Log("ShuffleChips");
        Chip[] newChips = new Chip[chips.Length];
        int[] randIndexes = CreateRandomIntArray(chips.Length);

        for (int i = 0; i < chips.Length; i++)
        {
            newChips[i] = chips[randIndexes[i]];
        }
        return newChips;
    }

    private static int[] CreateRandomIntArray(int arrLength)
    {
        Debug.Log("CreateRandomIntArray");
        int[] arr = new int[arrLength];
        System.Random random = new System.Random();
        int tmp;

        for (int i = 0; i < arrLength; i++)
        {
            tmp = random.Next(arrLength);
            Debug.Log(tmp);
            while (IsDup(tmp, arr))
            {
                tmp = random.Next(arrLength);
            }
            arr[i] = tmp;
        }

        foreach (var item in arr)
        {
            Debug.Log(item);
        }
        return arr;
    }

    private static bool IsDup(int tmp, int[] arr)
    {
        foreach (var item in arr)
        {
            if (item == tmp) return true;
        }
        return false;
    }
}
