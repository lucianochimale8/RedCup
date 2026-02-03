using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : UIPanel
{
    [SerializeField] private Button btnReanudar;
    [SerializeField] private Button btnMenu;

    private GestorUI gestorUI;

    private void Awake()
    {
        gestorUI = FindFirstObjectByType<GestorUI>();

        btnReanudar.onClick.AddListener(() =>
        {
            gestorUI.MostrarPaneles(0); // HUD
        });

        btnMenu.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MenuUI");
        });
    }

    public override void Mostrar()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public override void Ocultar()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}