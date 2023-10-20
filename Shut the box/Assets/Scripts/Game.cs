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
        currentId = (currentId == players.Count - 1) ? 0 : currentId + 1;
        FindAnyObjectByType<BoardRotator>().RotateTo(players[currentId].Setup.transform.rotation.eulerAngles);

        Debug.Log(currentId);
        //currentPlayer.UnblockMovement();
    }
}
