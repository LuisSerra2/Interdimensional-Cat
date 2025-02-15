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
            OnAnomaly(Vector4.one, true);
            timer = defaultTimer;

        }
    }

    public void OnInteract()
    {
        anomalyTime = true;
        //GameController.Instance.ChangeDimension();
        OnAnomaly(Vector4.zero, false);
    }

    private void OnAnomaly(Vector4 vector, bool active)
    {
        anomalyVisual.color = vector;
        circleCollider.enabled = active;
    }
}
