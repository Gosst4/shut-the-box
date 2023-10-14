using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    [SerializeField] Number _number;

    bool _isActive;

    public void OnMouseDown()
    {
        _isActive = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
}