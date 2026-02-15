using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Vidas")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private TMP_Text livesText;
    
    private int currentLives; 
    public bool IsPlayerDead { get; private set; }

    [Header("Niveles")]
    [SerializeField] private string escenaFinal = "Win";

    [Header("UI Nivel")]
    [SerializeField] private GameObject levelCompleteText;

    public int Lives => currentLives;
    

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        currentLives = startingLives;
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

        IsPlayerDead = false;
        UpdateLivesText();
    }
    #endregion

    #region Player Events
    private void OnPlayerHit()
    {
        Debug.Log("EVENTO: Player Hit recibido");

        currentLives--;
        UpdateLivesText();

        if (currentLives > 0)
        {
            StartCoroutine(WaitAndRestart(0.6f));
        }
        else
        {
            StartCoroutine(GameOverRoutine());
        }
    }

    #endregion

    #region Restart
    private IEnumerator WaitAndRestart(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (currentLives > 0)
        {
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

        currentLives = startingLives;

        IsPlayerDead = false;

        SceneManager.LoadScene(0); // Menu principal
    }

    #endregion

    #region UI

    private void UpdateLivesText()
    {
        if (livesText != null)
            livesText.text = $"Lives: {currentLives}";
    }

    #endregion
}
