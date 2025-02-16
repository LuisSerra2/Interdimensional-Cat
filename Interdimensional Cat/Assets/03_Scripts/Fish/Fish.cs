using UnityEngine;

public class Fish : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        GameController.Instance.OnCatchFish();
        GameController.Instance.PlaySound(SoundType.FishPickUp);
        Destroy(gameObject);
    }
}
