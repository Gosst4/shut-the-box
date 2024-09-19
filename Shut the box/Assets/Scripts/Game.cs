using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] PlayerSetup[] _allPlayerSetups;
    [SerializeField] GameOverScreen _gameOverScreen;
    [SerializeField] DiceResultScreen _diceResultScreen;
    [SerializeField] Hud _hudScreen;
    //[SerializeField] PlayerSelectionScreen _playerSelectionScreen;

    Round _round;
    const int _pointsToWin = 10;
    Player[] _players;

	static Game instance;
    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Game>();
            }
            return instance;
        }
    }

	public void Restart()
    {
        foreach (var setup in _allPlayerSetups)
        {
            setup.gameObject.SetActive(true);
            setup.RestoreSetup();

            var player = setup.GetComponent<Player>();
			if (player != null)
			{
				Destroy(player);
			}
        }

		FindObjectOfType<PlayerSelectionScreen>(true).gameObject.SetActive(true);

        DiceManager.Instance.ResetDice();
    }

    public void OnPlayersNumberSelected(List<PlayerData> datas)
    {
        _players = GameHelper.GetPlayers(datas, _allPlayerSetups);
        _round = new Round(_players);
        _round.OnRoundFinished += Round_OnRoundFinished;

        _hudScreen.gameObject.SetActive(true);
		_diceResultScreen.gameObject.SetActive(true);
	}

    public void CheckWinners()
    {
        if (IsGameOver())
        {
            _gameOverScreen.gameObject.SetActive(true);
            _gameOverScreen.UpdateWinnersText(GetWinner().Name);

			_hudScreen.gameObject.SetActive(false);
			_diceResultScreen.gameObject.SetActive(false);
		}
    }

    private bool IsGameOver()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i].Score >= _pointsToWin || !_players[i].Setup.HasAnyChips())
            {
                return true;
            }
        }
        return false;
    }
    private void Round_OnRoundFinished(Player[] players)
    {
        _round.OnRoundFinished -= Round_OnRoundFinished;
        _round = new Round(players);
        _round.OnRoundFinished += Round_OnRoundFinished;
        DiceManager.Instance.ResetDice();
    }

    private Player GetWinner()
    {
        int winningScore = _players[0].Score;
        Player winner = _players[0];
        foreach (var player in _players)
        {
            if (player.Score < winningScore)
            {
                winningScore = player.Score;
                winner = player;
            }
        }
        return winner;
    }
}
