using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextSpawner : MonoBehaviour
{
    [SerializeField] ScoreDisplay scoreTextPrefab;

    public void Spawn(int score)
    {
        ScoreDisplay instance = Instantiate<ScoreDisplay>(scoreTextPrefab, transform);
        instance.UpdateText(score);
    }
}
