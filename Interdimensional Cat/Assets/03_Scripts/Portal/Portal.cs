using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    private bool canEnterPortal;

    private void OnEnable()
    {
        GameController.Instance.OnPortalUnlock += CanEnterPortal;
    }

    private void OnDisable()
    {
        GameController.Instance.OnPortalUnlock -= CanEnterPortal;
    }
    public void OnInteract()
    {
        if (!canEnterPortal) return;
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockLeel", PlayerPrefs.GetInt("UnlockLeel", 1) + 1);
            PlayerPrefs.Save();

            GameController.Instance.SaveFish();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void CanEnterPortal()
    {
        canEnterPortal = true;
    }
}
