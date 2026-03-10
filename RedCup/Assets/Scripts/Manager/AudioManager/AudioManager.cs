using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource;
    [SerializeField] private TMP_Text musicText, sfxText;
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
    public void PlayMusic(AudioClip musicClip, float volume = 1f)
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.volume = musicVolumen * volume;
        musicAudioSource.Play();
    }
    #endregion

    #region Metodos Stop
    public void StopMusic()
    {
        musicAudioSource.Stop();
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
