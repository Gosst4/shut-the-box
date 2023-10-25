using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{    
    private void Start()
    {
        gameObject.SetActive(true);
    }
    public void OnPlayClick()
    {
        gameObject.SetActive(false);
    }
}
