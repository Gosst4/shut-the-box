using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] Chip[] chips;

    private void Start()
    {
        foreach (Chip chip in chips)
        {
            chip.OnChipClicked += Chip_OnChipClicked;
        }
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

    public void ShowPossibleMoves(int diceValue)
    {
        for (int i = 0; i < chips.Length; i++)
        {
            if (!chips[i].IsActive) continue;
            if (chips[i].GetValue() == diceValue)
            {
                chips[i].SetPossibleMove(true);
            }
            else if (chips[i].HasMatch(chips, diceValue))
            {
                chips[i].SetPossibleMove(true);
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
            if (numberOfChips == 2) break;
        }
        return total;
    }
}
