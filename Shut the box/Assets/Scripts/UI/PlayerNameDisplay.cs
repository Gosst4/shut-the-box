using TMPro;
using UnityEngine;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;

    public void SetPlayerName(string name)
    {
        nameText.text = name;
    }

    public void OnBackClick()
    {
        Destroy(gameObject);
    }
}
