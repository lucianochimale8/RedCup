
using UnityEngine;

public class MenuInicio : UIPanel
{
    private GestorUI gestorUI;

    private void Awake()
    {
        gestorUI = FindFirstObjectByType<GestorUI>();
    }
    private void Update()
    {
        if (!gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gestorUI.MostrarPanel(PanelType.MenuPrincipal);
        }
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
