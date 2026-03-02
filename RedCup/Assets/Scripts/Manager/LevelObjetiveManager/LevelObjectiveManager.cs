using UnityEngine;

public class LevelObjectiveManager : MonoBehaviour
{
    [Header("Objetivos del Nivel")]
    // Cuantos enemigos se requieren para completar el nivel
    private int enemiesRequired;
    // Cuantos enemigos fueron destruido
    private int enemiesKilled;
    // total de Llaves requieridas
    private int totalKeysRequired;
    private int keysCollected;
    // Si todos las waves fueron spawneadas
    private bool allWavesSpawned;
    private bool levelAlreadyCompleted;

    public int EnemiesKilled => enemiesKilled;
    public int EnemiesRequired => enemiesRequired;

    public int KeysCollected => keysCollected;
    public int TotalKeysRequired => totalKeysRequired;

    #region Unity Lifecycle
    private void Awake()
    {
        // Llaves
        AltarHealth[] altars = FindObjectsByType<AltarHealth>(FindObjectsSortMode.None);
        totalKeysRequired = altars.Length;

        // Enemigos
        Spawner[] spawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None);

        enemiesRequired = 0;

        foreach (var spawner in spawners)
        {
            enemiesRequired += spawner.TotalEnemiesToSpawn;
        }
    }
    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += RegisterEnemyKilled;
        GameEvents.OnKeyCollected += RegisterKeyCollected;
        GameEvents.OnAllWavesSpawned += RegisterAllWavesSpawned;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyKilled -= RegisterEnemyKilled;
        GameEvents.OnKeyCollected -= RegisterKeyCollected;
        GameEvents.OnAllWavesSpawned -= RegisterAllWavesSpawned;
    }
    private void Start()
    {
        Debug.Log("Enemigos requeridos: " + enemiesRequired);
        Debug.Log("Llaves requeridas: " + totalKeysRequired);
        // iniciar UI
        GameEvents.RaiseEnemiesUpdated(enemiesKilled, enemiesRequired);
        GameEvents.RaiseKeysUpdated(keysCollected, totalKeysRequired);
    }
    #endregion

    #region Register Events
    private void RegisterEnemyKilled()
    {
        Debug.Log("regristramos un enemigo muerto");
        enemiesKilled++;
        GameEvents.RaiseEnemiesUpdated(enemiesKilled, enemiesRequired);
        CheckCompletion();
    }
    private void RegisterAllWavesSpawned()
    {
        Debug.Log("Registramos all wave spawned");
        allWavesSpawned = true;
        CheckCompletion();
    }
    private void RegisterKeyCollected()
    {
        Debug.Log("haz obtenido una llave");
        keysCollected++;
        Debug.Log("LLaves obtenidas: " + keysCollected);
        GameEvents.RaiseKeysUpdated(keysCollected, totalKeysRequired);
        CheckCompletion();
    }
    #endregion

    #region Level Check & Completed
    /// <summary>
    /// Check que permite pasar de nivel
    /// </summary>
    private void CheckCompletion()
    {
        if (levelAlreadyCompleted)
            return;

        if (CanExitLevel())
        {
            LevelCompleted();
        }
    }
    /// <summary>
    /// Comprobar si se puede completar el nivel
    /// </summary>
    public bool CanExitLevel()
    {
        bool enemiesDone = enemiesKilled >= enemiesRequired && allWavesSpawned;
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
    #endregion
}
