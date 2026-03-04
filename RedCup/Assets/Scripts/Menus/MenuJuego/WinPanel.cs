using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPanel : UIPanel
{
    [SerializeField] private Button volverInicio;

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
    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        //GameManager.Instance.ChangeState(GameState.Playing);
        SceneManager.LoadScene(GestorUI.MENU_SCENE);
    }
}
