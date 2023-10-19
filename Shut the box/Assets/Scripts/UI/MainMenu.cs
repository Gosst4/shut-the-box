using UnityEngine;
using UnityEngine.UI;

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
