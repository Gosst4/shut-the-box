using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    List<Player> players = new List<Player>();

    void Start()
    {
        Player player = new Player("Player 1");
        players.Add(player);
    }
}
