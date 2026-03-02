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
    public static Action<int, int> OnEnemiesUpdated;

    // KEY
    public static Action OnKeyCollected;
    public static Action<int, int> OnKeysUpdated;

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
    public static void RaiseEnemiesUpdated(int current, int total)
    => OnEnemiesUpdated?.Invoke(current, total);
    // Key
    public static void RaiseKeyCollected() => OnKeyCollected?.Invoke();
    public static void RaiseKeysUpdated(int current, int total)
    => OnKeysUpdated?.Invoke(current, total);
    // Level
    public static void RaiseLevelCompleted() => OnLevelCompleted?.Invoke();
    public static void RaiseLevelStopped() => OnLevelStopped?.Invoke();
    public static void RaiseLevelResumed() => OnLevelResumed?.Invoke();

    #endregion
}
