using System;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spike;
    [SerializeField] private Sprite IceSpike;
    [SerializeField] private Sprite NetherSpike;

    private void OnEnable()
    {
        GameController.Instance.OnNether += OnNether;
        GameController.Instance.OnIce += OnIce;
    }


    private void OnDisable()
    {
        GameController.Instance.OnNether -= OnNether;
        GameController.Instance.OnIce -= OnIce;
    }

    private void OnNether()
    {
        foreach (var spike in spike)
        {
            if (spike == null) return;
            spike.sprite = NetherSpike;
        }

    }

    private void OnIce()
    {
        foreach (var spike in spike)
        {
            if (spike == null) return;
            spike.sprite = IceSpike;
        }
    }



}
