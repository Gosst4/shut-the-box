using TMPro;
using UnityEngine;

public class DiceNumberDisplay : MonoBehaviour
{
    TextMeshProUGUI text;
    public static int diceNumber;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        text.text = diceNumber.ToString();
    }
}
