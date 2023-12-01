using System;
using UnityEngine;

public class PlayerSelectionScreen : MonoBehaviour
{
    [SerializeField] PlayerSelectionItem playerItemPrefab;
    [SerializeField] SelectionButtons[] selectionButtons;

    public event Action<int> OnPlayersNumberSelected;
    public void SetNumberOfPlayers(int numberOfPlayers)
    {
        OnPlayersNumberSelected(numberOfPlayers);
        gameObject.SetActive(false);
    }
}
