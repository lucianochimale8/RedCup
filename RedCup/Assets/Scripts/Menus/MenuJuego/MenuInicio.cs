
using UnityEngine;

public class MenuInicio : UIPanel
{
    [Header("AudioClip")]
    [SerializeField] private AudioClip introMusic;
    [Header("Volumen")]
    [SerializeField] private float introVolume = 0.2f;

    private bool canContinue;

    #region Unity Lifecycle
    private void Update()
    {
        if (!gameObject.activeSelf) return;

        if (canContinue && Input.GetKeyDown(KeyCode.Space))
        {
            GestorUI.Instance.MostrarPanel(PanelType.MenuPrincipal);
        }
    }
    #endregion

    #region Mostrar & Ocultar
    public override void Mostrar()
    {
        gameObject.SetActive(true);

        // Reproduce la música de intro
        AudioManager.Instance.PlayMusic(introMusic, introVolume, false);

        canContinue = false;
        Invoke(nameof(HabilitarContinuar), 7f); // espera 10 segundos
    }
    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    #endregion
    #region Habilitar continuacion
    private void HabilitarContinuar()
    {
        canContinue = true;
    }
    #endregion
}
