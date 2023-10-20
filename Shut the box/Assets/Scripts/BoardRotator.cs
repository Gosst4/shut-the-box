using System.Collections;
using UnityEngine;

public class BoardRotator : MonoBehaviour
{
    [SerializeField] float _rotationDuration = 2f;

    bool isRotating = false;

    public void RotateTo(Vector3 eulerAngles)
    {
        if (isRotating) return;
        StartCoroutine(RotateToCoroutine(eulerAngles));
    }

    IEnumerator RotateToCoroutine(Vector3 eulerAngles)
    {
        isRotating = true;
        float timeElapsed = 0;

        var startPosition = transform.localRotation;
        var targetPosition = Quaternion.Euler(eulerAngles);

        while (timeElapsed < _rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startPosition, targetPosition, timeElapsed / _rotationDuration);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        isRotating = false;
    }
}
