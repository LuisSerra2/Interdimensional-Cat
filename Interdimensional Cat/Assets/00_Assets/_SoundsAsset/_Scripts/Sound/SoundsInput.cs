using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsInput : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundToggle;

    [SerializeField] private Sprite musicToggleSpriteOn;
    [SerializeField] private Sprite musicToggleSpriteOff;
    [SerializeField] private Sprite soundToggleSpriteOn;
    [SerializeField] private Sprite soundToggleSpriteOff;

    private void Start()
    {
        GameController.Instance.SoundsLoad();

        musicSlider.value = GameController.Instance.GetMusicVolume();
        soundSlider.value = GameController.Instance.GetSoundVolume();

        musicToggle.isOn = GameController.Instance.GetMusicToggle();
        soundToggle.isOn = GameController.Instance.GetSoundToggle();

        MusicToggle();
        SoundToggle();

        musicSlider.onValueChanged.AddListener(delegate { MusicSlider(); });
        soundSlider.onValueChanged.AddListener(delegate { SoundSlider(); });
        musicToggle.onValueChanged.AddListener(delegate { MusicToggle(); });
        soundToggle.onValueChanged.AddListener(delegate { SoundToggle(); });
    }

    private void MusicSlider()
    {
        GameController.Instance.MusicSlider(musicSlider.value);
    }

    private void SoundSlider()
    {
        GameController.Instance.SoundSlider(soundSlider.value);
    }

    private void MusicToggle()
    {
        GameController.Instance.MusicToggle(musicToggle.isOn);
    }

    private void SoundToggle()
    {
        GameController.Instance.SoundToggle(soundToggle.isOn);
    }

    public void ChangeMusicSprite(bool active)
    {
        musicToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = active ? musicToggleSpriteOn : musicToggleSpriteOff;
    }
    public void ChangeSoundSprite(bool active)
    {
        soundToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = active ? soundToggleSpriteOn : soundToggleSpriteOff;
    }
}
