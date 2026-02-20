using UnityEngine;
using System;

public static class GameEvents
{
    // PLAYER
    public static Action OnPlayerHit;
    public static Action<int> OnLivesChanged;
    public static Action OnPlayerDied;

    // ENEMY
    public static Action OnEnemyKilled;
    public static Action OnAllWavesSpawned;

    // ALTAR
    public static Action OnAltarDestroyed;

    // KEY
    public static Action OnKeyCollected;

    // LEVEL
    public static Action OnLevelCompleted;
    public static Action OnLevelStopped;
    public static Action OnLevelResumed;


    #region Raise Methods
    // Player
    public static void RaisePlayerHit() => OnPlayerHit?.Invoke();
    public static void RaiseLivesChanged(int lives) => OnLivesChanged?.Invoke(lives);
    public static void RaisePlayerDied() => OnPlayerDied?.Invoke();
    // Enemy
    public static void RaiseEnemyKilled() => OnEnemyKilled?.Invoke();
    public static void RaiseAllWavesSpawned() => OnAllWavesSpawned?.Invoke();
    // Altar
    public static void RaiseAltarDestroyed() => OnAltarDestroyed?.Invoke();
    // Key
    public static void RaiseKeyCollected() => OnKeyCollected?.Invoke();
    // Level
    public static void RaiseLevelCompleted() => OnLevelCompleted?.Invoke();
    public static void RaiseLevelStopped() => OnLevelStopped?.Invoke();
    public static void RaiseLevelResumed() => OnLevelResumed?.Invoke();

    #endregion

    public static void ClearAll()
    {
        OnPlayerHit = null;
        OnLivesChanged = null;
        OnPlayerDied = null;
        OnEnemyKilled = null;
        OnAllWavesSpawned = null;
        OnAltarDestroyed = null;
        OnKeyCollected = null;
        OnLevelCompleted = null;
        OnLevelStopped = null;
        OnLevelResumed = null;
    }
}
