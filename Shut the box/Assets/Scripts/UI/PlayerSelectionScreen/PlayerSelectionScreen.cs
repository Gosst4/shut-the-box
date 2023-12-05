using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionScreen : MonoBehaviour
{
    [SerializeField] PlayerSelectionItem[] items;
    
    List<PlayerSelectionItem> selectedItems;

    public event Action<int> OnPlayersNumberSelected;

    public void SetNumberOfPlayers(int numberOfPlayers)
    {
        OnPlayersNumberSelected(numberOfPlayers);
        gameObject.SetActive(false);
    }

    public void OnStartClick()
    {
        foreach (var item in items)
        {
            Debug.Log(item.GetPlayerData()?._playerType);
            PlayerData data = item.GetPlayerData();
        }
    }
}

