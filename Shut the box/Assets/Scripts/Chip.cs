using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    [SerializeField] Number _number;
    [SerializeField] GameObject _possibleMove;

    BoxCollider _boxCollider;
    Vector3 _startingPosition;
    public bool IsSelected { get; private set; } = false;
    public bool IsActive {  get; private set; } = true;

    public event Action OnChipClicked;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        _startingPosition = transform.position;
    }
    public void OnMouseDown()
    {
        HighlightSelection();
    }

    public int GetValue()
    {
        return (int)_number;
    }

    public void SetPossibleMove(bool isClicable)
    {
        _possibleMove.SetActive(isClicable);
        _boxCollider.enabled = isClicable;
    }

    public void Fall()
    {
        IsActive = false;
        GetComponent<Rigidbody>().useGravity = true;
        IsSelected = false;
        // запустить корутину отключения фишки после падения
    }

    private void HighlightSelection()
    {
        if (IsSelected) return;
        IsSelected = true;
        transform.position += new Vector3(0, 1f, 0);
        OnChipClicked();
    }

    public void RemoveSelection()
    {
        IsSelected = false;
        transform.position = _startingPosition;
    }
}