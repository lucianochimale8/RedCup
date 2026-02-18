using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [Header("Botones")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    public static string LEVEL1_SCENE = "Level1";

    #region Unity Lifecycle
    private void Awake()
    {
        if (playButton != null)
            playButton.onClick.AddListener(StartGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }
    #endregion

    #region Play & Quit
    private void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(LEVEL1_SCENE);
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
    #endregion
}
