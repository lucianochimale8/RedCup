using UnityEngine;
using UnityEngine.UI;

public class MenuOpciones : UIPanel
{
    [SerializeField] private Button btnVolver;

    private void Awake()
    {
        btnVolver.onClick.AddListener(() =>
        {
            GestorUI.Instance.MostrarPanel(PanelType.MenuPrincipal);
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