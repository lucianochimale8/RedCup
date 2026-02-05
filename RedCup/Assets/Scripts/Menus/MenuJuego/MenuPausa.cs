using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : UIPanel
{
    [SerializeField] private Button btnReanudar;
    [SerializeField] private Button btnMenu;

    private GestorUI gestorUI;

    private void Awake()
    {
        gestorUI = FindFirstObjectByType<GestorUI>();

        btnReanudar.onClick.AddListener(() =>
        {
            gestorUI.MostrarPanel(PanelType.HUD);
        });

        btnMenu.onClick.AddListener(() =>
        {      
            SalirAlMenu();
        });
    }

    public override void Mostrar()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public override void Ocultar()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    private void SalirAlMenu()
    {
        Time.timeScale = 1f;
        MenuStartup.panelInicial = PanelType.MenuPrincipal;
        SceneManager.LoadScene("MenuUI");
    }
}
