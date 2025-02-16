using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string Fish = "Fish";

    private int fish = 0;

    public bool resetFish; 

    private void Start()
    {
        Initialize();

        Debug.Log(fish);
    }

    private void Initialize()
    {
        if (!PlayerPrefs.HasKey(Fish) || resetFish)
        {
            PlayerPrefs.SetInt(Fish, 0);
        } else
        {
            fish = PlayerPrefs.GetInt(Fish);
        }
    }

    public void OnCatchFish()
    {
        fish++;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(Fish, fish);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(Fish))
        {
            fish = PlayerPrefs.GetInt(Fish);
        }
    }
}
