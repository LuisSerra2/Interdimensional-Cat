using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance => instance;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SoundInputManager soundInputManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (soundManager == null || soundInputManager == null)
        {
            Debug.LogError("SoundManager ou SoundInputManager não foram atribuídos no Inspector!");
            return;
        }

        Initialize(soundManager, soundInputManager);
    }

    public void Initialize(SoundManager sm, SoundInputManager sim)
    {
        soundManager = sm;
        soundInputManager = sim;
        SoundsLoad();
    }

    #region Sounds
    public void PlaySound(SoundType sound) => soundManager.PlaySound(sound);
    public void PlayMusic(MusicType music) => soundManager.PlaySound(music);
    public void SetMusicVolume(float volume)
    {
        soundManager.musicAudioSource.volume = soundInputManager.SetMusicVolume(volume);
        SoundsSave();
    }
    public void SetSoundVolume(float volume)
    {
        soundManager.soundEffectsAudioSource.volume = soundInputManager.SetSoundVolume(volume);
        SoundsSave();
    }
    public void SetToggleMusic(bool isEnabled)
    {
        soundManager.musicAudioSource.mute = !soundInputManager.SetMusicToggle(isEnabled);
        SoundsSave();
    }
    public void SetToggleSound(bool isEnabled)
    {
        soundManager.soundEffectsAudioSource.mute = !soundInputManager.SetSoundToggle(isEnabled);
        SoundsSave();
    }

    public float GetMusicVolume() => soundInputManager.GetMusicVolume();
    public float GetSoundVolume() => soundInputManager.GetSoundVolume();
    public bool GetMusicToggle() => soundInputManager.GetMusicToggle();
    public bool GetSoundToggle() => soundInputManager.GetSoundToggle();

    public void SoundsSave()
    {
        soundInputManager.Save();
    }

    public void SoundsLoad()
    {
        soundInputManager.Load();
    }

    public void MusicSlider(float value)
    {
        SetMusicVolume(value);
        soundManager.musicAudioSource.volume = value;
    }

    public void SoundSlider(float value)
    {
        SetSoundVolume(value);
        soundManager.soundEffectsAudioSource.volume = value;
    }

    public void MusicToggle(bool value)
    {
        soundManager.musicAudioSource.mute = !value;
        SetToggleMusic(value);

    }

    public void SoundToggle(bool value)
    {
        soundManager.soundEffectsAudioSource.mute = !value;
        SetToggleSound(value);

    }

    #endregion

    #region Save And Load

    #endregion

}
