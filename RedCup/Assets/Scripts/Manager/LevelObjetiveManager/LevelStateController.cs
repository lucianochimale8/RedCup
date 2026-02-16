using UnityEngine;

public class LevelStateController : MonoBehaviour
{
    // Singleton
    public static LevelStateController Instance { get; private set; }
    // Para parar a los enemigos
    private bool isStopped;

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
        GameEvents.OnLevelStopped += StopLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelStopped -= StopLevel;
    }
    #endregion

    #region Stop & Resume Enemies
    /// <summary>
    /// Metodo para frenar a todos los enemigos
    /// </summary>
    public void StopLevel()
    {
        isStopped = true;

        GameEvents.LevelStopped();

        EnemyIA[] enemies = FindObjectsByType<EnemyIA>(FindObjectsSortMode.None);

        foreach (var enemy in enemies)
            enemy.StopMovement();
    }
    /// <summary>
    /// Funcionamiento normal
    /// </summary>
    public void ResumeLevel()
    {
        isStopped = false;

        GameEvents.LevelResumed();

        EnemyIA[] enemies = FindObjectsByType<EnemyIA>(FindObjectsSortMode.None);

        foreach (var enemy in enemies)
            enemy.ResumeMovement();
    }

    public bool IsStopped => isStopped;

    #endregion
}
