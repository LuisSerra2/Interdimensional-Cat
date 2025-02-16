using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private float introDuration;
    [SerializeField] private CanvasGroup introCanvasGroup;
    [SerializeField] private Transform ScaleObjects;

    private void Start()
    {
        StartCoroutine(IntroAnim());
    }

    private IEnumerator IntroAnim()
    {
        yield return new WaitForSeconds(introDuration);

        introCanvasGroup.DOFade(0, introDuration).OnComplete(() =>
        {
            GameController.Instance.ChangeState(GameState.Playing);
        });

        ScaleObjects.DOScale(0, introDuration);

    }

}
