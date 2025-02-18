using System;
using UnityEngine;

public class ChangeDimension : MonoBehaviour
{
    [SerializeField] private bool OnNether;

    [Header("Ice Object")]
    [SerializeField] private GameObject IceObjects;

    [Space]

    [Header("Nether Object")]
    [SerializeField] private GameObject NetherObjects;


    private void OnEnable()
    {
        GameController.Instance.OnAnomaly += OnChangeDimension;
        GameController.Instance.OnAnomalyFinish += OnChangeDimensionFinish;

    }

    private void OnDisable()
    {
        GameController.Instance.OnAnomaly -= OnChangeDimension;
        GameController.Instance.OnAnomalyFinish -= OnChangeDimensionFinish;
    }
    private void OnChangeDimension()
    {
        GameController.Instance.PlaySound(SoundType.ChangeDimension);
        if (OnNether)
        {
            NetherObjects.SetActive(false);
            IceObjects.SetActive(true);
            GameController.Instance.OnIceEvent();

        } else
        {
            IceObjects.SetActive(false);
            NetherObjects.SetActive(true);
            GameController.Instance.OnNetherEvent();
        }
    }

    private void OnChangeDimensionFinish()
    {

        if (OnNether)
        {
            GameController.Instance.OnNetherEvent();
            IceObjects.SetActive(false);
            NetherObjects.SetActive(true);

        } else
        {
            GameController.Instance.OnIceEvent();
            NetherObjects.SetActive(false);
            IceObjects.SetActive(true);
        }
    }
}
