using System;
using UnityEngine;
using UnityEngine.UI;

public enum SoundController
{
    Music,
    Sound
}

public enum SoundType
{
    ButtonsOnHover,
    ButtonsClick,
}

public enum MusicType
{
    MainMenu,
}

[ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    public SoundsData[] soundsDatas;
    public AudioSource musicAudioSource;
    public AudioSource soundEffectsAudioSource;

    public void PlaySound(System.Enum sound)
    {
        AudioClip clip = null;
        SoundController soundController;

        switch (sound)
        {
            case SoundType soundType:
                clip = soundsDatas[(int)soundType].Sounds;
                soundController = soundsDatas[(int)soundType].SoundController;
                break;

            case MusicType musicType:
                clip = soundsDatas[(int)musicType + System.Enum.GetValues(typeof(SoundType)).Length].Sounds;
                soundController = soundsDatas[(int)musicType + System.Enum.GetValues(typeof(SoundType)).Length].SoundController;
                break;

            default:
                Debug.LogWarning("Unrecognized sound type: " + sound.ToString());
                return;
        }

        if (clip == null)
        {
            Debug.LogWarning("AudioClip is missing for " + sound.ToString());
            return;
        }

        if (soundController == SoundController.Music)
        {
            musicAudioSource.clip = clip;
            musicAudioSource.volume = GameController.Instance.GetMusicVolume();
            musicAudioSource.Play();
        } else if (soundController == SoundController.Sound)
        {
            soundEffectsAudioSource.PlayOneShot(clip, GameController.Instance.GetSoundVolume());
        }
    }

    public AudioSource GetMusicAudioSource() => musicAudioSource;
    public AudioSource GetSoundAudioSource() => soundEffectsAudioSource;

#if UNITY_EDITOR
    //private void OnEnable()
    //{
    //    InitializeSoundsDatas();
    //}

    //private void InitializeSoundsDatas()
    //{
    //    int totalSounds = Enum.GetValues(typeof(SoundType)).Length + Enum.GetValues(typeof(MusicType)).Length;
    //    soundsDatas = new SoundsData[totalSounds];

    //    int index = 0;

    //    int soundTypeCount = Enum.GetValues(typeof(SoundType)).Length;
    //    for (int i = 0; i < soundTypeCount; i++)
    //    {
    //        SoundType soundType = (SoundType)i;
    //        soundsDatas[index] = new SoundsData
    //        {
    //            Name = soundType.ToString(),
    //            SoundController = SoundController.Sound,
    //        };
    //        index++;
    //    }

    //    int musicTypeCount = Enum.GetValues(typeof(MusicType)).Length;
    //    for (int i = 0; i < musicTypeCount; i++)
    //    {
    //        MusicType musicType = (MusicType)i;
    //        soundsDatas[index] = new SoundsData
    //        {
    //            Name = musicType.ToString(),
    //            SoundController = SoundController.Music,
    //        };
    //        index++;
    //    }
    //}
#endif
}

[System.Serializable]
public struct SoundsData
{
    public string Name;
    public SoundController SoundController;
    public AudioClip Sounds;
}
