using TMPro;
using UnityEngine;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void UpdateText(int diceResult)
    {
        text.text = diceResult.ToString();
    }
}
