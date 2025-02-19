using System;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    private SpriteRenderer[] spikes;
    [SerializeField] private Sprite IceSpike;
    [SerializeField] private Sprite NetherSpike;

    private void Start()
    {
        spikes = GetComponentsInChildren<SpriteRenderer>();
    }

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
        foreach (var spikes in spikes)
        {
            if (spikes == null) return;
            spikes.sprite = NetherSpike;
        }

    }

    private void OnIce()
    {
        foreach (var spikes in spikes)
        {
            if (spikes == null) return;
            spikes.sprite = IceSpike;
        }
    }



}
