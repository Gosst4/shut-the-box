using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] public ScoreDisplay scoreDisplay;
    [SerializeField] Chip[] chips;     
    public Chip[] Chips { get => chips; }
    public Vector3 TargetEulerAngles { get; private set; }

    private void Start()
    {
        foreach (Chip chip in Chips)
        {
            chip.OnChipClicked += Chip_OnChipClicked;
        }

        TargetEulerAngles = transform.localEulerAngles;        
    }

    private void Chip_OnChipClicked()
    {
        if (DiceManager.Instance.AllDiceResult == CalculateSelectedChips())
        {
            foreach (Chip chip in Chips)
            {
                if (chip.gameObject.activeInHierarchy == false) continue;
                if (chip.IsSelected)
                {
                    StartCoroutine(chip.Fall());
                    DiceManager.Instance.CanRollDice(true);

                    if (!HasMoreThanSix()) DiceManager.Instance.ShowDiceSelection(true);

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
        foreach (Chip chip in Chips)
        {
            if (chip.IsActive)
                score += chip.GetValue();
        }
        return score;
    }

    public bool CanMakeMove(int diceResult)
    {
        if (!HasAnyChips()) return false;
        foreach (Chip chip in Chips)
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
        foreach (Chip chip in Chips)
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

        for (int i = 0; i < Chips.Length; i++)
        {
            if (!Chips[i].IsActive) continue;

            int b = Chips[i].GetValue();            

            if (a == b) continue;

            if (a + b == total) return true;
        }
        return false;
    }

    public bool HasAnyChips()
    {
        foreach (Chip chip in Chips)
        {
            if (chip.IsActive) return true;
        }
        Debug.Log("There is a winner!");
        return false;
    }
    
    private bool HasMoreThanSix()
    {
        return Chips[6].IsActive || Chips[7].IsActive || Chips[8].IsActive;
    }

    private int CalculateSelectedChips()
    {
        int total = 0;
        int numberOfChips = 0;
    
        for (int i = 0; i < Chips.Length; i++)
        {
            if (Chips[i].IsSelected)
            {
                total += Chips[i].GetValue();
                numberOfChips++;
            }            
        }

        if (numberOfChips >= 3) return -1;
        
        return total;
    }
}
