using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] Player[] _allPlayers;
    [SerializeField] GameOverScreen _gameOverScreen;
    [SerializeField] PlayerSelectionScreen _playerSelectionScreen;

    Player[] _players;
    int _currentId = 0;
    int _pointsToWin;

    private void Start()
    {
        _playerSelectionScreen.OnPlayersNumberSelected += OnPlayersNumberSelected;        
        DiceManager.Instance.OnAllRollsFinished += DiceManager_OnAllRollsFinished;
        _pointsToWin = 45;
    }

    public void Restart()
    {
        foreach (var player in _allPlayers)
        {
            player.gameObject.SetActive(true);
            player.ResetScore();
            player.Setup.RestoreSetup();
        }
        _playerSelectionScreen.gameObject.SetActive(true);
        _currentId = 0;
        ResetBoard();
    }

    private void OnPlayersNumberSelected(int number)
    {
        _players = GameHelper.GetPlayers(number, _allPlayers);
        RotateBoardTo(_players[_currentId]);
    }

    public void NextPlayer()
    {
        if (!BoardRotator.Instance.IsRotating)
        {
            if (_currentId == _players.Length - 1)
            {
                _currentId = 0;
                DiceManager.Instance.RestoreState();
                foreach (Player player in _players)
                {
                    player.Setup.RestoreSetup();
                }
            }
            else { _currentId++;  }

            ResetBoard();
        }
    }

    private void ResetBoard()
    {
        RotateBoardTo(_players[_currentId]);
        DiceManager.Instance.HideDiceSelection(true);
        DiceManager.Instance.CanRollDice(true);
    }

    private void RotateBoardTo(Player player)
    {
        BoardRotator.Instance.RotateTo(player.TargetEulerAngles);
    }

    private void CheckWinners()
    {
        if (IsGameOver(out int id))
        {
            _gameOverScreen.gameObject.SetActive(true);
            _gameOverScreen.UpdateWinnersText(_players[id].Name);
        }
    }
    private void DiceManager_OnAllRollsFinished(int _result)
    {
        /*        if (!players[currentId].Setup.CanMakeMove(_result))
                {
                    NextPlayer();
                }*/
        _players[_currentId].UnblockMovement(_result);
        CheckWinners();
    }

    private bool IsGameOver(out int winnerId)
    {
        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i].Score >= _pointsToWin || !_players[i].Setup.HasAnyChips())
            {
                winnerId = i;
                return true;
            }
        }
        winnerId = -1;
        return false;
    }
}
