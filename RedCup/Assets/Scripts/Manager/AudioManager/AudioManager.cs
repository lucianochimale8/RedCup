using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioClip")]
    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource;
    [Header("Textos")]
    [SerializeField] private TMP_Text musicText, sfxText;
    [Header("Volumen")]
    [SerializeField] private float sfxVolumen = 1f, musicVolumen = 0.3f;
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

        musicAudioSource.volume = musicVolumen;
        sfxAudioSource.volume = sfxVolumen;
    }
    #endregion

    #region Metodos Play
    public void PlaySoundEffect(AudioClip audioClip, float volume = 1f)
    {
        sfxAudioSource.PlayOneShot(audioClip, sfxVolumen * volume);
    }
    public void PlayMusic(AudioClip musicClip, float volume = 1f, bool loop = true)
    {
        if (musicAudioSource.clip == musicClip && musicAudioSource.isPlaying)
            return;

        musicAudioSource.clip = musicClip;
        musicAudioSource.volume = musicVolumen * volume;
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
        if (musicAudioSource.mute)
        {
            musicText.text = "Music: off";
        }
        else
        {
            musicText.text = "Music: on";
        }
    }
    public void ToggleSFX()
    {
        sfxAudioSource.mute = !sfxAudioSource.mute;
        if (sfxAudioSource.mute)
        {
            sfxText.text = "SFX: off";
        }
        else
        {
            sfxText.text = "SFX: on";
        }
    }
    #endregion
}
