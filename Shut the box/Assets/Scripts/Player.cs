using UnityEngine;

public class Player : MonoBehaviour
{
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
            Setup.UpdateScoreInUi(Score);
        }
    }
}
