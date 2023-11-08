using System;

public class Round 
{
    int _currentId = 0;
    int _pointsToWin;
    public Player[] Players { get; }

    public event Action<Player[]> OnRoundFinished;

    public Round(Player[] players)
    {
        Players = players;
        _pointsToWin = 45;
        DiceManager.Instance.OnAllRollsFinished += DiceManager_OnAllRollsFinished;
        _currentId = 0;
        BoardRotator.Instance.RotateTo(Players[_currentId].TargetEulerAngles);
        foreach (var player in Players)
        {
            player.Setup.RestoreSetup();
        }
    }

    public void NextPlayer()
    {
        if (_currentId == Players.Length - 1)
        {
            OnRoundFinished(Players);
        }
        else
        {
            _currentId++;
            BoardRotator.Instance.RotateTo(Players[_currentId].TargetEulerAngles);
            DiceManager.Instance.Reset();
        }
    }

    private void DiceManager_OnAllRollsFinished(int _result)
    {
        bool hasMoreMoves = Players[_currentId].TryTakeTurn(_result);
        CheckWinners();

        if (!hasMoreMoves) NextPlayer();
    }

    private void CheckWinners()
    {
        if (IsGameOver(out int id))
        {
            //_gameOverScreen.gameObject.SetActive(true);
            //_gameOverScreen.UpdateWinnersText(Players[id].Name);
        }
    }

    private bool IsGameOver(out int winnerId)
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].Score >= _pointsToWin || !Players[i].Setup.HasAnyChips())
            {
                winnerId = i;
                return true;
            }
        }
        winnerId = -1;
        return false;
    }
}
