using UnityEngine;
using TMPro;

public class MenuHUD : UIPanel
{
    [Header("Textos")]
    [SerializeField] private TMP_Text enemiesText;
    [SerializeField] private TMP_Text keysText;
    #region Events
    private void OnEnable()
    {
        GameEvents.OnPlayerDied += Ocultar;
        GameEvents.OnEnemiesUpdated += UpdateEnemies;
        GameEvents.OnKeysUpdated += UpdateKeys;
        
        LevelObjectiveManager manager = FindFirstObjectByType<LevelObjectiveManager>();

        if (manager != null)
        {
            UpdateEnemies(manager.EnemiesKilled, manager.EnemiesRequired);
            UpdateKeys(manager.KeysCollected, manager.TotalKeysRequired);
        }
        
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerDied -= Ocultar;
        GameEvents.OnEnemiesUpdated -= UpdateEnemies;
        GameEvents.OnKeysUpdated -= UpdateKeys;
    }
    #endregion

    #region Mostrar & Ocultar
    public override void Mostrar()
    {
        gameObject.SetActive(true);
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region Updates
    // Para actualizar los enemigos restantes
    private void UpdateEnemies(int current, int total)
    {
        enemiesText.text = $": {current}/{total}";
    }
    // Para actualizar las llaves obtenidas
    private void UpdateKeys(int current, int total)
    {
        keysText.text = $": {current}/{total}";
    }
    #endregion
}
