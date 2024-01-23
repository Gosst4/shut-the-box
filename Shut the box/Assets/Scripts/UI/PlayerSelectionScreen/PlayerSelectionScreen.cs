using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionScreen : MonoBehaviour
{
    [SerializeField] PlayerSelectionItem[] items;    

    public event Action<List<PlayerData>> OnPlayersNumberSelected;

    public void OnStartClick()
    {
        List<PlayerData> dataList = new List<PlayerData>();
        foreach (var item in items)
        {
            PlayerData data = item.GetPlayerData();
            if (data != null) dataList.Add(data);
        }
        OnPlayersNumberSelected(dataList);

        BoardRotator.Instance.StartRotation();
        gameObject.SetActive(false);
    }
}

