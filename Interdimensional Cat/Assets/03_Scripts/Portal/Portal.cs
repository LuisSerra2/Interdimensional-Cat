using System;
using System.Collections;
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

        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 1);
        int unlockLevel = PlayerPrefs.GetInt("UnlockLeel", 1);

        if (SceneManager.GetActiveScene().buildIndex >= reachedIndex)
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockLeel", unlockLevel + 1);
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
