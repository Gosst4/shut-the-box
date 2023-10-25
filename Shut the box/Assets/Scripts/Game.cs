using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] Player[] allPlayers;

    List<Player> players = new List<Player>();
    int currentId = 0;
    Player _currentPlayer;

    private void Start()
    {
        CreatePlayers(2);
        DiceManager.Instance.OnAllRollsFinished += DiceManager_OnAllRollsFinished;
        _currentPlayer = players[0];
        RotateBoardTo(players[currentId]);
    }

    private void CreatePlayers(int number)
    {
        for (int i = 0; i < number; i++)
        {
            players.Add(allPlayers[i]);
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
        BoardRotator.Instance.RotateTo(player.Setup.transform.localRotation.eulerAngles);
    }

    private void CheckWinners()
    {
        if (IsGameOver())
        {
            var gameOverScreen = FindObjectOfType<GameOverScreen>();
            gameOverScreen?.gameObject.SetActive(true);
            gameOverScreen?.UpdateWinnersText($"The winner is {players[currentId].Name}!");
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
            if (player.Score >= 45)
            {
                return true;
            }
            if (!player.Setup.HasAnyChips())
            {
                return true;
            }
        }
        return false;
    }
}
