
using UnityEngine;

public class MenuInicio : UIPanel
{

    [Header("Audio")]
    [SerializeField] private AudioClip introMusic;

    private bool canContinue;

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
            // Cambia de panel
            GestorUI.Instance.MostrarPanel(PanelType.MenuPrincipal);
        }
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
