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
    // Actualizar la vida
    private int currentLives;
    // Propiedades
    public int Lives => currentLives;
    // Estado del arma
    public bool HasWand { get; private set; }
    // Para definir en que nivel ya empieza con el arma
    [SerializeField] private bool startWithWand;

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

    private void OnEnable()
    {
        GameEvents.OnPlayerHit += HandlePlayerHit;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerHit -= HandlePlayerHit;
    }

    private void Start()
    {
        ResetGame();
    }
    #endregion

    #region Player Events
    /// <summary>
    /// Metodo para cuando dañan al jugador
    /// </summary>
    private void HandlePlayerHit()
    {

        Debug.Log("Jugador se quito una vida");

        currentLives--;

        GameEvents.RaiseLivesChanged(currentLives);

        if (currentLives <= 0)
        {
            GameEvents.RaisePlayerDied();
        }
    }
    #endregion

    #region Restart
    public void StartNewGame()
    {
        ResetGame();
    }
    /// <summary>
    /// Resetear las estadisticas por nivel
    /// </summary>
    public void ResetGame()
    {
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
}
