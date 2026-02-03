using UnityEngine;
using UnityEngine.UI;

public class MenuCreditos : UIPanel
{
    [SerializeField] private Button btnVolver;
    [SerializeField] private GestorUI gestorUI;
    public override void Mostrar()
    {
        gameObject.SetActive(true);
        if (gestorUI == null)
        {
            Debug.Log("error: gestor incompatible");
            return;
        }

        btnVolver.onClick.RemoveAllListeners();
        btnVolver.onClick.AddListener(() => gestorUI.MostrarPaneles(2));
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
