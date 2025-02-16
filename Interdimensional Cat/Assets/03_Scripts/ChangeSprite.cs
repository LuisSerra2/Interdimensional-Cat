using System;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spike;
    [SerializeField] private Sprite IceSpike;
    [SerializeField] private Sprite NetherSpike;

    private void Start()
    {
        spike = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
       // GameController.Instance.OnChangeSprite += OnChangeSprite;
    }

    private void OnDisable()
    {
        //GameController.Instance.OnChangeSprite -= OnChangeSprite;
    }

    private void OnChangeSprite(bool active)
    {       
        if (active)
        {
            spike.sprite = NetherSpike;
        } else
        {
            spike.sprite = IceSpike;
        }
    }
}
