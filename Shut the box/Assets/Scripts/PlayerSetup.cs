using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] public ScoreDisplay scoreDisplay;
    [SerializeField] Chip[] chips;

    public Vector3 TargetEulerAngles { get; private set; }

    private void Start()
    {
        foreach (Chip chip in chips)
        {
            chip.OnChipClicked += Chip_OnChipClicked;
        }

        TargetEulerAngles = transform.localEulerAngles;        
    }

    private void Chip_OnChipClicked()
    {
        if (DiceManager.Instance.AllDiceResult == CalculateSelectedChips())
        {
            foreach (Chip chip in chips)
            {
                if (chip.gameObject.activeInHierarchy == false) continue;
                if (chip.IsSelected)
                {
                    StartCoroutine(chip.Fall());
                    DiceManager.Instance.CanRollDice(true);

                    if (!HasMoreThanSix()) DiceManager.Instance.HideDiceSelection(false);

                    Game.Instance.CheckWinners();
                }
                else
                {
                    chip.SetPossibleMove(false);
                    chip.RemoveSelection();
                }
            }
        }
    }

    public List<Chip> ShowPossibleMoves(int diceValue)
    {
        List<Chip> possibleMoves= new List<Chip>();

        for (int i = 0; i < chips.Length; i++)
        {
            if (!chips[i].IsActive) continue;
            if (chips[i].GetValue() == diceValue)
            {
                chips[i].SetPossibleMove(true);
                possibleMoves.Add(chips[i]);
            }
            else if (chips[i].HasMatch(chips, diceValue))
            {
                chips[i].SetPossibleMove(true);
                possibleMoves.Add(chips[i]);
            }
        }   
        return possibleMoves;
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
            if (!chip.IsActive) continue;
            if (chip.GetValue() == diceResult)
                return true;
            else if (CanMakeASum(chip, diceResult))
                return true;
        }
        return false;
    }

    public void RestoreSetup()
    {        
        foreach (Chip chip in chips)
        {
            chip.RestoreState();
        }
    }
    public void UpdateScoreInUi(int score)
    {
        scoreDisplay.UpdateScoreText(score);
    }

    private bool CanMakeASum(Chip chip, int total)
    {
        int a = chip.GetValue();

        for (int i = 0; i < chips.Length; i++)
        {
            if (!chips[i].IsActive) continue;

            int b = chips[i].GetValue();            

            if (a == b) continue;

            if (a + b == total) return true;
        }
        return false;
    }

    public bool HasAnyChips()
    {
        foreach (Chip chip in chips)
        {
            if (chip.IsActive) return true;
        }
        Debug.Log("There is a winner!");
        return false;
    }
    
    private bool HasMoreThanSix()
    {
        return chips[6].IsActive || chips[7].IsActive || chips[8].IsActive;
    }

    private int CalculateSelectedChips()
    {
        int total = 0;
        int numberOfChips = 0;
    
        for (int i = 0; i < chips.Length; i++)
        {
            if (chips[i].IsSelected)
            {
                total += chips[i].GetValue();
                numberOfChips++;
            }            
        }

        if (numberOfChips >= 3) return -1;
        
        return total;
    }
}
