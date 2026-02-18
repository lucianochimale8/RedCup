using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (LevelObjectiveManager.Instance != null &&
            LevelObjectiveManager.Instance.CanExitLevel())
        {
            nextSceneIndex++;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No cumpliste los objetivos del nivel.");
        }
    }
}
