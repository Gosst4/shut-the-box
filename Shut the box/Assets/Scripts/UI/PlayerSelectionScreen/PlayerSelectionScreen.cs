using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionScreen : MonoBehaviour
{
    [SerializeField] Button _startButton;
	[SerializeField] PlayerSelectionItem[] items;    

    public event Action<List<PlayerData>> OnPlayersNumberSelected;

	private void Start()
	{
        OnPlayersNumberSelected += Game.Instance.OnPlayersNumberSelected;
        _startButton.interactable = false;
	}

	public void OnStartClick()
    {
        List<PlayerData> dataList = new List<PlayerData>();
        foreach (var item in items)
        {
            PlayerData data = item.GetPlayerData();
            if (data != null) 
                dataList.Add(data);
        }
        OnPlayersNumberSelected(dataList);

        BoardRotator.Instance.StartRotation();
        gameObject.SetActive(false);
    }

	public void OnHomeClick()
	{
		FindObjectOfType<MainMenu>(true).gameObject.SetActive(true);
        gameObject.SetActive(false);
	}

	public void CheckStartButton()
	{
		int selected = 0;
		foreach (var item in items)
		{
			var data = item.GetPlayerData();
			if (item.GetPlayerData() != null)
			{
				selected++;
				if (selected > 1)
				{
					_startButton.interactable = true;
					return;
				}
			}
		}
		_startButton.interactable = false;
	}
}

