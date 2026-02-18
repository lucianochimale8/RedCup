using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    // PLAYER
    public static Action OnPlayerDied;
    public static Action OnPlayerHit;
    
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
    public static void RaisePlayerHit() => OnPlayerHit?.Invoke();
    public static void RaisePlayerDied() => OnPlayerDied?.Invoke();
    public static void RaiseEnemyKilled() => OnEnemyKilled?.Invoke();
    public static void RaiseAllWavesSpawned() => OnAllWavesSpawned?.Invoke();
    public static void RaiseAltarDestroyed() => OnAltarDestroyed?.Invoke();
    public static void RaiseKeyCollected() => OnKeyCollected?.Invoke();
    public static void RaiseLevelCompleted() => OnLevelCompleted?.Invoke();
    public static void LevelStopped() => OnLevelStopped?.Invoke();
    public static void LevelResumed() => OnLevelResumed?.Invoke();
    #endregion
}
