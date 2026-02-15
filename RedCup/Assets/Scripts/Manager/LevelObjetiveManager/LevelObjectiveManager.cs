using UnityEngine;

public class LevelObjectiveManager : MonoBehaviour
{
    public static LevelObjectiveManager Instance { get; private set; }

    [Header("Objetivos del Nivel")]
    [SerializeField] private int enemiesRequired;
    [SerializeField] private bool requireKey = true;
    [SerializeField] private GameObject levelCompleteUI;

    private int enemiesKilled;
    private bool keyCollected;
    private bool altarDestroyed;
    private bool allWavesSpawned;

    #region Unity Lifecycle
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += RegisterEnemyDeath;
        GameEvents.OnKeyCollected += RegisterKeyCollected;
        GameEvents.OnAltarDestroyed += RegisterAltarDestroyed;
        GameEvents.OnAllWavesSpawned += RegisterAllWavesSpawned;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyKilled -= RegisterEnemyDeath;
        GameEvents.OnKeyCollected -= RegisterKeyCollected;
        GameEvents.OnAltarDestroyed -= RegisterAltarDestroyed;
        GameEvents.OnAllWavesSpawned -= RegisterAllWavesSpawned;
    }
    #endregion

    #region Register Events
    private void RegisterEnemyDeath()
    {
        enemiesKilled++;
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

    private void RegisterAllWavesSpawned()
    {
        allWavesSpawned = true;
        CheckCompletion();
    }
    #endregion

    #region Level Check & Completed
    private void CheckCompletion()
    {
        bool enemiesDone = enemiesKilled >= enemiesRequired && allWavesSpawned;
        bool keyDone = !requireKey || keyCollected;

        if (enemiesDone && keyDone)
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        Debug.Log("Nivel Completado");

        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(true);
    }
    #endregion
}
