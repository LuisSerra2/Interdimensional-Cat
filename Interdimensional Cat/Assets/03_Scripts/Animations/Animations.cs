using DG.Tweening;
using System.Collections;
using UnityEngine;

public enum AnimationsType
{
    Position,
    Scale
}

public class Animations : MonoBehaviour
{
    [Header("Position Animation")]
    [SerializeField] private Vector2 EndPosition;
    [SerializeField] private float animTimer = 1;
    [SerializeField] private Ease PositionEase;

    [Header("Scale Animation")]
    [SerializeField] private Vector3 EndScale = Vector3.one * 1.2f;
    [SerializeField] private Ease ScaleEase;

    [SerializeField] private AnimationsType Type;

    private Vector2 InitialPosition;
    private Vector3 InitialScale;
    private Coroutine positionCoroutine;
    private Coroutine scaleCoroutine;

    private void Start()
    {
        if (!gameObject.activeInHierarchy) return;


        switch (Type)
        {
            case AnimationsType.Position:
                InitialPosition = transform.position;
                positionCoroutine = StartCoroutine(PositionAnimation());
                break;
            case AnimationsType.Scale:
                InitialScale = transform.localScale;
                scaleCoroutine = StartCoroutine(ScaleAnimation());
                break;
        }
    }

    private IEnumerator PositionAnimation()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return transform.DOMove(InitialPosition + EndPosition, animTimer).SetEase(PositionEase).WaitForCompletion();
            yield return transform.DOMove(InitialPosition, animTimer).SetEase(PositionEase).WaitForCompletion();
        }
    }

    private IEnumerator ScaleAnimation()
    {
        while (gameObject.activeInHierarchy)
        {

            yield return transform.DOScale(EndScale, animTimer).SetEase(ScaleEase).WaitForCompletion();
            yield return transform.DOScale(InitialScale, animTimer).SetEase(ScaleEase).WaitForCompletion();

        }
    }

    private void OnDisable()
    {
        DOTween.Kill(transform);

        switch (Type)
        {
            case AnimationsType.Position:
                if (positionCoroutine != null)
                {
                    StopCoroutine(positionCoroutine);
                    positionCoroutine = null;
                }
                break;
            case AnimationsType.Scale:

                if (scaleCoroutine != null)
                {
                    StopCoroutine(scaleCoroutine);
                    scaleCoroutine = null;
                }
                break;
        }
    }

    private void OnEnable()
    {
        switch (Type)
        {
            case AnimationsType.Position:
                if (positionCoroutine == null)
                    positionCoroutine = StartCoroutine(PositionAnimation());
                break;
            case AnimationsType.Scale:
                if (scaleCoroutine == null)
                    scaleCoroutine = StartCoroutine(ScaleAnimation());
                break;
        }


    }
}
