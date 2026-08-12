using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public enum DoorState
    {
        Locked,
        Unlocked,
        Transitioning
    }

    [SerializeField] private LevelObjectiveManager objectiveManager;

    private DoorState currentState = DoorState.Locked;
    public DoorState CurrentState => currentState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (currentState == DoorState.Transitioning)
            return;

        if (objectiveManager != null && objectiveManager.CanExitLevel())
        {
            SetState(DoorState.Unlocked);
            ExecuteTransition();
        }
        else
        {
            Debug.Log("No cumpliste los objetivos del nivel.");
        }
    }

    private void ExecuteTransition()
    {
        SetState(DoorState.Transitioning);

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

    private void SetState(DoorState newState)
    {
        currentState = newState;
    }
}
