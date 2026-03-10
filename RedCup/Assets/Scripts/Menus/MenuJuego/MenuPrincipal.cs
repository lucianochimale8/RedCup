using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : UIPanel
{
    [Header("Botones")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOpciones;
    [SerializeField] private Button btnCreditos;
    [SerializeField] private Button btnSalir;
    [SerializeField] private Button btnInicio;

    [Header("Nivel Tutorial")]
    public static string TUTORIAL_SCENE = "LevelTutorial";

    [Header("AudioClip")]
    [SerializeField] private AudioClip menuMusic;

    private void Awake()
    {
        btnPlay.onClick.AddListener(() =>
        {
            Jugar();
        });

        btnOpciones.onClick.AddListener(() =>
        {
            GestorUI.Instance.MostrarPanel(PanelType.MenuOpciones);
        });

        btnCreditos.onClick.AddListener(() =>
        {
            GestorUI.Instance.MostrarPanel(PanelType.MenuCreditos);
        });

        btnSalir.onClick.AddListener(() =>
        {
            GestorUI.Instance.Salir();
        });

        btnInicio.onClick.AddListener(VolverAlInicio);
    }

    private void Jugar()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame();
        }
        SceneManager.LoadScene(TUTORIAL_SCENE);
    }
    private void VolverAlInicio()
    {
        GestorUI.Instance.MostrarPanel(PanelType.MenuInicio);
    }

    public override void Mostrar()
    {
        gameObject.SetActive(true);
        // Cambia la música
        AudioManager.Instance.PlayMusic(menuMusic);
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}