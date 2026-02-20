using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private PanelType panelInicial = PanelType.MenuInicio;

    private void Start()
    {
        if (GestorUI.Instance != null)
        {
            GestorUI.Instance.MostrarPanel(panelInicial);
        }
    }
}
