using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : UIPanel
{
    [SerializeField] private Button volverInicio;
    public static string MENU_SCENE = "MenuUI";

    private void Awake()
    {
        if (volverInicio != null)
            volverInicio.onClick.AddListener(VolverAlMenu);
    }

    public override void Mostrar()
    {
        gameObject.SetActive(true);
    }

    public override void Ocultar()
    { 
        gameObject.SetActive(false);
    }

    // Botón Volver al Menú
    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MENU_SCENE);
    }
}
