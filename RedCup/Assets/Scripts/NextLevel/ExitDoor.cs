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
            GameEvents.RaiseLevelCompleted();
        }
        else
        {
            Debug.Log("No cumpliste los objetivos del nivel.");
        }
    }
}
