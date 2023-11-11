using System;
using System.Collections;
using UnityEngine;

public class Chip : MonoBehaviour
{
    [SerializeField] Number _number;
    [SerializeField] GameObject _possibleMove;
    [SerializeField] float _fallDuration;

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
        IsSelected = false;

        float timeElapsed = 0;

        var startPosition = transform.parent.position;
        var targetPosition = startPosition + new Vector3(0, -30f, 0);

        while (timeElapsed < _fallDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / _fallDuration);

            timeElapsed += Time.deltaTime;
            yield return null;
        }     
        
        gameObject.SetActive(false);
    }

    public void RestoreState()
    {
        gameObject.SetActive(true);
        SetPossibleMove(false);
        transform.position = transform.parent.position;
        IsActive = true;        
    }

    public void RemoveSelection()
    {
        IsSelected = false;
        transform.position = transform.parent.position;
    }

    public bool HasMatch(Chip[] chips, int total)
    {
        foreach (var chip in chips)
        {
            if (!chip.IsActive) continue;
            if (GetValue() == chip.GetValue()) continue;
            if (GetValue() + chip.GetValue() == total)            
                return true;            
        }
        return false;
    }

    public void Select()
    {
        HighlightSelection();
        OnChipClicked();
    }
    private void HighlightSelection()
    {
        IsSelected = true;
        transform.position += new Vector3(0, 1f, 0);
    }
}