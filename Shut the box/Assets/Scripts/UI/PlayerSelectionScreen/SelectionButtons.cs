using UnityEngine;

public class SelectionButtons : MonoBehaviour
{
    [SerializeField] PlayerSelectionItem playerItemPrefab;
    public void AddHumanPlayer()
    {
        InstantiateSelectionButtons();
    }
    public void AddComputerPlayer()
    {
        InstantiateSelectionButtons();
    }

    private void InstantiateSelectionButtons()
    {
        Instantiate(playerItemPrefab, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}
