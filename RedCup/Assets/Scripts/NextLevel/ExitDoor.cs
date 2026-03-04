using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private LevelObjectiveManager objectiveManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (objectiveManager != null && objectiveManager.CanExitLevel())
        {
            LoadNextScene();
        }
        else
        {
            Debug.Log("No cumpliste los objetivos del nivel.");
        }
    }

    private void LoadNextScene()
    {
        Time.timeScale = 1f;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("No hay más niveles configurados.");
        }
    }
}
