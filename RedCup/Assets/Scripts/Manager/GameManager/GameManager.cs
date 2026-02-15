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
    public bool isGameOver;

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
    }
    private void OnEnable()
    {
        GameEvents.OnPlayerHit += HandlePlayerHit;
        GameEvents.OnPlayerDied += HandlePlayerDied;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerHit -= HandlePlayerHit;
        GameEvents.OnPlayerDied -= HandlePlayerDied;
    }

    private void Start()
    {
        currentLives = startingLives;
        UpdateLivesUI();
    }
    #endregion

    #region Player Events
    private void HandlePlayerHit()
    {
        if (isGameOver) return;

        currentLives--;
        UpdateLivesUI();

        //LevelStateController.Instance.StopLevel();

        if (currentLives > 0)
        {
            StartCoroutine(RestartLevel(0.6f));
        }
        else
        {
            StartCoroutine(GameOverRoutine());
        }
    }

    private void HandlePlayerDied()
    {
        HandlePlayerHit();
    }
    #endregion

    #region Restart
    private IEnumerator RestartLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator GameOverRoutine()
    {
        isGameOver = true;

        yield return new WaitForSeconds(1f);

        currentLives = startingLives;
        isGameOver = false;

        SceneManager.LoadScene(0);
    }
    #endregion

    #region UI
    private void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = $"Lives: {currentLives}";
    }
    #endregion
}
