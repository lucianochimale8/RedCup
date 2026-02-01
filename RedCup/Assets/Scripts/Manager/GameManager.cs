using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Vidas")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private TMP_Text livesText;
    private int lives;

    private int enemiesLeft;
    private bool allWavesSpawned;

    public int Lives => lives;
    public bool IsPlayerDead { get; private set; }

    public static GameManager Instance { get; private set; }

    #region Unity Lifecycle

    private void Awake()
    {
        Debug.Log("GameManager AWAKE");

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        lives = startingLives;
    }

    private void OnEnable()
    {
        Debug.Log("GameManager OnEnable - suscribiendo eventos");

        GameEvents.OnPlayerHit += OnPlayerHit;
        GameEvents.OnPlayerDied += OnPlayerDied;
        GameEvents.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerHit -= OnPlayerHit;
        GameEvents.OnPlayerDied -= OnPlayerDied;
        GameEvents.OnEnemyKilled -= OnEnemyKilled;
    }

    private void Start()
    {
        ResetWaves();
        UpdateLivesText();
    }

    #endregion

    #region Player Events
    private void StopPlayer()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
            player.StopPlayer();
    }
    private void OnPlayerHit()
    {
        Debug.Log("EVENTO: Player Hit recibido");

        lives--;
        UpdateLivesText();

        StopEnemiesMovement();
        StopEnemiesSpawn();
        StopPlayer();

        StartCoroutine(WaitAndRestart(0.6f));
    }

    private void OnPlayerDied()
    {

        Debug.Log("EVENTO: Player Died recibido");

        if (IsPlayerDead) return;

        IsPlayerDead = true;

        lives--;
        UpdateLivesText();

        StopEnemiesMovement();
        StopEnemiesSpawn();
        StopPlayer();

        StartCoroutine(WaitAndRestart(0.8f));
    }

    #endregion

    #region Enemy Events

    private void OnEnemyKilled()
    {
        Debug.Log("EVENTO: Enemy Killed recibido");

        enemiesLeft--;

        if (enemiesLeft <= 0 && allWavesSpawned)
        {
            LoadNextScene();
        }
    }

    public void IncreaseEnemiesLeft()
    {
        enemiesLeft++;
    }

    public void SetAllWavesSpawned()
    {
        allWavesSpawned = true;
    }

    #endregion

    #region Waves & Restart

    private void ResetWaves()
    {
        enemiesLeft = 0;
        allWavesSpawned = false;
        IsPlayerDead = false;
    }

    private IEnumerator WaitAndRestart(float delay)
    {
        yield return new WaitForSeconds(delay);

        ResetWaves();

        if (lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            lives = startingLives;
            ResetGame();
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);

        if (AudioManager.Instance != null)
            Destroy(AudioManager.Instance.gameObject);
    }

    #endregion

    #region Stop Enemies

    private void StopEnemiesSpawn()
    {
        Spawner spawner = FindFirstObjectByType<Spawner>();
        if (spawner != null)
            spawner.StopAllCoroutines();
    }

    private void StopEnemiesMovement()
    {
        EnemyIA[] enemies = FindObjectsByType<EnemyIA>(FindObjectsSortMode.None);

        foreach (EnemyIA enemy in enemies)
        {
            enemy.StopMovement();
        }
    }

    #endregion

    #region UI

    private void UpdateLivesText()
    {
        if (livesText != null)
            livesText.text = $"Lives: {lives}";
    }

    #endregion

    #region Scene Management

    private void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }

    #endregion
}
