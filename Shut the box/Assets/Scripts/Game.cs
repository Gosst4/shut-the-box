using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] Player[] allPlayers;

    List<Player> players = new List<Player>();
    int currentId = 0;

    private void Start()
    {
        CreatePlayers(2);
        DiceManager.Instance.OnAllRollsFinished += DiceManager_OnAllRollsFinished;
        
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
