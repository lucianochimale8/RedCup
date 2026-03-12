using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    #region Eventos
    private void OnEnable()
    {
        GameEvents.OnPlayerDied += OnGameOver;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerDied -= OnGameOver;
    }
    #endregion
    #region Unity Lifecycle
    private void Start()
    {
        if (GestorUI.Instance.HasPanel(PanelType.HUD))
            GestorUI.Instance.MostrarPanel(PanelType.HUD);
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
        } else 
            if (GameManager.Instance.CurrentState == GameState.Paused)
            ResumeGame();
    }
    #endregion
    #region Pause, Resume, GameOver
    private void PauseGame()
    {
        GameManager.Instance.ChangeState(GameState.Paused);
        GestorUI.Instance.MostrarPanel(PanelType.Pausa);
    }
    public void ResumeGame()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        GestorUI.Instance.MostrarPanel(PanelType.HUD);
    }
    private void OnGameOver()
    {  
        Debug.Log("GAME OVER MOSTRADO");
        GameManager.Instance.ChangeState(GameState.GameOver);
        GestorUI.Instance.MostrarPanel(PanelType.GameOver);
    }
    #endregion
}
