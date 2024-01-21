using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceResultScreen : MonoBehaviour
{
    [SerializeField] ResultDisplay resultDisplayPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //ShowResult(1, 2);
        }
    }

    public void ShowResultScreen(List<int> allDiceResult)
    {
        ResultDisplay display = Instantiate(resultDisplayPrefab, transform);
        if (allDiceResult.Count == 1)
        {
            display.ShowResult(allDiceResult[0]);
        }
        else if (allDiceResult.Count == 2)
        {
            display.ShowResult(allDiceResult[0], allDiceResult[1]);
        }
        StartCoroutine(HideCor(display));
    }

    private IEnumerator HideCor(ResultDisplay display)
    {
        yield return new WaitForSeconds(2);
        Destroy(display.gameObject);
    }
}
