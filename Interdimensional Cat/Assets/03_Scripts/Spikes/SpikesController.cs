using System.Collections;
using UnityEngine;

public class SpikesController : MonoBehaviour, IInteractable
{
    public GameObject[] spikes; 
    public float delayBetweenSpikes = 0.2f; 
    private bool activated = false;

    private void Start()
    {
        activated = false;
    }

    public void OnInteract()
    {
        Debug.Log("Interact");
        if (!activated)
        {
            Debug.Log("FUNFA");
            activated = true;
            StartCoroutine(DropSpikes());
        }
    }


    IEnumerator DropSpikes()
    {
        foreach (GameObject spike in spikes)
        {
            Rigidbody2D rb = spike.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic; 
            }
            yield return new WaitForSeconds(delayBetweenSpikes);
        }
    }


}
