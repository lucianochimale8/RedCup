using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : UIPanel
{
    [Header("Boton Inicio")]
    [SerializeField] private Button volverInicio;
    [Header("AudioClip")]
    [SerializeField] private AudioClip winClip;
    [Header("Volumen")]
    [SerializeField] private float volumen;
    [Header("Tiempo Final")]
    [SerializeField] private TMP_Text finalTimeText;

    #region Unity Lifecycle
    private void Awake()
    {
        if (volverInicio != null)
            volverInicio.onClick.AddListener(VolverAlMenu);
    }
    #endregion

    #region Mostrar & Ocultar
    public override void Mostrar()
    {
        gameObject.SetActive(true);

        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.StopTimer();
            finalTimeText.text = TimeManager.Instance.GetFormattedTime();
        }

        AudioManager.Instance.PlaySoundEffect(winClip, volumen);        
    }
    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region Volver Al Menu
    private void VolverAlMenu()
    {
        TimeManager.Instance.ResetTimer();

        GestorUI.PanelMenuAlCargar = PanelType.MenuPrincipal;
        SceneManager.LoadScene(GestorUI.MENU_SCENE);
    }
    #endregion
}
