using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : UIPanel
{
    [SerializeField] private Button btnReanudar;
    [SerializeField] private Button btnMenu;
    [Header("Texts")]
    [SerializeField] private TMP_Text musicText;
    [SerializeField] private TMP_Text sfxText;

    #region Unity Lifecycle
    private void Awake()
    {
        btnReanudar.onClick.AddListener(Continuar);
        btnMenu.onClick.AddListener(SalirAlMenu);
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
    #region Events
    public override void Mostrar()
    {
        gameObject.SetActive(true);
        UpdateTexts();
    }
    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region Continuar
    /// <summary>
    /// Boton para Reanudar la partida
    /// </summary>
    private void Continuar()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        GestorUI.Instance.MostrarPanel(PanelType.HUD);
    }
    #endregion

    #region Salir Al Menu
    /// <summary>
    /// Para salir al Menu Inicio
    /// </summary>
    private void SalirAlMenu()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.ResetTimer();

        Time.timeScale = 1f;
        GameManager.Instance.ChangeState(GameState.Playing);
        GestorUI.PanelMenuAlCargar = PanelType.MenuPrincipal;
        SceneManager.LoadScene(GestorUI.MENU_SCENE);
    }
    #endregion

    #region Audio Buttons
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }
    private void UpdateTexts()
    {
        musicText.text = AudioManager.Instance.IsMusicMuted() ? "OFF" : "ON";
        sfxText.text = AudioManager.Instance.IsSFXMuted() ? "OFF" : "ON";
    }
    #endregion
}
