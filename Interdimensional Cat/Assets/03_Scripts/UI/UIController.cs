using DG.Tweening;
using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform Menu;
    [SerializeField] private float duration = .5f;

    private bool IsActive;

    private void Start()
    {
        IsActive = false;
    }

    private void OnEnable()
    {
        GameController.Instance.OnMenu += OnMenu;
    }


    private void OnDisable()
    {
        GameController.Instance.OnMenu -= OnMenu;
    }
    private void OnMenu()
    {
        IsActive = !IsActive;

        if (IsActive)
        {
            MenuAnimation(Vector3.one, duration);
        } else
        {
            MenuAnimation(Vector3.zero, duration);
        }
    }

    private void MenuAnimation(Vector3 value, float duration)
    {
        Menu.DOScale(value, duration);
    }
}
