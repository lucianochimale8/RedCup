using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : UIPanel
{
    [SerializeField] private Button btnReanudar;
    [SerializeField] private Button btnMenu;

    private void Awake()
    {
        gameObject.SetActive(false);

        btnReanudar.onClick.AddListener(Continuar);
        btnMenu.onClick.AddListener(SalirAlMenu);
    }

    public override void Mostrar()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Continuar()
    {
        GameEvents.OnLevelResumed();
    }
    private void SalirAlMenu()
    {
        SceneManager.LoadScene("MenuUI");
    }
}
