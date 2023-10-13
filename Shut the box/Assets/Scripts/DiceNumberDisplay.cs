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

    void Update()
    {
        text.text = diceNumber.ToString();
    }
}
