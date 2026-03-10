using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPanel : UIPanel
{
    [SerializeField] private Button volverInicio;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private float volumen;

    private void Awake()
    {
        if (volverInicio != null)
            volverInicio.onClick.AddListener(VolverAlMenu);
    }
    public override void Mostrar()
    {
        gameObject.SetActive(true);
        AudioManager.Instance.PlaySoundEffect(winClip, volumen);
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    public void VolverAlMenu()
    {
        GestorUI.PanelMenuAlCargar = PanelType.MenuPrincipal;
        SceneManager.LoadScene(GestorUI.MENU_SCENE);
    }
}
