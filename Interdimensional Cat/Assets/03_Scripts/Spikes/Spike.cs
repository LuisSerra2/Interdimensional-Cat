using UnityEngine;

public class Spike : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            Debug.Log("Hit");
            playerController.TakeHit(1);
        } else
        {
            Destroy(gameObject);
        }
    }
}
