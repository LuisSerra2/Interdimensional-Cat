using System;
using UnityEngine;

public class AnomalyController : MonoBehaviour
{

    private float timer;
    private bool anomalyTime = false;

    private AnomalyData anomalyData;

    private void OnEnable()
    {
        GameController.Instance.OnAnomaly += OnAnomaly;
        GameController.Instance.OnAnomalyData += OnAnomalyData;
        GameController.Instance.OnAnomalyFinish += OnAnomalyFinish;
    }

    private void OnDisable()
    {
        GameController.Instance.OnAnomaly -= OnAnomaly;
        GameController.Instance.OnAnomalyData -= OnAnomalyData;
        GameController.Instance.OnAnomalyFinish -= OnAnomalyFinish;
    }

    private void Update()
    {
        if (!anomalyTime) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            anomalyTime = false;
            GameController.Instance.OnAnomalyFinishEvent();
            timer = anomalyData.GetTimer();
        }
    }

    private void OnAnomaly()
    {
        
        anomalyTime = true;
    }

    private void OnAnomalyData(AnomalyData data)
    {
        anomalyData = data;
        timer = anomalyData.GetTimer();
        anomalyData.GetAnomalyGameObject().SetActive(false);
    }

    private void OnAnomalyFinish()
    {
        anomalyData.GetAnomalyGameObject().SetActive(true);
    }


}

public struct AnomalyData
{
    private GameObject anomalyObj;
    private float timer;
    public AnomalyData(GameObject gameObject, float GOtimer)
    {
        anomalyObj = gameObject;
        timer = GOtimer;
    }

    public GameObject GetAnomalyGameObject() => anomalyObj;
    public float GetTimer() => timer;

}
