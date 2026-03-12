using UnityEngine;

public class LevelObjectiveManager : MonoBehaviour
{
    [Header("Objetivos del Nivel")]
    // Cuantos enemigos se requieren para completar el nivel
    [SerializeField] private int enemiesRequired;
    private int enemiesKilled;
    // total de Llaves requieridas
    [SerializeField] private int totalKeysRequired;
    private int keysCollected;
    // Si el nivel se ha completado
    [SerializeField] private bool levelAlreadyCompleted;

    public int EnemiesKilled => enemiesKilled;
    public int EnemiesRequired => enemiesRequired;

    public int KeysCollected => keysCollected;
    public int TotalKeysRequired => totalKeysRequired;

    #region Unity Lifecycle
    private void Awake()
    {
        enemiesKilled = 0;
        keysCollected = 0;

        Spawner[] spawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None);

        foreach (var spawner in spawners)
        {
            enemiesRequired += spawner.TotalEnemiesToSpawn;
        }

        AltarHealth[] altars = FindObjectsByType<AltarHealth>(FindObjectsSortMode.None);
        totalKeysRequired = altars.Length;
    }
    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += RegisterEnemyKilled;
        GameEvents.OnKeyCollected += RegisterKeyCollected;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyKilled -= RegisterEnemyKilled;
        GameEvents.OnKeyCollected -= RegisterKeyCollected;
    }
    private void Start()
    {
        Debug.Log("Enemigos requeridos: " + enemiesRequired);
        Debug.Log("Llaves requeridas: " + totalKeysRequired);
    }
    #endregion

    #region Register Events
    private void RegisterEnemyKilled()
    {
        enemiesKilled++;
        GameEvents.RaiseEnemiesUpdated(enemiesKilled, enemiesRequired);
        CheckCompletion();
    }
    private void RegisterKeyCollected()
    {
        keysCollected++;
        GameEvents.RaiseKeysUpdated(keysCollected, totalKeysRequired);
        CheckCompletion();
    }
    public void RegisterKeyRequirement()
    {
        totalKeysRequired++;
        Debug.Log("Spawners activados: " + totalKeysRequired);
    }
    #endregion

    #region Level Check & Completed
    /// <summary>
    /// Comprobar si se puede completar el nivel
    /// </summary>
    public bool CanExitLevel()
    {
        bool enemiesDone = enemiesKilled >= enemiesRequired;
        bool keyDone = keysCollected >= totalKeysRequired;

        return enemiesDone && keyDone;
    }
    /// <summary>
    /// Metodo de nivel completado
    /// </summary>
    private void LevelCompleted()
    {
        Debug.Log("Nivel Completado");
        levelAlreadyCompleted = true;
        GameEvents.RaiseLevelCompleted();
    }
    /// <summary>
    /// Check que permite pasar de nivel
    /// </summary>
    private void CheckCompletion()
    {
        if (levelAlreadyCompleted)
            return;

        if (CanExitLevel())
            LevelCompleted();
    }
    #endregion
}
