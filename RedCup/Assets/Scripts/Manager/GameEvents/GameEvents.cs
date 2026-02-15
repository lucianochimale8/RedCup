using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    
    public static Action OnPlayerDied;
    public static Action OnPlayerHit;
    
    public static Action OnEnemyKilled;

    public static Action OnKeyCollected;
    public static Action OnAltarDestroyed;
    public static Action OnAllWavesSpawned;

    #region Raise Methods
    public static void RaisePlayerHit() => OnPlayerHit?.Invoke();
    public static void RaisePlayerDied() => OnPlayerDied?.Invoke();
    public static void RaiseEnemyKilled() => OnEnemyKilled?.Invoke();
    public static void RaiseKeyCollected() => OnKeyCollected?.Invoke();
    public static void RaiseAltarDestroyed() => OnAltarDestroyed?.Invoke();
    public static void RaiseAllWavesSpawned() => OnAllWavesSpawned?.Invoke();
    #endregion
}
