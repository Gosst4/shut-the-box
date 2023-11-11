using System.Collections;
using UnityEngine;

public class BoardRotator : MonoBehaviour
{
    [SerializeField] float _rotationDuration = 2f;

    public bool IsRotating { get; private set; } = false;

    static BoardRotator instance;
    public static BoardRotator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoardRotator>();
            }
            return instance;
        }
    }

    public void RotateTo(Vector3 eulerAngles, float rotateAfter)
    {
        if (IsRotating) return;
        StartCoroutine(WaitForRotation(eulerAngles, rotateAfter));
    }

    IEnumerator WaitForRotation(Vector3 eulerAngles, float rotateAfter)
    {
        Coroutine c = StartCoroutine(RotateToCoroutine(eulerAngles, rotateAfter));
        yield return c;
    }

    IEnumerator RotateToCoroutine(Vector3 eulerAngles, float rotateAfter)
    {
        IsRotating = true;
        DiceManager.Instance.CanRollDice(false);
        yield return new WaitForSeconds(rotateAfter);
        
        float timeElapsed = 0;

        var startPosition = transform.localRotation;
        var targetPosition = Quaternion.Euler(eulerAngles * (- 1));

        while (timeElapsed < _rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startPosition, targetPosition, timeElapsed / _rotationDuration);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        IsRotating = false;
        DiceManager.Instance.CanRollDice(true);
    }
}
