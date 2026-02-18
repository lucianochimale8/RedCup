using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : UIPanel
{
    [SerializeField] private Button volverInicio;

    private void Awake()
    {
        gameObject.SetActive(false);

        if (volverInicio != null)
            volverInicio.onClick.AddListener(VolverAlMenu);
        else
            Debug.LogError("Botón VolverInicio no asignado en GameOverPanel");
    }

    public override void Mostrar()
    {
        //Time.timeScale = 0f; // Pausa el juego cuando aparece
        gameObject.SetActive(true);
    }

    public override void Ocultar()
    { 
        //Time.timeScale = 1f; // Reanuda si se oculta
        gameObject.SetActive(false);
    }

    // Botón Volver al Menú
    public void VolverAlMenu()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("MenuUI");
    }
}
