using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Header("AudioClip")]
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [Header("Volumen")]
    [SerializeField] private float sfxVolumen = 1f;
    [SerializeField] private float musicVolumen = 0.3f;
    public static Action OnAudioStateChanged;
    public static AudioManager Instance { get; private set; }

    #region Unity Lifecycle
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        musicVolumen = PlayerPrefs.GetFloat("MusicVolume", musicVolumen);
        sfxVolumen = PlayerPrefs.GetFloat("SFXVolume", sfxVolumen);

        bool musicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        bool sfxMuted = PlayerPrefs.GetInt("SFXMuted", 0) == 1;

        musicAudioSource.volume = musicVolumen;
        sfxAudioSource.volume = sfxVolumen;

        musicAudioSource.mute = musicMuted;
        sfxAudioSource.mute = sfxMuted;
    }
    #endregion

    #region Metodos Play
    public void PlaySoundEffect(AudioClip audioClip, float volume = 1f)
    {
        sfxAudioSource.PlayOneShot(audioClip, sfxVolumen * volume);
    }
    public void PlayMusic(AudioClip musicClip,float baseVolume = 1f, bool loop = true)
    {
        if (musicAudioSource.clip == musicClip && musicAudioSource.isPlaying)
            return;

        musicAudioSource.clip = musicClip;
        musicAudioSource.volume = musicVolumen * baseVolume;
        musicAudioSource.loop = loop;
        musicAudioSource.Play();
    }
    #endregion

    #region Metodos Pause & Resume
    public void PauseMusic()
    {
        if (musicAudioSource.isPlaying)
            musicAudioSource.Pause();
    }

    public void ResumeMusic()
    {
        musicAudioSource.UnPause();
    }
    #endregion

    #region Activar-Desactivar en UI
    public void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
        PlayerPrefs.SetInt("MusicMuted", musicAudioSource.mute ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void ToggleSFX()
    {
        sfxAudioSource.mute = !sfxAudioSource.mute;
        PlayerPrefs.SetInt("SFXMuted", sfxAudioSource.mute ? 1 : 0);
        PlayerPrefs.Save();
    }
    #endregion

    #region Setters
    public void SetMusicVolume(float volume)
    {
        musicVolumen = volume;
        musicAudioSource.volume = musicVolumen;

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolumen = volume;
        sfxAudioSource.volume = sfxVolumen;

        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
    #endregion

    #region Getters
    public float GetMusicVolume()
    {
        return musicVolumen;
    }
    public float GetSFXVolume()
    {
        return sfxVolumen;
    }
    #endregion

    #region Muted
    public bool IsMusicMuted()
    {
        return musicAudioSource.mute;
    }
    public bool IsSFXMuted()
    {
        return sfxAudioSource.mute;
    }
    #endregion
}
