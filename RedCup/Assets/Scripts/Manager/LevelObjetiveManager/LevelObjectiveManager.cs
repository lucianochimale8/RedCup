using UnityEngine;

public class LevelObjectiveManager : MonoBehaviour
{
    [Header("Objetivos del Nivel")]
    // Cuantos enemigos se requieren para completar el nivel
    [SerializeField] private int enemiesRequired;
    [SerializeField] private bool requireKey = true;
    [SerializeField] private bool requireAltar = true;
    // Cuantos enemigos fueron destruido
    private int enemiesKilled;
    // Si la llave fue recogida
    private bool keyCollected;
    // Si todos las waves fueron spawneadas
    private bool allWavesSpawned;
    // Si los altares fueron destruidos
    private bool altarDestroyed;
    private bool levelAlreadyCompleted;

    #region Unity Lifecycle
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
        Debug.Log("regristramos un enemigo muerto");
        enemiesKilled++;
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
        keyCollected = true;
        CheckCompletion();
    }
    private void RegisterAltarDestroyed()
    {
        Debug.Log("haz destruido un altar");
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

        levelAlreadyCompleted = true;

        GameEvents.RaiseLevelCompleted();
    }
    #endregion
}
