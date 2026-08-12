using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public enum TimerState
    {
        Stopped,
        Running
    }
    public static TimeManager Instance { get; private set; }

    private float elapsedTime;
    private TimerState currentState = TimerState.Stopped;

    public float ElapsedTime => elapsedTime;
    public TimerState CurrentState => currentState;

    #region Unity Lifecycle
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (currentState == TimerState.Stopped)
        {
            StartTimer();
        }
    }
    private void Update()
    {
        // Regla de Guarda por Estado
        if (currentState != TimerState.Running)
            return;

        elapsedTime += Time.deltaTime;
    }
    #endregion

    #region Modificar el tiempo
    public void StartTimer()
    {
        elapsedTime = 0f;
        currentState = TimerState.Running;
    }
    public void StopTimer()
    {
        currentState = TimerState.Stopped;
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
        currentState = TimerState.Stopped;
    }
    public void ResumeTimer()
    {
        if (currentState == TimerState.Stopped)
        {
            currentState = TimerState.Running;
        }
    }
    #endregion

    #region Get Time
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion
}
