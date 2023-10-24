using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] Chip[] chips;
    [SerializeField] ScoreDisplay scoreDisplay;

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

                    if (!HasMoreThanSix()) DiceManager.Instance.ShowDiceSelection(false);
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
            if (chips[i].GetValue() <= diceValue)
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

    private bool CanMakeASum(Chip chip, int total)
    {
        for (int i = 1; i < chips.Length; i++)
        {
            if (!chips[i].IsActive) continue;

            int a = chips[i].GetValue();
            int b = chip.GetValue();

            if (a == b) continue;

            if (a + b == total) return true;
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
    
    private bool HasMoreThanSix()
    {
        return chips[6].IsActive || chips[7].IsActive || chips[8].IsActive;
    }

    private int CalculateSelectedChips()
    {
        int total = 0;
        foreach (Chip chip in chips)
        {
            if (chip.IsSelected)
                total += chip.GetValue();
        }
        return total;
    }
}
