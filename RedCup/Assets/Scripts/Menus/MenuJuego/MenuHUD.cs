using UnityEngine;
using TMPro;

public class MenuHUD : UIPanel
{
    [SerializeField] private TMP_Text enemiesText;
    [SerializeField] private TMP_Text keysText;
    /*
    private void Awake()
    {
        gameObject.SetActive(true);
    }
    */
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
    public override void Mostrar()
    {
        gameObject.SetActive(true);
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    // Para actualizar los enemigos restantes
    private void UpdateEnemies(int current, int total)
    {
        enemiesText.text = $"Enemigos: {current}/{total}";
    }
    // Para actualizar las llaves obtenidas
    private void UpdateKeys(int current, int total)
    {
        keysText.text = $"Llaves: {current}/{total}";
    }
}
