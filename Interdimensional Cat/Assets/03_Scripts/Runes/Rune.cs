using UnityEngine;

public class Rune : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        GameController.Instance.OnPickRune();
        Destroy(gameObject);
    }
}
