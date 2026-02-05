using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuPrincipal : UIPanel
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOpciones;
    [SerializeField] private Button btnCreditos;
    [SerializeField] private Button btnSalir;
    [SerializeField] private GestorUI gestorUI;

    private void Awake()
    {
        gestorUI = FindFirstObjectByType<GestorUI>();

        btnPlay.onClick.AddListener(() => SceneManager.LoadScene("TextArea"));
        btnOpciones.onClick.AddListener(() => gestorUI.MostrarPaneles(1));
        btnCreditos.onClick.AddListener(() => gestorUI.MostrarPaneles(2));
        btnSalir.onClick.AddListener(gestorUI.Salir);
    }

    public override void Mostrar()
    {
        gameObject.SetActive(true);
        if (gestorUI == null)
        {
            Debug.Log("error: gestor incompatible");
            return;
        }
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
