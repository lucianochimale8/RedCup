using UnityEngine;
using UnityEngine.UI;

public class MenuOpciones : UIPanel
{
    [SerializeField] private Button btnVolver;
    private GestorUI gestorUI;

    private void Awake()
    {
        gestorUI = FindFirstObjectByType<GestorUI>();

        btnVolver.onClick.AddListener(() =>
        {
            gestorUI.MostrarPanel(PanelType.MenuPrincipal);
        });
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