using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuHUD : UIPanel
{
    [Header("UI Toolkit Element Names")]
    [SerializeField] private string enemiesLabelName = "enemies-label";
    [SerializeField] private string keysLabelName = "keys-label";
    [SerializeField] private string timerLabelName = "timer-label";

    private Label enemiesLabel;
    private Label keysLabel;
    private Label timerLabel;

    // Método para vincular los elementos del UI Builder al script
    public override void Inicializar(VisualElement root)
    {
        base.Inicializar(root);

        if (container == null) return;

        // Buscamos los Labels dentro del contenedor del HUD
        enemiesLabel = container.Q<Label>(enemiesLabelName);
        keysLabel = container.Q<Label>(keysLabelName);
        timerLabel = container.Q<Label>(timerLabelName);
    }
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

    #region Unity Lifecycle
    private void Update()
    {
        if (TimeManager.Instance == null)
            return;

        timerLabel.text = TimeManager.Instance.GetFormattedTime();
    }
    #endregion
    #region Mostrar & Ocultar
    public override void Mostrar()
    {
        if (container != null)
        {
            container.style.display = DisplayStyle.Flex;
        }
    }

    public override void Ocultar()
    {
        if (container != null)
        {
            container.style.display = DisplayStyle.None;
        }
    }
    #endregion

    #region Updates
    // Para actualizar los enemigos restantes
    private void UpdateEnemies(int current, int total)
    {
        if (enemiesLabel != null)
        {
            enemiesLabel.text = $": {current}/{total}";
        }
    }
    // Para actualizar las llaves obtenidas
    private void UpdateKeys(int current, int total)
    {
        if (keysLabel != null)
        {
            keysLabel.text = $": {current}/{total}";
        }
    }
    #endregion
}
