using System;
using UnityEngine;

public class ChangeDimension : MonoBehaviour
{

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
        IceObjects.SetActive(false);
        NetherObjects.SetActive(true);
    }

    private void OnChangeDimensionFinish()
    {
        NetherObjects.SetActive(false);
        IceObjects.SetActive(true);
    }
}
