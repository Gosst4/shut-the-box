using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void UpdateText(int score)
    {
        text.text = score.ToString();
    }
}
