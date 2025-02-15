using UnityEngine;

public class SoundInputManager : MonoBehaviour
{
    public float musicVolume = 1f;
    public float soundVolume = 1f;
    public bool musicToggle = true;
    public bool soundToggle = true;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundVolumeKey = "SoundVolume";
    private const string MusicToggleKey = "MusicToggle";
    private const string SoundToggleKey = "SoundToggle";

    public float SetMusicVolume(float volume)
    {
        return musicVolume = volume;
    }

    public float SetSoundVolume(float volume)
    {
        return soundVolume = volume;
    }

    public bool SetMusicToggle(bool isEnabled)
    {
        return musicToggle = isEnabled;
    }

    public bool SetSoundToggle(bool isEnabled)
    {
        return soundToggle = isEnabled;
    }

    public float GetMusicVolume() => musicVolume;
    public float GetSoundVolume() => soundVolume;
    public bool GetMusicToggle() => musicToggle;
    public bool GetSoundToggle() => soundToggle;

    public void Save()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(SoundVolumeKey, soundVolume);
        PlayerPrefs.SetInt(MusicToggleKey, musicToggle ? 1 : 0);
        PlayerPrefs.SetInt(SoundToggleKey, soundToggle ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
            musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);

        if (PlayerPrefs.HasKey(SoundVolumeKey))
            soundVolume = PlayerPrefs.GetFloat(SoundVolumeKey);

        if (PlayerPrefs.HasKey(MusicToggleKey))
            musicToggle = PlayerPrefs.GetInt(MusicToggleKey) == 1;

        if (PlayerPrefs.HasKey(SoundToggleKey))
            soundToggle = PlayerPrefs.GetInt(SoundToggleKey) == 1;
    }
}
