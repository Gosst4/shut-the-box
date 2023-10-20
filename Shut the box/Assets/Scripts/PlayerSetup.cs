using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] Chip[] chips;

    public int CalculateRound()
    {
        int score = 0;
        foreach (Chip chip in chips)
        {
            if (chip.IsActive)
                score += chip.GetValue();
        }
        return score;
    }

}
