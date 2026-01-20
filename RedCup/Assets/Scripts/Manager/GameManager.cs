using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] protected TMP_Text livesText;

    private int enemiesLeft;
    private bool allWavesSpawned;

    public int Lives => lives;
    public bool IsPlayerDead { get; private set; }
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        Reset();
        UpdateLivesText();
    }
    /// <summary>
    /// Actualizacion de Texto de las vidas restantes del jugador
    /// </summary>
    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
   
    /// <summary>
    /// Logica de cuamdo el jugador, muere
    /// </summary>
    public void Die()
    {
        IsPlayerDead = true;
        lives--;
        UpdateLivesText();
        StopEnemiesMovement();
        StopEnemiesSpawn();
        StartCoroutine(WaitAndRestart(0.5f));
    }
    #region  Logica de reinicio de ronda y partida
    public void PlayerHit()
    {
        if (IsPlayerDead) return;
        Die();
    }
    /// <summary>
    /// Reinicio de partida para reiniciar el nivel
    /// </summary>
    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        Destroy(AudioManager.Instance.gameObject);
    }
    /// <summary>
    /// Reinicio de estadisticas de cada ronda
    /// </summary>
    private void Reset()
    {
        enemiesLeft = 0;
        allWavesSpawned = false;
        IsPlayerDead = false;
    }
    /// <summary>
    /// Corrutina para usar unos segundos antes de reiniciar cada ronda
    /// </summary>
    private IEnumerator WaitAndRestart(float restartTime)
    {
        yield return new WaitForSeconds(restartTime);
        Reset();

        if (lives > 0)
        {
            int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(activeSceneIndex);
        }
        else
        {
            ResetGame();
        }
    }
    #endregion
    #region Neutralizar movimiento de los enemigos y su reaparicion
    /// <summary>
    /// Metodo para parar la reaparicion de los enemigos
    /// </summary>
    private void StopEnemiesSpawn()
    {
        Spawner spawner = FindFirstObjectByType<Spawner>();
        spawner.StopAllCoroutines();
    }
    /// <summary>
    /// Metodo para parar el movimiento de cada enemigo en partida
    /// </summary>
    private void StopEnemiesMovement()
    {
        EnemyIA[] enemies = FindObjectsByType<EnemyIA>(FindObjectsSortMode.None);

        foreach (EnemyIA enemy in enemies)
        {
            enemy.StopMovement();
        }
    }
    #endregion
    #region Sistema de conteo de los enemigos restantes
    /// <summary>
    /// Incrementacion en el contador de enemigos restantes
    /// </summary>
    public void IncreaseEnemiesLeft()
    {
        enemiesLeft++;
    }
    /// <summary>
    /// Decrementacion en el contador de enemigos restantes
    /// </summary>
    public void DecreaseEnemiesLeft()
    {
        enemiesLeft--;

        if (enemiesLeft == 0 && allWavesSpawned)
        {
            // pasar de nivel
            LoadNextScene();
        }
    }
    // setear que todas las waves fueron spawneadas
    public void SetAllWavesSpawned()
    {
        allWavesSpawned = true;
    }
    /// <summary>
    /// Metodo con la logica para pasar de scene
    /// </summary>
    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
    #endregion
}
