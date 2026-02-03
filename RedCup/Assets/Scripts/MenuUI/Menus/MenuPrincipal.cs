using UnityEngine;
using UnityEngine.UI;


public class MenuPrincipal : UIPanel
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOpciones;
    [SerializeField] private Button btnCreditos;
    [SerializeField] private Button btnSalir;
    [SerializeField] private GestorUI gestorUI;
    public override void Mostrar()
    {
        gameObject.SetActive(true);
        if (gestorUI == null)
        {
            Debug.Log("error: gestor incompatible");
            return;
        }

        btnPlay.onClick.RemoveAllListeners();
        btnPlay.onClick.AddListener(() => gestorUI.MostrarPaneles(0));

        btnOpciones.onClick.RemoveAllListeners();
        btnOpciones.onClick.AddListener(()=>gestorUI.MostrarPaneles(1));

        btnCreditos.onClick.RemoveAllListeners();
        btnCreditos.onClick.AddListener(()=>gestorUI.MostrarPaneles(2));

        btnSalir.onClick.RemoveAllListeners();
        btnSalir.onClick.AddListener(()=>gestorUI.Salir());
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
