using System;
using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] LayerMask _mask;
    [SerializeField] float _rollForce = 10f;
    [SerializeField] float _torque = 10f;
    [SerializeField] float _startHeight = 2f;

    Rigidbody _rb;
    bool _isRolling = false;

    public event Action<int> OnRollFinished;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
    }

    private void RollDice()
    {
        StopAllCoroutines();
        _isRolling = true;

        transform.position = new Vector3(0, _startHeight, 0);
        transform.rotation = Quaternion.identity;
        _rb.AddForce(transform.up * 10, ForceMode.Impulse);
        _rb.AddTorque(GetRandFl(_torque), GetRandFl(_torque), GetRandFl(_torque), ForceMode.Impulse);
        StartCoroutine(WaitForDiceToStop());
    }

    private float GetRandFl(float max)
    {
        return UnityEngine.Random.Range(0, max);
    }

    private IEnumerator WaitForDiceToStop() 
    {
        while (!_rb.IsSleeping())
        {
            yield return null;
        }
        yield return null;
        _isRolling = false;
        int sideValue = GetSideValue();
        Debug.Log(sideValue);
        OnRollFinished?.Invoke(sideValue);
    }

    private int GetSideValue()
    {
        Vector3[] directions =
        {
            - transform.forward,
            - transform.up,
            transform.right,
            - transform.right,
            transform.up,
            transform.forward
        };

        for (int i = 0; i < directions.Length; i++)
        {
            if (!Physics.Raycast(transform.position, directions[i], 1f, _mask)) continue;
            return i + 1;
        }
        Debug.Log("Error getting die value.");
        return 0;
    }
}
