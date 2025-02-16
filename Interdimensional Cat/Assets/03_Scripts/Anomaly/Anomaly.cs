using UnityEngine;

public class Anomaly : MonoBehaviour, IInteractable
{
    [SerializeField] private float timer = 5f;

    private AnomalyData anomalyData;

    public void OnInteract()
    {       
        anomalyData = new AnomalyData(gameObject, timer);
        GameController.Instance.OnAnomalyEvent();
        GameController.Instance.OnAnomalyEvent(anomalyData);
    }

}


