using System;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] Chip[] chips;
    [SerializeField] ScoreDisplay scoreDisplay;

    public void ShowPossibleMoves(int diceValue)
    {
        foreach (Chip chip in chips)
        {
            if (!chip.IsActive) continue;
            if (chip.GetValue() == diceValue)
            {
                chip.SetPossibleMove();
            }
        }       
    }
    public int CalculateRound()
    {
        int score = 0;
        foreach (Chip chip in chips)
        {
            if (chip.IsActive)
                score += chip.GetValue();
        }
        return score;
    }

    public bool CanMakeMove(int diceResult)
    {
        if (!HasAnyChips()) return false;
        foreach (Chip chip in chips)
        {
            if (chip.IsActive && chip.GetValue() == diceResult) 
                return true;
        }
        return false;
    }

/*    public void SetClickability(bool isClickable)
    {
        foreach(Chip chip in chips)
        {
            chip.SetClickability(isClickable);
        }
    }*/

    public bool HasAnyChips()
    {
        foreach (Chip chip in chips)
        {
            if (chip.IsActive) return true;
        }
        Debug.Log("There is a winner!");
        return false;
    }

    internal void UpdateScoreInUi(int score)
    {
        scoreDisplay.UpdateText(score);
    }
}
