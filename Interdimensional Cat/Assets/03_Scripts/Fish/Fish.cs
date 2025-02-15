using UnityEngine;

public class Fish : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        GameController.Instance.OnCatchFish();
        Destroy(gameObject);
    }
}
