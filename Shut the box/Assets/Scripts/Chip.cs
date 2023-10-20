using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    [SerializeField] Number _number;

    BoxCollider _boxCollider;
    public bool IsActive {  get; private set; } = true;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    public void OnMouseDown()
    {
        IsActive = false;
        GetComponent<Rigidbody>().useGravity = true;
        // запустить корутину отключения фишки после падения
    }

    public int GetValue()
    {
        return (int)_number;
    }

    public void SetClickability(bool isClicable)
    {
        _boxCollider.enabled = isClicable;
    }
}