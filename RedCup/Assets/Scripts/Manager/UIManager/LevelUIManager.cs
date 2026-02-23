using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    // Gestor
    [SerializeField] private GestorUI gestorUI;
    #region Eventos
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
    #endregion
    #region Unity Lifecycle
    private void Start()
    {
        if (gestorUI.HasPanel(PanelType.HUD))
            gestorUI.MostrarPanel(PanelType.HUD);
    }
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;
        Debug.Log("Escape REAL detectado");

        if (GameManager.Instance.CurrentState == GameState.GameOver)
            return;

        if (GameManager.Instance.CurrentState == GameState.Playing)
        { 
            PauseGame();
            Debug.Log("Pausa");
        }

        else if (GameManager.Instance.CurrentState == GameState.Paused)
            ResumeGame();
    }
    #endregion
    #region Pause, Resume, GameOver
    private void PauseGame()
    {
        GameManager.Instance.ChangeState(GameState.Paused);
        gestorUI.MostrarPanel(PanelType.Pausa);
    }
    public void ResumeGame()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        gestorUI.MostrarPanel(PanelType.HUD);
    }
    private void OnGameOver()
    {  
        Debug.Log("GAME OVER MOSTRADO");
        GameManager.Instance.ChangeState(GameState.GameOver);
        gestorUI.MostrarPanel(PanelType.GameOver);
    }
    #endregion
}
