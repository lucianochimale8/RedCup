using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : UIPanel
{
    [SerializeField] private Button btnReanudar;
    [SerializeField] private Button btnMenu;
    [SerializeField] private GestorUI gestorUI;

    private void Awake()
    {
        btnReanudar.onClick.AddListener(Continuar);
        btnMenu.onClick.AddListener(SalirAlMenu);
    }

    public override void Mostrar()
    {
        gameObject.SetActive(true);
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Boton para Reanudar la partida
    /// </summary>
    private void Continuar()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        gestorUI.MostrarPanel(PanelType.HUD);
    }
    /// <summary>
    /// Para salir al Menu Inicio
    /// </summary>
    private void SalirAlMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ChangeState(GameState.Playing);
        SceneManager.LoadScene(GestorUI.MENU_SCENE);
    }
}
