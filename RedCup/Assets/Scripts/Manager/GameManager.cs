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
    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
    // logica pasar de escena
    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    // logica si el jugador muere
    public void Die()
    {
        IsPlayerDead = true;
        lives--;
        UpdateLivesText();
        StopEnemies();
        StopEnemiesSpawn();
        StartCoroutine(WaitAndRestart(0.5f));
    }

    // resetear juego
    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        Destroy(AudioManager.Instance.gameObject);
    }

    private void Reset()
    {
        enemiesLeft = 0;
        allWavesSpawned = false;
        IsPlayerDead = false;
    }

    // segundos antes de reiniciar cada ronda
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

    // reestablecer o parar spawners de enemigos
    private void StopEnemiesSpawn()
    {
        Spawner spawner = FindFirstObjectByType<Spawner>();
        spawner.StopAllCoroutines();
    }

    private void StopEnemies()
    {
        EnemyIA[] enemies = FindObjectsByType<EnemyIA>(FindObjectsSortMode.None);

        foreach (EnemyIA enemy in enemies)
        {
            enemy.StopMovement();
        }
    }
    // incremento o decremento en lista restantes de enemigos
    public void IncreaseEnemiesLeft()
    {
        enemiesLeft++;
    }

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
}
