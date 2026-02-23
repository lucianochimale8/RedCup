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

    [SerializeField] private GestorUI gestorUI;

    private void Awake()
    {
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
        SceneManager.LoadScene(TUTORIAL_SCENE);
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