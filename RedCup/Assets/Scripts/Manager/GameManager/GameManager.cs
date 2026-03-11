using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // Sistema de vida
    [Header("Vidas")]
    [SerializeField] private int startingLives = 3;
    // Actualizar la vida
    private int currentLives;
    // Propiedades
    public int Lives => currentLives;
    // Estado del arma
    public bool HasWand { get; private set; }
    [Header("Start With Wand")]
    // Para definir en que nivel ya empieza con el arma
    [SerializeField] private bool startWithWand;
    public GameState CurrentState { get; private set; }

    private bool gameInitialized = false;

    #region Unity Lifecycle
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        currentLives = startingLives;

        CurrentState = GameState.None;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnEnable()
    {
        GameEvents.OnPlayerHit += HandlePlayerHit;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerHit -= HandlePlayerHit;
    }
    private void OnDestroy()
    {
        GameEvents.OnPlayerHit -= HandlePlayerHit;
    }
    private void Start()
    {
        if (!gameInitialized)
        {
            ResetGame();
            gameInitialized = true;
        }
        ChangeState(GameState.Playing);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeState(GameState.Playing);
    }
    #endregion

    #region Player Events
    /// <summary>
    /// Metodo para cuando dañan al jugador
    /// </summary>
    private void HandlePlayerHit()
    {
        currentLives--;

        Debug.Log("Jugador se quito una vida: " + currentLives);

        GameEvents.RaiseLivesChanged(currentLives);

        GameEvents.RaiseLevelStopped();

        //ChangeState(GameState.Paused);

        if (currentLives <= 0)
        {
            currentLives = 0;
            StartCoroutine(GameOverRoutine());
            return;
        }

        StartCoroutine(ResetRoundRoutine());
    }
    #endregion

    #region Restart
    private IEnumerator ResetRoundRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// Resetear las estadisticas por nivel
    /// </summary>
    public void ResetGame()
    {
        currentLives = startingLives;

        GameEvents.RaiseLivesChanged(currentLives);
    }
    #endregion

    #region Game Over
    private IEnumerator GameOverRoutine()
    {
        ChangeState(GameState.GameOver);
        GameEvents.RaisePlayerDied();
        yield return new WaitForSecondsRealtime(1.2f);
    }
    #endregion

    #region Wand
    public void SetWand(bool value)
    {
        HasWand = value;
        GameEvents.RaiseWandStateChanged(value);
    }
    #endregion

    #region Change State
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                GameEvents.RaiseLevelResumed();
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                GameEvents.RaiseLevelStopped();
                break;

            case GameState.GameOver:
                AudioManager.Instance.PauseMusic();
                break;
        }
    }
    #endregion
}
