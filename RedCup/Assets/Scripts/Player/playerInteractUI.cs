using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    /// <summary>
    /// Referencia al icono de interracion
    /// </summary>
    [SerializeField] private GameObject icon;
    private void Awake()
    {
        icon.SetActive(false);
    }
    /// <summary>
    /// Para mostrar
    /// </summary>
    public void Show()
    {
        icon.SetActive(true);
    }
    /// <summary>
    /// Para ocultar
    /// </summary>
    public void Hide()
    {
        icon.SetActive(false);
    }
}
