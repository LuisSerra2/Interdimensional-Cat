using UnityEngine;

public class Anomaly : MonoBehaviour, IInteractable
{
    public float defaultTimer = 5;
    private float timer;

    private bool anomalyTime = false;

    private CircleCollider2D circleCollider;
    private SpriteRenderer anomalyVisual;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        anomalyVisual = GetComponentInChildren<SpriteRenderer>();

        timer = defaultTimer;
    }

    private void Update()
    {
        if (!anomalyTime) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            anomalyTime = false;
            GameController.Instance.OnAnomalyFinishEvent();
            OnAnomaly(1, true);
            timer = defaultTimer;

        }
    }

    public void OnInteract()
    {
        anomalyTime = true;
        GameController.Instance.OnAnomalyEvent();
        OnAnomaly(0, false);
    }

    private void OnAnomaly(float alpha, bool active)
    {
        Color color = anomalyVisual.color;
        color.a = alpha;
        anomalyVisual.color = color; 
        circleCollider.enabled = active;
    }

}
