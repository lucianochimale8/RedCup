using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static Action OnEnemyKilled;
    public static Action OnPlayerDied;
    public static Action OnPlayerHit;

    public static Action OnAltarDestroyed;
    public static Action OnKeyCollected;

    public static void RaisePlayerHit() => OnPlayerHit?.Invoke();
    public static void RaisePlayerDied() => OnPlayerDied?.Invoke();
    public static void RaiseEnemyKilled() => OnEnemyKilled?.Invoke();
}
