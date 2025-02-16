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
        if (OnNether)
        {
            NetherObjects.SetActive(false);
            IceObjects.SetActive(true);
        } else
        {
            IceObjects.SetActive(false);
            NetherObjects.SetActive(true);
        }
    }

    private void OnChangeDimensionFinish()
    {
        if (OnNether)
        {
            IceObjects.SetActive(false);
            NetherObjects.SetActive(true);
        } else
        {
            NetherObjects.SetActive(false);
            IceObjects.SetActive(true);
        }
    }
}
