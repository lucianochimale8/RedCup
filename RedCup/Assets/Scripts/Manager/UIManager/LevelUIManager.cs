using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    private bool isPaused;
    private void OnEnable()
    {
        GameEvents.OnPlayerDied += OnGameOver;
        GameEvents.OnLevelCompleted += ResumeGame;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDied -= OnGameOver;
        GameEvents.OnLevelCompleted -= ResumeGame;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GestorUI.Instance.PanelActual == PanelType.GameOver)
                return;

            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        GestorUI.Instance.MostrarPanel(PanelType.Pausa);   
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        GestorUI.Instance.MostrarPanel(PanelType.HUD);   
    }
    private void OnGameOver()
    {
        isPaused = true;
        Time.timeScale = 0f;
        GestorUI.Instance.MostrarPanel(PanelType.GameOver);
        Debug.Log("GAME OVER MOSTRADO");
    }
}
