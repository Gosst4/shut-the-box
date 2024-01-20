using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceResultDisplay : MonoBehaviour
{
    [SerializeField] ResultDisplay resultDisplayPrefab;

    public void ShowResult(int firstDice, int secondDice) 
    {
        ResultDisplay display = Instantiate(resultDisplayPrefab, transform);
        display.UpdateText(firstDice + secondDice);
        StartCoroutine(HideCor(display));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowResult(1, 2);
        }
    }

    private IEnumerator HideCor(ResultDisplay display)
    {
        yield return new WaitForSeconds(2);
        Destroy(display.gameObject);
    }
}
