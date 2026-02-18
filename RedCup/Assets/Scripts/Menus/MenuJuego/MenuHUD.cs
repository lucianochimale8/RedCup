using UnityEngine;

public class MenuHUD : UIPanel
{
    private void Awake()
    {
        gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        GameEvents.OnPlayerDied += Ocultar;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerDied -= Ocultar;
    }
    public override void Mostrar()
    {
        gameObject.SetActive(true);
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
