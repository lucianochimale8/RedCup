using UnityEngine;

public class LevelObjectiveManager : MonoBehaviour
{
    // Singleton
    public static LevelObjectiveManager Instance { get; private set; }

    [Header("Objetivos del Nivel")]
    // Cuantos enemigos se requieren para completar el nivel
    [SerializeField] private int enemiesRequired;
    [SerializeField] private bool requireKey = true;
    [SerializeField] private bool requireAltar = true;
    // Texto de nivel completado
    [SerializeField] private GameObject levelCompleteUI;
    // Cuantos enemigos fueron destruido
    private int enemiesKilled;
    // Si la llave fue recogida
    private bool keyCollected;
    // Si todos las waves fueron spawneadas
    private bool allWavesSpawned;
    // Si los altares fueron destruidos
    private bool altarDestroyed;

    #region Unity Lifecycle
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += RegisterEnemyKilled;
        GameEvents.OnKeyCollected += RegisterKeyCollected;
        GameEvents.OnAllWavesSpawned += RegisterAllWavesSpawned;
        GameEvents.OnAltarDestroyed += RegisterAltarDestroyed;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyKilled -= RegisterEnemyKilled;
        GameEvents.OnKeyCollected -= RegisterKeyCollected;
        GameEvents.OnAllWavesSpawned -= RegisterAllWavesSpawned;
        GameEvents.OnAltarDestroyed -= RegisterAltarDestroyed;
    }
    #endregion

    #region Register Events
    private void RegisterEnemyKilled()
    {
        enemiesKilled++;
        CheckCompletion();
    }
    private void RegisterAllWavesSpawned()
    {
        allWavesSpawned = true;
        CheckCompletion();
    }
    private void RegisterKeyCollected()
    {
        keyCollected = true;
        CheckCompletion();
    }
    private void RegisterAltarDestroyed()
    {
        altarDestroyed = true;
        CheckCompletion();
    }
    #endregion

    #region Level Check & Completed
    /// <summary>
    /// Check que permite pasar de nivel
    /// </summary>
    private void CheckCompletion()
    {
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
        bool keyDone = !requireKey || keyCollected;
        bool altarDone = !requireAltar || altarDestroyed;

        return enemiesDone && keyDone && altarDone;
    }
    /// <summary>
    /// Metodo de nivel completado
    /// </summary>
    private void LevelCompleted()
    {
        Debug.Log("Nivel Completado");

        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(true);

        GameEvents.RaiseLevelCompleted();
    }
    #endregion
}
