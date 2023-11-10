using System;

public class Round 
{
    int _currentId = 0;
    public Player[] _players;

    public event Action<Player[]> OnRoundFinished;

    public Round(Player[] players)
    {
        _players = players;        
        DiceManager.Instance.OnAllRollsFinished += DiceManager_OnAllRollsFinished;
        BoardRotator.Instance.RotateTo(_players[_currentId].TargetEulerAngles, 1f);
        foreach (var player in _players)
        {
            player.Setup.RestoreSetup();
        }
    }

    public void NextPlayer()
    {
        if (_currentId == _players.Length - 1)
        {
            OnRoundFinished(_players);
            DiceManager.Instance.OnAllRollsFinished -= DiceManager_OnAllRollsFinished;
        }
        else
        {
            _currentId++;
            BoardRotator.Instance.RotateTo(_players[_currentId].TargetEulerAngles, 1f);
            DiceManager.Instance.Reset();
        }
    }

    private void DiceManager_OnAllRollsFinished(int _result)
    {
        bool hasMoreMoves = _players[_currentId].TryTakeTurn(_result);
        Game.Instance.CheckWinners();

        if (!hasMoreMoves) NextPlayer();
    }
}
