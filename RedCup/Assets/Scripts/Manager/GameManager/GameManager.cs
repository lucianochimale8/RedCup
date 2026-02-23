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
    // Para definir en que nivel ya empieza con el arma
    [SerializeField] private bool startWithWand;
    public GameState CurrentState { get; private set; }

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
        ResetGame();
        ChangeState(GameState.Playing);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LevelTutorial")
        {
            ResetGame();
            ChangeState(GameState.Playing);
        }
    }
    #endregion

    #region Player Events
    /// <summary>
    /// Metodo para cuando dañan al jugador
    /// </summary>
    private void HandlePlayerHit()
    {

        Debug.Log("Jugador se quito una vida: " + currentLives);

        currentLives--;

        GameEvents.RaiseLivesChanged(currentLives);

        if (currentLives <= 0)
        {
            currentLives = 0;
            Debug.Log("PLAYER DIED EVENT");
            GameEvents.RaisePlayerDied();
        }
    }
    #endregion

    #region Restart
    /// <summary>
    /// Resetear las estadisticas por nivel
    /// </summary>
    public void ResetGame()
    {
        Time.timeScale = 1f;

        currentLives = startingLives;
        HasWand = false;

        GameEvents.RaiseLivesChanged(currentLives);
    }
    #endregion

    #region Wand
    public void SetWand(bool value)
    {
        HasWand = value;
    }
    #endregion
    public void ChangeState(GameState newState)
    {
        bool isSameState = CurrentState == newState;

        CurrentState = newState;

        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                if (!isSameState)
                    GameEvents.RaiseLevelResumed();
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                if (!isSameState)
                    GameEvents.RaiseLevelStopped();
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }
}
