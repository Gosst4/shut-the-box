using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    [SerializeField] Number _number;

    public bool IsActive {  get; private set; } = true;

    public void OnMouseDown()
    {
        IsActive = false;
        GetComponent<Rigidbody>().useGravity = true;
    }

    public int GetValue()
    {
        return (int)_number;
    }
}