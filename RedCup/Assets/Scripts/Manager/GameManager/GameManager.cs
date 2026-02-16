using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Singleton del GameManager
    public static GameManager Instance { get; private set; }

    // Sistema de vida
    [Header("Vidas")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private TMP_Text livesText;
    // Actualizar la vida
    private int currentLives;
    // Para saber cuando deberia lanzar el gameover
    public bool isGameOver;
    // Para saber cuando el jugador esta muerto
    private bool isPlayerDead;
    // Propiedades
    public int Lives => currentLives;
    public bool IsPlayerDead => isPlayerDead;

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
    }
    /// <summary>
    /// Eventos
    /// </summary>
    private void OnEnable()
    {
        GameEvents.OnPlayerHit += HandlePlayerHit;
        GameEvents.OnPlayerDied += HandlePlayerDeath;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerHit -= HandlePlayerHit;
        GameEvents.OnPlayerDied -= HandlePlayerDeath;
    }

    private void Start()
    {
        currentLives = startingLives;
        UpdateLivesUI();
    }
    #endregion

    #region Player Events
    /// <summary>
    /// Metodo para cuando dañan al jugador
    /// </summary>
    private void HandlePlayerHit()
    {
        if (isGameOver) return;

        Debug.Log("se quito una vida");

        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
            StartCoroutine(GameOverRoutine());
        else
            StartCoroutine(RestartLevel());
    }
    /// <summary>
    /// Metodo para cuando el jugador deberia morir
    /// </summary>
    private void HandlePlayerDeath()
    {
        Debug.Log("GameManager detectó muerte");
        StartCoroutine(GameOverRoutine());
    }
    #endregion

    #region Restart
    /// <summary>
    /// Metodo para resetear el nivel
    /// </summary>
    private IEnumerator RestartLevel()
    {
        Debug.Log("Restart");

        GameEvents.LevelStopped();

        yield return new WaitForSeconds(0.6f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Corrutina para el GameOver
    /// </summary>
    private IEnumerator GameOverRoutine()
    {
        isGameOver = true;

        Debug.Log("Entró en GameOver");

        GameEvents.LevelStopped();

        yield return new WaitForSeconds(1f);

        ResetGame();
        
        currentLives = startingLives;
        UpdateLivesUI();

        isGameOver = false;

        Debug.Log("Cargando menú");

        SceneManager.LoadScene("MenuUI");
    }
    /// <summary>
    /// Resetear las estadisticas por nivel
    /// </summary>
    public void ResetGame()
    {
        currentLives = startingLives;
        isGameOver = false;
        isPlayerDead = false;
    }
    #endregion

    #region UI
    /// <summary>
    /// Metodo para actualizar las vidas restantes del jugador
    /// </summary>
    private void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = $"Lives: {currentLives}";
    }
    #endregion
}
