using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] PlayerSetup[] playerSetups;

    List<Player> players = new List<Player>();
    int currentId = 0;
    int _id = 0;

    private void Start()
    {
        CreatePlayer(_id, "Player 1", playerSetups[0]);
        CreatePlayer(_id, "Player 2", playerSetups[1]);
        DiceManager.Instance.OnAllRollsFinished += DiceManager_OnAllRollsFinished;
    }

    private Player CreatePlayer(int id, string name, PlayerSetup setup)
    {
        Player player = new Player(id, name, setup);
        _id++;
        players.Add(player);
        return player;
    }



    public void NextPlayer()
    {
        if (!BoardRotator.Instance.IsRotating)
        {
            currentId = (currentId == players.Count - 1) ? 0 : currentId + 1;
            BoardRotator.Instance.RotateTo(players[currentId].Setup.transform.rotation.eulerAngles);
        }
    }

    private void CheckWinners()
    {
        if (IsGameOver())
        {
            Debug.Log("Game over!");
            Debug.Log($"The winner is {players[currentId].Name}!");
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
