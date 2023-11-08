using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] Player[] _allPlayers;
    [SerializeField] GameOverScreen _gameOverScreen;
    [SerializeField] PlayerSelectionScreen _playerSelectionScreen;

    Round _round;

    private void Start()
    {
        _playerSelectionScreen.OnPlayersNumberSelected += OnPlayersNumberSelected;
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

        DiceManager.Instance.Reset();
    }

    private void OnPlayersNumberSelected(int number)
    {
        Player[] players = GameHelper.GetPlayers(number, _allPlayers);
        _round = new Round(players);
        _round.OnRoundFinished += Round_OnRoundFinished;
    }

    private void Round_OnRoundFinished(Player[] players)
    {
        _round = new Round(players);
        _round.OnRoundFinished += Round_OnRoundFinished;
        DiceManager.Instance.Reset();

    }
}
