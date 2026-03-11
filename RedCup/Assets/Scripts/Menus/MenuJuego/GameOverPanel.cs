using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : UIPanel
{
    [Header("Boton Inicio")]
    [SerializeField] private Button volverInicio;

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
    }
    public override void Ocultar()
    { 
        gameObject.SetActive(false);
    }
    #endregion

    #region Volver Al Menu
    // Botón Volver al Menú
    public void VolverAlMenu()
    {
        GestorUI.PanelMenuAlCargar = PanelType.MenuPrincipal;
        SceneManager.LoadScene(GestorUI.MENU_SCENE);
    }
    #endregion
}
