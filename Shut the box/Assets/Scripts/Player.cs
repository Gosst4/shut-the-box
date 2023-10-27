using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ScoreDisplay scoreDisplay;

    public string Name { get; private set; }
    public int Score { get; private set; }
    public PlayerSetup Setup { get; private set; }

    public Vector3 TargetEulerAngles { get; private set; }

    private void Awake()
    {
        Setup = GetComponent<PlayerSetup>();
    }

    private void Start()
    {
        TargetEulerAngles = transform.localEulerAngles;
    }

    public void UnblockMovement(int diceResult)
    {
        if (Setup.CanMakeMove(diceResult))
        {
            Setup.ShowPossibleMoves(diceResult);
        }
        else
        {
            Score += Setup.CalculateRound();
            UpdateScoreInUi(Score);
        }
    }

    public void SetPlayerName(string name)
    {
        Name = name;
        scoreDisplay.SetPlayerName(Name);
    }

    public void ResetScore()
    {
        Score = 0; 
        UpdateScoreInUi(Score);
    }

    private void UpdateScoreInUi(int score)
    {
        scoreDisplay.UpdateScoreText(score);
    }
}
