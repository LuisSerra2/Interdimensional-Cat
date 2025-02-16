using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Vector3 scaleAnimEnd;
    public bool IsBack;

    private RectTransform m_RectTransform;

    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_RectTransform != null)
            m_RectTransform.DOScale(scaleAnimEnd, 0.1f);
        GameController.Instance.PlaySound(SoundType.ButtonsOnHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_RectTransform != null)
            m_RectTransform.DOScale(Vector3.one, 0.1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsBack)
        {
            GameController.Instance.PlaySound(SoundType.ButtonBack);
        } else
        {
            GameController.Instance.PlaySound(SoundType.ButtonsClick);
        }
    }
}
