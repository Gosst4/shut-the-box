using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player // : MonoBehaviour
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public Player(string name)
    {
        Name = name;
        Score = 0;
    }
}
