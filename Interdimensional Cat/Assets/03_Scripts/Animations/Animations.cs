using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Vector2 EndPosition;
    [SerializeField] private float animTimer = 1;
    [SerializeField] private Ease Ease;

    private Vector2 InitialPosition;
    private Coroutine animationCoroutine;

    private void Start()
    {
        if (!gameObject.activeInHierarchy) return;

        InitialPosition = transform.position;
        animationCoroutine = StartCoroutine(PositionAnimation());
    }

    private IEnumerator PositionAnimation()
    {
        while (gameObject.activeInHierarchy) 
        {
            yield return transform.DOMove(InitialPosition + EndPosition, animTimer).SetEase(Ease).WaitForCompletion();
            yield return transform.DOMove(InitialPosition, animTimer).SetEase(Ease).WaitForCompletion();
        }
    }

    private void OnDisable()
    {
        DOTween.Kill(transform); 
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine); 
            animationCoroutine = null;
        }
    }

    private void OnEnable()
    {
        if (animationCoroutine == null)
            animationCoroutine = StartCoroutine(PositionAnimation());
    }
}
