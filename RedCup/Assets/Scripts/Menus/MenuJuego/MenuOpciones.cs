using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuOpciones : UIPanel
{
    [SerializeField] private Button btnVolver;
    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [Header("Textos")]
    [SerializeField] private TMP_Text musicText;
    [SerializeField] private TMP_Text sfxText;

    #region Unity Lifecycle
    private void Awake()
    {
        btnVolver.onClick.AddListener(() =>
        {
            GestorUI.Instance.MostrarPanel(PanelType.MenuPrincipal);
        });

        musicSlider.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.SetMusicVolume(value);
        });

        sfxSlider.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.SetSFXVolume(value);
        });
    }
    private void OnEnable()
    {
        AudioManager.OnAudioStateChanged += UpdateTexts;
    }

    private void OnDisable()
    {
        AudioManager.OnAudioStateChanged -= UpdateTexts;
    }
    #endregion

    #region Mostrar & Ocultar
    public override void Mostrar()
    {
        gameObject.SetActive(true);

        musicSlider.SetValueWithoutNotify(AudioManager.Instance.GetMusicVolume());
        sfxSlider.SetValueWithoutNotify(AudioManager.Instance.GetSFXVolume());

        UpdateTexts();
    }
    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region Updates
    private void UpdateTexts()
    {
        musicText.text = AudioManager.Instance.IsMusicMuted() ? "OFF" : "ON";
        sfxText.text = AudioManager.Instance.IsSFXMuted() ? "OFF" : "ON";
    }
    #endregion

    #region Toggles
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        UpdateTexts();
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
        UpdateTexts();
    }
    #endregion
}