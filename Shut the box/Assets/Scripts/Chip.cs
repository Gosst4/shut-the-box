using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    [SerializeField] Number _number;
    [SerializeField] GameObject _possibleMove;

    BoxCollider _boxCollider;
    public bool IsSelected { get; private set; } = false;
    public bool IsActive {  get; private set; } = true;

    public event Action OnChipClicked;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void OnMouseDown()
    {
        if (!IsSelected) HighlightSelection();
        else RemoveSelection();
        OnChipClicked();
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

    public IEnumerator Fall()
    {
        IsActive = false;
        GetComponent<Rigidbody>().useGravity = true;
        IsSelected = false;

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }

    private void HighlightSelection()
    {        
        IsSelected = true;
        transform.position += new Vector3(0, 1f, 0);
    }

    public void RemoveSelection()
    {
        IsSelected = false;
        transform.position = transform.parent.position;
    }
}