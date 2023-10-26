using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] Player[] _allPlayers;
    [SerializeField] GameOverScreen _gameOverScreen;
    [SerializeField] PlayerSelectionScreen _playerSelectionScreen;

    List<Player> _players = new List<Player>();
    int _currentId = 0;
    int _pointsToWin;

    private void Start()
    {
        _playerSelectionScreen.OnPlayersNumberSelected += OnPlayersNumberSelected;        
        DiceManager.Instance.OnAllRollsFinished += DiceManager_OnAllRollsFinished;
        _pointsToWin = 45;
    }

    private void OnPlayersNumberSelected(int number)
    {
        CreatePlayers(number);
        RotateBoardTo(_players[_currentId]);
    }

    private void CreatePlayers(int number)
    {
        for (int i = 0; i < number; i++)
        {
            _players.Add(_allPlayers[i]);
        }
        foreach (Player player in _allPlayers)
        {
            if (_players.Contains(player)) continue;
            else player.gameObject.SetActive(false);
        }
    }

    public void NextPlayer()
    {
        if (!BoardRotator.Instance.IsRotating)
        {
            if (_currentId == _players.Count - 1)
            {
                _currentId = 0;
                DiceManager.Instance.RestoreState();
                foreach (Player player in _players)
                {
                    player.Setup.RestoreSetup();
                }
            }
            else { _currentId++;  }

            RotateBoardTo(_players[_currentId]);
            DiceManager.Instance.DiceSelection(false);
        }
    }

    private void RotateBoardTo(Player player)
    {
        BoardRotator.Instance.RotateTo(player.TargetEulerAngles);
    }

    private void CheckWinners()
    {
        if (IsGameOver())
        {
            _gameOverScreen.gameObject.SetActive(true);
            _gameOverScreen.UpdateWinnersText(_players[_currentId].Name);
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

    private bool IsGameOver()
    {
        foreach (var player in _players)
        {
            if (player.Score >= _pointsToWin) return true;
            if (!player.Setup.HasAnyChips()) return true;
        }
        return false;
    }
}
