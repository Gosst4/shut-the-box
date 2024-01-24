using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

    public void RotateTo(Player player, float rotateAfter)
    {
        if (IsRotating) return;
        StartCoroutine(RotateToCoroutine(player, rotateAfter));
    }

    public void SetBoardPosition(Vector3 eulerAngles)
    {
        if (IsRotating) return;
        transform.rotation = Quaternion.Euler(eulerAngles * (-1));
    }

    IEnumerator RotateToCoroutine(Player player, float rotateAfter)
    {
        DiceManager.Instance.CanRollDice(false);
        IsRotating = true;
        yield return new WaitForSeconds(rotateAfter);
        
        float timeElapsed = 0;

        var startPosition = transform.localRotation;
        var targetPosition = Quaternion.Euler(player.Setup.TargetEulerAngles * (- 1));

        while (timeElapsed < _rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startPosition, targetPosition, timeElapsed / _rotationDuration);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Hud.Instance.ShowPlayerName(player.Name);
        DiceManager.Instance.CanRollDice(true);
        IsRotating = false;
    }
}
