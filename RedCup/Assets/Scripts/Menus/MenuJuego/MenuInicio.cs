
using UnityEngine;

public class MenuInicio : UIPanel
{
    private GestorUI gestorUI;

    [Header("Audio")]
    [SerializeField] private AudioClip introMusic;
    [SerializeField] private AudioClip menuMusic;

    private bool canContinue;

    private void Awake()
    {
        gestorUI = FindFirstObjectByType<GestorUI>();
    }

    public override void Mostrar()
    {
        gameObject.SetActive(true);

        // Reproduce la música de intro
        AudioManager.Instance.PlayMusic(introMusic);

        canContinue = false;
        Invoke(nameof(HabilitarContinuar), 7f); // espera 10 segundos
    }

    void HabilitarContinuar()
    {
        canContinue = true;
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        if (canContinue && Input.GetKeyDown(KeyCode.Space))
        {
            // Cambia la música
            AudioManager.Instance.PlayMusic(menuMusic);

            // Cambia de panel
            gestorUI.MostrarPanel(PanelType.MenuPrincipal);
        }
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
