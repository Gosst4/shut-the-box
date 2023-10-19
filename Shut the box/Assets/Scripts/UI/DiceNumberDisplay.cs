using TMPro;
using UnityEngine;

public class DiceNumberDisplay : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(int diceResult)
    {
        text.text = diceResult.ToString();
    }
}
