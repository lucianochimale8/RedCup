using UnityEngine;

public class LevelStateController : MonoBehaviour
{
    public static LevelStateController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StopLevel()
    {
        StopEnemiesMovement();
        StopSpawners();
        StopPlayer();
    }

    private void StopEnemiesMovement()
    {
        EnemyIA[] enemies = FindObjectsByType<EnemyIA>(FindObjectsSortMode.None);

        foreach (EnemyIA enemy in enemies)
        {
            enemy.StopMovement();
        }
    }

    private void StopSpawners()
    {
        Spawner[] spawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None);

        foreach (Spawner spawner in spawners)
        {
            spawner.StopAllCoroutines();
        }
    }

    private void StopPlayer()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();

        if (player != null)
            player.StopPlayer();
    }
}
