using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameState gameState;

    private static GameController instance;
    public static GameController Instance => instance;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SoundInputManager soundInputManager;
    [SerializeField] private ScoreManager scoreManager;

    #region Anomaly
    [HideInInspector]
    public Action OnAnomaly;
    [HideInInspector]
    public Action<AnomalyData> OnAnomalyData;
    [HideInInspector]
    public Action OnAnomalyFinish;
    #endregion

    #region Dimensions
    [HideInInspector]
    public Action OnNether;
    [HideInInspector]
    public Action OnIce;
    #endregion
    

    #region portal
    [HideInInspector]
    public Action OnPortalUnlock;
    #endregion

    #region Runes
    [HideInInspector]
    public Action OnPickRune;
    #endregion

    #region Menu
    [HideInInspector]
    public Action OnMenu;
    #endregion

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
        if (soundManager == null || soundInputManager == null || scoreManager == null)
        {
            Debug.LogError("SoundManager ou SoundInputManager não foram atribuídos no Inspector!");
            return;
        }

        Initialize(soundManager, soundInputManager, scoreManager);
    }

    public void Initialize(SoundManager sm, SoundInputManager sim, ScoreManager scoreM)
    {
        soundManager = sm;
        soundInputManager = sim;
        scoreManager = scoreM;
        SoundsLoad();
        LoadFish();
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

    #region ScoreManager

    public void OnCatchFish()
    {
        scoreManager.OnCatchFish();
    }

    public float GetFish() => scoreManager.GetFish();


    public void SaveFish()
    {
        scoreManager.Save();
    }

    public void LoadFish()
    {
        scoreManager.Load();
    }

    #endregion

    #region Observer

    #region Anomaly
    public void OnAnomalyEvent()
    {
        OnAnomaly?.Invoke();
    }
    public void OnAnomalyEvent(AnomalyData anomalyData)
    {
        OnAnomalyData?.Invoke(anomalyData); 
    }

    public void OnAnomalyFinishEvent()
    {
        OnAnomalyFinish?.Invoke();
    }
    #endregion

    #region Runes
    public void OnPickRuneEvent()
    {
        OnPickRune?.Invoke();
    }

    #endregion

    #region Portal
    public void OnPortalUnlockEvent()
    {
        OnPortalUnlock?.Invoke();
    }

    #endregion

    #region Menu
    public void OnMenuEvent()
    {
        OnMenu?.Invoke();
    }

    #endregion
    
    #region Dimension
    public void OnNetherEvent()
    {
        OnNether?.Invoke();
    }
    public void OnIceEvent()
    {
        OnIce?.Invoke();
    }

    #endregion

    #endregion

    #region States
    public void ChangeState(GameState state)
    {
        gameState = state;
    }

    public GameState GameState { get { return gameState; } }
    #endregion

    private void OnApplicationQuit()
    {
        SoundsSave();
        SaveFish();
    }

}
