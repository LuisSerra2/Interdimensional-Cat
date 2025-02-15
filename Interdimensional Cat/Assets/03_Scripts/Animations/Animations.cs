using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Vector2 EndPosition;
    [SerializeField] private float animTimer = 1;
    [SerializeField] private Ease Ease;

    private Vector2 InitialPosition;

    private void Start()
    {
        if (!gameObject.activeInHierarchy) return;

        InitialPosition = transform.position;
        StartCoroutine(PositionAnimation());
    }

    private IEnumerator PositionAnimation()
    {
        transform.DOMove(InitialPosition + EndPosition, animTimer).OnComplete(() =>
        {
            transform.DOMove(InitialPosition, animTimer).SetEase(Ease).OnComplete(() =>
            {
                StartCoroutine(PositionAnimation());
            });
        });

        yield return null;
    }

}
