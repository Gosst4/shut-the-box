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

    public void StartRotation()
    {
        GetComponent<Animator>().SetTrigger("StartRotation");
    }

    public void RotateTo(Vector3 eulerAngles, float rotateAfter)
    {
        if (IsRotating) return;
        StartCoroutine(RotateToCoroutine(eulerAngles, rotateAfter));
    }

    public void SetBoardPosition(Vector3 eulerAngles)
    {
        if (IsRotating) return;
        transform.rotation = Quaternion.Euler(eulerAngles * (-1));
    }

    IEnumerator RotateToCoroutine(Vector3 eulerAngles, float rotateAfter)
    {
        DiceManager.Instance.CanRollDice(false);
        IsRotating = true;
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
        DiceManager.Instance.CanRollDice(true);
        IsRotating = false;
    }
}
