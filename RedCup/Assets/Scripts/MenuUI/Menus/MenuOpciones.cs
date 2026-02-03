using UnityEngine;
using UnityEngine.UI;

public class MenuOpciones : UIPanel
{
    [SerializeField] private Button btnVolver; 
    [SerializeField] private GestorUI gestorUI;

    private void Awake()
    {
        gestorUI = FindFirstObjectByType<GestorUI>();

        btnVolver.onClick.AddListener(() => gestorUI.MostrarPaneles(0));
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
