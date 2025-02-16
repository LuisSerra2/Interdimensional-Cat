using UnityEngine;

public class Rune : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        GameController.Instance.OnPickRune();
        GameController.Instance.PlaySound(SoundType.PickUpRune);
        Destroy(gameObject);
    }
}
