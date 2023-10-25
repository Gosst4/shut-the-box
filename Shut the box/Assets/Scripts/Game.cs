using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] Player[] allPlayers;
    [SerializeField] GameOverScreen gameOverScreen;
    [SerializeField] PlayerSelectionScreen _playerSelectionScreen;

    List<Player> players = new List<Player>();
    int currentId = 0;
    Player _currentPlayer;

    private void Start()
    {
        _playerSelectionScreen.OnPlayersNumberSelected += OnPlayersNumberSelected;        
        DiceManager.Instance.OnAllRollsFinished += DiceManager_OnAllRollsFinished;
    }

    private void OnPlayersNumberSelected(int number)
    {
        CreatePlayers(number);
        _currentPlayer = players[0];
        RotateBoardTo(players[currentId]);
    }

    private void CreatePlayers(int number)
    {
        for (int i = 0; i < number; i++)
        {
            players.Add(allPlayers[i]);
        }
        foreach (Player player in allPlayers)
        {
            if (players.Contains(player)) continue;
            else player.gameObject.SetActive(false);
        }
    }

    public void NextPlayer()
    {
        if (!BoardRotator.Instance.IsRotating)
        {
            if (currentId == players.Count - 1)
            {
                currentId = 0;
                foreach (Player player in players)
                {
                    player.Setup.RestoreSetup();
                }
            }
            else { currentId++;  }

            RotateBoardTo(players[currentId]);       
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
            gameOverScreen.gameObject.SetActive(true);
            gameOverScreen.UpdateWinnersText(players[currentId].Name);
        }
    }
    private void DiceManager_OnAllRollsFinished(int _result)
    {
        /*        if (!players[currentId].Setup.CanMakeMove(_result))
                {
                    NextPlayer();
                }*/
        players[currentId].UnblockMovement(_result);
        CheckWinners();
    }

    private bool IsGameOver()
    {
        foreach (var player in players)
        {
            if (player.Score >= 10) return true;
            if (!player.Setup.HasAnyChips()) return true;
        }
        return false;
    }
}
