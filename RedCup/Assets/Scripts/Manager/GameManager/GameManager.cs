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
    // Estado del arma
    public bool HasWand { get; private set; }
    // Para definir en que nivel ya empieza con el arma
    [SerializeField] private bool startWithWand;
    // Referencia al gestorUI
    private GestorUI gestorUI;

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

        HasWand = startWithWand;
        currentLives = startingLives;
    }
    /// <summary>
    /// Eventos
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        GameEvents.OnPlayerHit += HandlePlayerHit;
        GameEvents.OnPlayerDied += HandlePlayerDeath;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameEvents.OnPlayerHit -= HandlePlayerHit;
        GameEvents.OnPlayerDied -= HandlePlayerDeath;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gestorUI = GestorUI.Instance;

        // Reiniciar HUD y paneles al cargar la escena
        if (gestorUI != null)
        {
            gestorUI.OcultarTodos();
            // Mostrar panel de inicio si es MenuUI
            if (scene.name == "MenuUI")
            {
                if (gestorUI.HasPanel(PanelType.MenuInicio))
                    gestorUI.MostrarPanel(PanelType.MenuInicio);
                else
                    Debug.LogError("MenuInicio no asignado en GestorUI de MenuUI");
            }
            else if (gestorUI.HasPanel(PanelType.HUD))
            {
                gestorUI.MostrarPanel(PanelType.HUD);
            }
        }
        // Resetear GameManager
        isGameOver = false;

        // Resetear HUD de vidas
        if (livesText != null)
            livesText.gameObject.SetActive(true);
    }

    private void Start()
    {
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

        Debug.Log("Jugador se quito una vida");

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
        if (isGameOver) return;
        StartCoroutine(GameOverRoutine());
    }
    #endregion

    #region Restart
    /// <summary>
    /// Metodo para resetear el nivel
    /// </summary>
    private IEnumerator RestartLevel()
    {
        Debug.Log("Restart de level");

        GameEvents.LevelStopped();

        yield return new WaitForSecondsRealtime(0.6f);

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

        yield return new WaitForSecondsRealtime(1f);
        
        while (GestorUI.Instance == null)
            yield return null;

        Debug.Log("Intentando mostrar GameOver");

        GestorUI.Instance.MostrarPanel(PanelType.GameOver);

        if (livesText != null)
            livesText.gameObject.SetActive(false);
    }
    /// <summary>
    /// Resetear las estadisticas por nivel
    /// </summary>
    public void ResetGame()
    {
        currentLives = startingLives;
        HasWand = startWithWand;

        UpdateLivesUI();

        if (livesText != null)
            livesText.gameObject.SetActive(true);

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

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;

        ResetGame();

        SceneManager.LoadScene("MenuUI");
    }
    #endregion

    #region Wand
    public void SetWand(bool value)
    {
        HasWand = value;
    }

    #endregion
}
