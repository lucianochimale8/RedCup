using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPanel : UIPanel
{
    [Header("Boton Inicio")]
    [SerializeField] private Button volverInicio;
    [Header("AudioClip")]
    [SerializeField] private AudioClip winClip;
    [Header("Volumen")]
    [SerializeField] private float volumen;

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
        AudioManager.Instance.PlaySoundEffect(winClip, volumen);
    }
    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region Volver Al Menu
    public void VolverAlMenu()
    {
        GestorUI.PanelMenuAlCargar = PanelType.MenuPrincipal;
        SceneManager.LoadScene(GestorUI.MENU_SCENE);
    }
    #endregion
}
