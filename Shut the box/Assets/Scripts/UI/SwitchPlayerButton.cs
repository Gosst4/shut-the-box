using UnityEngine;
using UnityEngine.UI;

public class SwitchPlayerButton : MonoBehaviour
{
    Button nextPlayerButton;
    void Start()
    {
        nextPlayerButton = GetComponent<Button>();
    }

    void Update()
    {
        if (BoardRotator.Instance.IsRotating) 
        {
            nextPlayerButton.enabled = false;
        }
        else
        {
            nextPlayerButton.enabled = true;
        }
    }
}
