using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : UIPanel
{
    [SerializeField] private Button volverInicio;
    public static string MENU_SCENE = "MenuUI";

    private void Awake()
    {
        gameObject.SetActive(false);

        if (volverInicio != null)
            volverInicio.onClick.AddListener(VolverAlMenu);
    }
    private void OnEnable()
    {
        GameEvents.OnPlayerDied += Mostrar;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDied -= Mostrar;
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
        SceneManager.LoadScene(MENU_SCENE);
    }
}
