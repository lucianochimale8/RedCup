using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : UIPanel
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOpciones;
    [SerializeField] private Button btnCreditos;
    [SerializeField] private Button btnSalir;
    [SerializeField] private Button btnInicio;

    public static string TUTORIAL_SCENE = "LevelTutorial";

    private GestorUI gestorUI;

    private void Awake()
    {
        gestorUI = GestorUI.Instance;

        btnPlay.onClick.AddListener(() =>
        {
            Jugar();
        });

        btnOpciones.onClick.AddListener(() =>
        {
            gestorUI.MostrarPanel(PanelType.MenuOpciones);
        });

        btnCreditos.onClick.AddListener(() =>
        {
            gestorUI.MostrarPanel(PanelType.MenuCreditos);
        });

        btnSalir.onClick.AddListener(() =>
        {
            gestorUI.Salir();
        });

        btnInicio.onClick.AddListener(VolverAlInicio);
    }

    private void Jugar()
    {
        MenuStartup.panelInicial = PanelType.HUD;
        
        SceneManager.LoadScene(TUTORIAL_SCENE);

        GameManager.Instance.ResetGame();
    }
    private void VolverAlInicio()
    {
        gestorUI.MostrarPanel(PanelType.MenuInicio);
    }

    public override void Mostrar()
    {
        gameObject.SetActive(true);
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}