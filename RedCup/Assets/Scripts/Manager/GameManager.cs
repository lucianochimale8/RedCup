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

    [Header("Niveles")]
    [SerializeField] private string escenaFinal = "Win";

    [Header("Enemigos restantes y Waves")]
    private int enemiesLeft;
    private bool allWavesSpawned;

    [Header("Collect Key y Altar destruido")]
    private bool altarDestroyed;
    private bool keyCollected;

    [Header("UI Nivel")]
    [SerializeField] private GameObject levelCompleteText;

    public int Lives => lives;
    public bool AllEnemiesDead => altarDestroyed && keyCollected && enemiesLeft <= 0 && allWavesSpawned;
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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnEnable()
    {
        Debug.Log("GameManager OnEnable - suscribiendo eventos");

        GameEvents.OnPlayerHit += OnPlayerHit;
        GameEvents.OnPlayerDied += OnPlayerDied;
        GameEvents.OnEnemyKilled += OnEnemyKilled;
        GameEvents.OnKeyCollected += OnKeyColleted;

    }

    private void OnDisable()
    {
        GameEvents.OnPlayerHit -= OnPlayerHit;
        GameEvents.OnPlayerDied -= OnPlayerDied;
        GameEvents.OnEnemyKilled -= OnEnemyKilled;
        GameEvents.OnKeyCollected -= OnKeyColleted;
    }

    private void Start()
    {
        ResetWaves();
        UpdateLivesText();
    }

    #endregion

    #region Scene Loaded

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TMP_Text[] texts = FindObjectsByType<TMP_Text>(FindObjectsSortMode.None);

        foreach (TMP_Text txt in texts)
        {
            if (txt.name == "LivesText")
            {
                livesText = txt;
                break;
            }
        }

        ResetLevelState();
        UpdateLivesText();
    }

    #endregion

    #region Level State

    public void TryExitLevel()
    {
        Debug.Log("Intentando salir del nivel...");

        if (AllEnemiesDead)
        {
            Debug.Log("Nivel completado!");
            LoadNextLevel();
        }
        else
        {
            Debug.Log("No podés salir todavía. faltan:" + enemiesLeft);
        }
    }

    private void CheckIfLevelCanBeCompleted()
    {
        if (keyCollected && enemiesLeft <= 0 && allWavesSpawned)
        {
            if (levelCompleteText != null)
                levelCompleteText.SetActive(true);
        }
    }

    public void OnAltarDestroyed()
    {
        Debug.Log("Altar destruido! - enemigos cercanos eliminados");

        StopEnemiesSpawn();

        enemiesLeft = 0;

        altarDestroyed = true;
        allWavesSpawned = true;
    }

    private void OnKeyColleted()
    {
        Debug.Log("Llave recolecatada!");
        keyCollected = true;
        CheckIfLevelCanBeCompleted();
    }

    private void ResetLevelState()
    {
        Debug.Log("Se reseteo el level state");

        altarDestroyed = false;
        keyCollected = false;

        if (levelCompleteText != null)
            levelCompleteText.SetActive(false);
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

        CheckIfLevelCanBeCompleted();
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
            ResetWaves();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(GameOverRoutine());
        }
    }
    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(1f);

        lives = startingLives;

        IsPlayerDead = false;

        ResetWaves();
        ResetLevelState();

        SceneManager.LoadScene(0); // Menu principal
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

    #region Level Progression

    private void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            SceneManager.LoadScene(escenaFinal);
        }
    }

    #endregion
}
