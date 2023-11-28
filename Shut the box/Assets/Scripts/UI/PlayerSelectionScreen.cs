using System;
using UnityEngine;

public class PlayerSelectionScreen : MonoBehaviour
{
    //[SerializeField] 

    public event Action<int> OnPlayersNumberSelected;
    public void SetNumberOfPlayers(int numberOfPlayers)
    {
        OnPlayersNumberSelected(numberOfPlayers);
        gameObject.SetActive(false);
    }
}
