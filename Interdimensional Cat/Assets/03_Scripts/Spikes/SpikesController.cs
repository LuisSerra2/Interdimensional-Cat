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
        if (!activated)
        {
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
                GameController.Instance.PlaySound(SoundType.SpikeFall);
            }
            yield return new WaitForSeconds(delayBetweenSpikes);
        }

        yield return new WaitForSeconds(.2f);

        foreach (GameObject spike in spikes)
        {
            BoxCollider2D col = spike.GetComponent<BoxCollider2D>();
            if (col != null)
            {
                col.isTrigger = false;
            }
            yield return new WaitForSeconds(delayBetweenSpikes);
        }

        
    }


}
